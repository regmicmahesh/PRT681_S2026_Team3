# Week 5 Practice — Selenium Grid (Docker) + CI Regression Gating

---

## Part A: Run the Suite Against a Multi-Browser Grid, Locally (~1 hr)

### 1. Add the Grid definition

Create `qa-tests/docker-compose.grid.yml`:

```yaml
version: "3"
services:
    selenium-hub:
        image: selenium/hub:latest
        container_name: selenium-hub
        ports:
            - "4442:4442"
            - "4443:4443"
            - "4444:4444"

    chrome:
        image: selenium/node-chrome:latest
        shm_size: 2gb
        depends_on:
            - selenium-hub
        environment:
            - SE_EVENT_BUS_HOST=selenium-hub
            - SE_EVENT_BUS_PUBLISH_PORT=4442
            - SE_EVENT_BUS_SUBSCRIBE_PORT=4443

    firefox:
        image: selenium/node-firefox:latest
        shm_size: 2gb
        depends_on:
            - selenium-hub
        environment:
            - SE_EVENT_BUS_HOST=selenium-hub
            - SE_EVENT_BUS_PUBLISH_PORT=4442
            - SE_EVENT_BUS_SUBSCRIBE_PORT=4443
```

### 2. Start the Grid

```bash
docker compose -f qa-tests/docker-compose.grid.yml up -d
```

Check it's ready:

```bash
curl http://localhost:4444/status
```

Look for `"ready": true` in the response. Optional: visit `http://localhost:4444/ui` in a browser to see the Grid's own dashboard with connected nodes.

### 3. Update the test fixture to use the Grid

Modify the `driver` fixture in `qa-tests/test_login.py`, replace the local headless Chrome fixture with a Remote WebDriver one, parametrised across both browsers:

```python
import pytest
from selenium import webdriver

GRID_URL = "http://localhost:4444/wd/hub"
LOGIN_URL = "https://www.saucedemo.com/"


@pytest.fixture(params=["chrome", "firefox"])
def driver(request):
    if request.param == "chrome":
        options = webdriver.ChromeOptions()
    else:
        options = webdriver.FirefoxOptions()

    drv = webdriver.Remote(command_executor=GRID_URL, options=options)
    drv.get(LOGIN_URL)
    yield drv
    drv.quit()
```

Because the fixture is parametrised, every existing test in the file now automatically runs twice, once per browser, with no changes needed to the test functions themselves.

### 4. Run it

```bash
pytest qa-tests/test_login.py -v
```

You should see each test name appear twice, e.g. `test_valid_login[chrome]` and `test_valid_login[firefox]`. Screenshot the output for your results folder.

### 5. Tear down

```bash
docker compose -f qa-tests/docker-compose.grid.yml down
```

_(Optional, not required: add `pytest-xdist` and run `pytest -n auto` to actually execute these in parallel rather than one after another, Grid supports concurrent sessions either way, this just controls whether your test runner takes advantage of it.)_

---

## Part B: Wire the Grid into GitHub Actions + Prove It Fails on Regression (~45 min)

### 1. New workflow file

Create `.github/workflows/qa-selenium-grid-tests.yml` (separate from the Week 3 workflow, so both are visible in your Actions history):

```yaml
name: QA Practice - Selenium Grid Tests

on:
    push:
        branches: [main]
        paths:
            - "qa-tests/**"

jobs:
    selenium-grid-tests:
        runs-on: ubuntu-latest
        steps:
            - uses: actions/checkout@v4

            - name: Set up Python
              uses: actions/setup-python@v5
              with:
                  python-version: "3.12"

            - name: Start Selenium Grid
              run: docker compose -f qa-tests/docker-compose.grid.yml up -d

            - name: Wait for Grid to be ready
              run: |
                  for i in $(seq 1 30); do
                    if curl -sf http://localhost:4444/status | grep -q '"ready": *true'; then
                      echo "Grid is ready"
                      exit 0
                    fi
                    echo "Waiting for grid..."
                    sleep 2
                  done
                  echo "Grid did not become ready in time"
                  exit 1

            - name: Install dependencies
              run: pip install -r qa-tests/requirements.txt

            - name: Run Selenium Grid tests
              run: pytest qa-tests/test_login.py -v

            - name: Stop Selenium Grid
              if: always()
              run: docker compose -f qa-tests/docker-compose.grid.yml down
```

`if: always()` on the teardown step means the Grid gets stopped even if the tests fail, not just on success.

### 2. Push it and confirm it passes

```bash
git add qa-tests/docker-compose.grid.yml qa-tests/test_login.py .github/workflows/qa-selenium-grid-tests.yml
git commit -m "Add Selenium Grid (Docker) and wire into GitHub Actions"
git push
```

Check the Actions tab, both `[chrome]` and `[firefox]` variants of each test should show as passed. Screenshot this.

### 3. Prove it actually fails the build on a regression

This is the part that's easy to skip but is the actual point of "gating", don't just assume pytest's exit code will fail the build, show it:

1. Temporarily break something real, e.g. in `test_valid_login`, change the assertion to `assert "wrong-page" in driver.current_url`.
2. Commit and push.
3. Watch the Actions run go red. Screenshot it.
4. Revert the change, push again, confirm it goes back to green. Screenshot that too.

That before/after pair (deliberate failure to red build, fix to green build) is your actual evidence that the pipeline gates on regressions, rather than just asserting that it does.

---
