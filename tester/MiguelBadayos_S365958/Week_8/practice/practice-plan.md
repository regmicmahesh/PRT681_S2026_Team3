# Week 3 Practice (Simplified) — GitHub Actions + Jira

Goal: get your Week 2 Selenium tests running automatically in CI, and consolidate all bugs found so far into a Jira board with severity/priority tagged. Kept deliberately small — roughly 1–1.5 hrs, same as previous weeks.

---

## Part A: Push Your Selenium Tests to GitHub

### 1. Create the repo
On github.com → **New repository** → name it e.g. `qa-practice` → keep it **Public** (so GitHub Actions is free) → Create.

### 2. Push your existing code
In the folder containing your `test_login.py` from Week 2:
```bash
git init
git add test_login.py
git commit -m "Add Week 2 Selenium login tests"
git branch -M main
git remote add origin https://github.com/<your-username>/qa-practice.git
git push -u origin main
```

### 3. Add a requirements file
So GitHub Actions knows what to install. Create `requirements.txt`:
```
selenium
pytest
```
Commit and push this too:
```bash
git add requirements.txt
git commit -m "Add requirements.txt"
git push
```

---

## Part B: GitHub Actions Workflow

### 1. Update your test script to run headless
CI runners have no visible screen, so the browser must run in **headless mode**. Update the `driver` fixture in `test_login.py`:

```python
from selenium.webdriver.chrome.options import Options

@pytest.fixture
def driver():
    options = Options()
    options.add_argument("--headless=new")
    options.add_argument("--window-size=1920,1080")
    options.add_argument("--no-sandbox")
    drv = webdriver.Chrome(options=options)
    drv.get(LOGIN_URL)
    yield drv
    drv.quit()
```
(This also works fine locally — the browser just won't visibly pop up anymore, which is normal for CI-style tests.)

### 2. Create the workflow file
Create `.github/workflows/selenium-tests.yml`:

```yaml
name: Selenium Test Automation

on:
  push:
    branches: [ main ]

jobs:
  selenium-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Set up Python
        uses: actions/setup-python@v5
        with:
          python-version: '3.12'

      - name: Install Chrome
        uses: browser-actions/setup-chrome@v1

      - name: Install dependencies
        run: pip install -r requirements.txt

      - name: Run Selenium tests
        run: pytest test_login.py -v
```

### 3. Push and watch it run
```bash
git add .github/workflows/selenium-tests.yml test_login.py
git commit -m "Add GitHub Actions workflow"
git push
```
Go to your repo's **Actions** tab on GitHub — you should see the workflow running, then a green checkmark (or a red X with logs telling you what failed, which is useful information too).

---

## Part C: Consolidate Bugs into Jira

### 1. Create a free Jira site
Go to id.atlassian.com/signup → create a Jira site → choose **Kanban** template (simplest, no sprint setup needed) → name the project e.g. `QA Practice`.

### 2. Add a Severity field (Priority already exists by default)
Go to **Project settings → Fields** (or just create the issue first — Jira lets you add a "Severity" custom field with values Low/Medium/High/Critical from the issue screen directly in team-managed projects).

### 3. Migrate your bugs
For each bug you've already logged (in Trello and/or the Excel template — should be around 3–5 total from Weeks 1–2):
1. Click **Create** → Issue Type: **Bug**.
2. Paste in the Title, Steps to Reproduce, Expected/Actual Result (same content you already wrote — this is a copy-over, not a rewrite).
3. Set **Priority** (business urgency) and **Severity** (technical impact) — refer to the Severity vs. Priority table in the notes if unsure which is which.
4. Set **Status** to match reality (most are probably still "To Do" since nothing's been fixed).

### 4. (Optional) Link a bug to its test case
In the issue description, just reference the test case ID, e.g. "Found via TC12 — case sensitivity check." Jira also supports formal "linked issues" if you want to try that, but a text reference is enough for this practice.

---

## Time Budget (~1–1.5 hrs)
- 15 min — push code to GitHub, add requirements.txt.
- 30 min — update the driver fixture for headless mode, add the workflow file, debug until it goes green.
- 25 min — set up the Jira Kanban project and migrate existing bugs with severity/priority.
- 10 min — note anything CI revealed that manual runs didn't (e.g., a test that only fails headless) — that's a genuinely useful real-world observation, not something you need to fix right now.
