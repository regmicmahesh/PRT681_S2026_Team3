# Week 2 Practice (Simplified) — Selenium + Postman

Goal: automate 5 of your Week 1 manual login test cases with Selenium, and write 5 API test requests in Postman. Kept intentionally small — this should take about 1–1.5 hours total, not a full framework build.

---

## Part A: Selenium — Automate 5 Login Test Cases (Python)

### One-time setup

```bash
pip install selenium webdriver-manager pytest
```

`webdriver-manager` handles the ChromeDriver download for you — no manual driver setup needed.

### The script

Save as `test_login.py` and run with `pytest test_login.py -v`.

```python
"""
Selenium practice — automates 5 of the Week 1 manual test cases
for the SauceDemo login feature (TC01, TC03, TC06, TC08, TC13).
"""

import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager

LOGIN_URL = "https://www.saucedemo.com/"


@pytest.fixture
def driver():
    service = Service(ChromeDriverManager().install())
    drv = webdriver.Chrome(service=service)
    drv.get(LOGIN_URL)
    yield drv
    drv.quit()


def login(driver, username, password):
    driver.find_element(By.ID, "user-name").clear()
    driver.find_element(By.ID, "user-name").send_keys(username)
    driver.find_element(By.ID, "password").clear()
    driver.find_element(By.ID, "password").send_keys(password)
    driver.find_element(By.ID, "login-button").click()


def get_error_text(driver):
    return driver.find_element(By.CSS_SELECTOR, "[data-test='error']").text


# TC01 — Functional: valid login succeeds
def test_valid_login(driver):
    login(driver, "standard_user", "secret_sauce")
    assert "inventory" in driver.current_url


# TC03 — Functional: locked-out account is blocked
def test_locked_out_user(driver):
    login(driver, "locked_out_user", "secret_sauce")
    assert "locked out" in get_error_text(driver).lower()


# TC06 — Negative: wrong password
def test_wrong_password(driver):
    login(driver, "standard_user", "wrongpass")
    assert "do not match" in get_error_text(driver).lower()


# TC08 — Negative: empty username
def test_empty_username(driver):
    login(driver, "", "secret_sauce")
    assert "username is required" in get_error_text(driver).lower()


# TC13 — Boundary: very long input string
def test_long_username_input(driver):
    login(driver, "a" * 500, "secret_sauce")
    assert get_error_text(driver) != ""  # confirms app didn't crash
```

That's it — 5 tests, mapped directly to test cases you already wrote by hand in Week 1. Nothing fancier (no Page Object Model, no reporting plugin) needed yet.

---

## Part B: Postman — 5 API Test Requests

**Using [dummyjson.com](https://dummyjson.com)** — free, no signup, no API key.
_(If you'd rather use reqres.in, note it now requires a free API key — sign up at reqres.in and add header `x-api-key: <your key>` to every request.)_

### Setup

1. Open Postman (desktop or web) → New Collection → name it `Week 2 API Practice`.
2. Add each request below. For each one, paste the assertion code into the request's **"Tests"** tab (Postman runs this JS after the response comes back).

| #   | Type              | Method & URL                                 | Body                        | Assertions to write in "Tests" tab                                         |
| --- | ----------------- | -------------------------------------------- | --------------------------- | -------------------------------------------------------------------------- |
| 1   | Functional        | `GET https://dummyjson.com/products/1`       | —                           | `pm.response.to.have.status(200)` and `pm.response.json().id === 1`        |
| 2   | Functional        | `GET https://dummyjson.com/products?limit=5` | —                           | Status 200; `pm.response.json().products.length <= 5`                      |
| 3   | Negative/Boundary | `GET https://dummyjson.com/products/99999`   | —                           | `pm.response.to.have.status(404)` (nonexistent ID)                         |
| 4   | Functional        | `POST https://dummyjson.com/products/add`    | `{"title": "Test Product"}` | Status 201; response body has an `id` field and `title === "Test Product"` |
| 5   | Functional        | `DELETE https://dummyjson.com/products/1`    | —                           | Status 200; response body has `isDeleted === true`                         |

### Example test script (paste into the "Tests" tab, adjust per request)

```javascript
pm.test("Status code is correct", function () {
    pm.response.to.have.status(200); // change per request
});

pm.test("Response has expected field", function () {
    const json = pm.response.json();
    pm.expect(json).to.have.property("id");
});
```

Run each request once, confirm the green checkmarks appear under the "Test Results" tab — that's your pass/fail evidence, same as the manual test case log from Week 1.

---

## Time Budget (~1–1.5 hrs)

- 20 min — install Selenium/webdriver-manager, run the 5 provided tests, confirm they pass.
- 20 min — (optional, if time) tweak one test's data to see it fail, then fix it — good for understanding failure output.
- 30 min — build the 5 Postman requests + test scripts, run them, confirm results.
- 10 min — note anything that surprised you (e.g., does `products/99999` really 404? does delete really respond 200?) — these observations are useful next week when we go deeper into automation frameworks.
