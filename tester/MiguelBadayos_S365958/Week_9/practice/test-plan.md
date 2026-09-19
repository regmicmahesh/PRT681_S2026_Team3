# Week 4 Practice — JMeter Load Test + BDD/Page Object Model

---

## Part A: JMeter 50-User Load Test (~45 min)

### 1. Install JMeter

Download from jmeter.apache.org (no install needed, it's a zip — extract and run `bin/jmeter.bat` on Windows or `bin/jmeter.sh` on Mac/Linux). Requires Java — JMeter will tell you if it's missing.

### 2. Open the ready-made test plan

A working test plan is attached: **`week4-load-test.jmx`**. Open it via JMeter → File → Open.

It's already configured with:

- **Thread Group:** 50 users, 10-second ramp-up, 1 loop each
- **HTTP Request:** `GET https://dummyjson.com/products`
- **Two Listeners:** Summary Report (the numbers) and View Results Tree (individual request/response detail)

_(Swap the domain/path in the HTTP Request sampler if you'd rather point it at a different public endpoint — e.g. `reqres.in/api/users`.)_

### 3. Run it

Click the green ▶ (Start) button. With only 50 users and a 10s ramp-up, this finishes in seconds.

### 4. Read the Summary Report

| Column         | What to look at                                                                                        |
| -------------- | ------------------------------------------------------------------------------------------------------ |
| **# Samples**  | Should be 50 (one request per virtual user)                                                            |
| **Average**    | Typical response time in ms                                                                            |
| **Min / Max**  | Range — a big gap between them can indicate inconsistent performance                                   |
| **Error %**    | Should be 0% against a healthy public API; anything higher is worth investigating in View Results Tree |
| **Throughput** | Requests/second the endpoint handled                                                                   |

### 5. Write up your findings (a few sentences is enough)

- What was the average response time and throughput at 50 concurrent users?
- Was the error rate 0%? If not, what error came back (check View Results Tree for the response code/body)?
- Does anything suggest the endpoint would struggle at higher concurrency (e.g., response time climbing steeply even at just 50 users)?

Save a screenshot of the Summary Report to your results folder, same pattern as previous weeks.

---

## Part B: Convert Selenium Suite to BDD + Page Object Model (~1–1.25 hrs)

Refactors your existing `test_login.py` into a proper Page Object, then adds a Gherkin/behave layer on top of it, same underlying Selenium actions, described in plain language.

> **Cucumber vs. behave:** the course lists "Cucumber," but Cucumber has no maintained Python binding (it's native to Java/Ruby/JS). `behave` is the direct Python equivalent — it reads the identical Gherkin `.feature` syntax, just with a Python-native runner, which fits an existing Python/Selenium/pytest suite without a rewrite. See `jmeter-and-bdd-notes.md` (Part 2 §3) for the full comparison.

### 1. Install behave

```bash
pip install behave
```

Add it to `qa-tests/requirements.txt` too.

### 2. Suggested folder structure

```
qa-tests/
├── test_login.py              (existing pytest suite, keep as-is)
├── requirements.txt
├── pages/
│   └── login_page.py           (new, Page Object)
└── features/
    ├── login.feature           (new, Gherkin scenarios)
    ├── environment.py          (new, setup/teardown hooks)
    └── steps/
        └── login_steps.py      (new, step definitions)
```

### 3. Page Object — `pages/login_page.py`

```python
from selenium.webdriver.common.by import By


class LoginPage:
    URL = "https://www.saucedemo.com/"

    USERNAME_INPUT = (By.ID, "user-name")
    PASSWORD_INPUT = (By.ID, "password")
    LOGIN_BUTTON = (By.ID, "login-button")
    ERROR_MESSAGE = (By.CSS_SELECTOR, "[data-test='error']")

    def __init__(self, driver):
        self.driver = driver

    def load(self):
        self.driver.get(self.URL)

    def login(self, username, password):
        self.driver.find_element(*self.USERNAME_INPUT).clear()
        self.driver.find_element(*self.USERNAME_INPUT).send_keys(username)
        self.driver.find_element(*self.PASSWORD_INPUT).clear()
        self.driver.find_element(*self.PASSWORD_INPUT).send_keys(password)
        self.driver.find_element(*self.LOGIN_BUTTON).click()

    def get_error_text(self):
        return self.driver.find_element(*self.ERROR_MESSAGE).text

    def is_logged_in(self):
        return "inventory" in self.driver.current_url
```

### 4. Gherkin scenarios — `features/login.feature`

Same 3 test cases you've already automated with pytest (TC01, TC06, TC08), now described in plain language:

```gherkin
Feature: SauceDemo Login
  As a user of the SauceDemo store
  I want to log in with my credentials
  So that I can access the product inventory

  Scenario: Valid login succeeds
    Given I am on the SauceDemo login page
    When I log in with username "standard_user" and password "secret_sauce"
    Then I should be redirected to the inventory page

  Scenario: Login fails with wrong password
    Given I am on the SauceDemo login page
    When I log in with username "standard_user" and password "wrongpass"
    Then I should see an error message containing "do not match"

  Scenario: Login fails with empty username
    Given I am on the SauceDemo login page
    When I log in with username "" and password "secret_sauce"
    Then I should see an error message containing "Username is required"
```

### 5. Step definitions — `features/steps/login_steps.py`

```python
from behave import given, when, then
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from pages.login_page import LoginPage


@given('I am on the SauceDemo login page')
def step_open_login_page(context):
    options = Options()
    options.add_argument("--headless=new")
    options.add_argument("--window-size=1920,1080")
    context.driver = webdriver.Chrome(options=options)
    context.login_page = LoginPage(context.driver)
    context.login_page.load()


@when('I log in with username "{username}" and password "{password}"')
def step_login(context, username, password):
    context.login_page.login(username, password)


@then('I should be redirected to the inventory page')
def step_check_redirect(context):
    assert context.login_page.is_logged_in(), "Expected redirect to inventory page"


@then('I should see an error message containing "{expected_text}"')
def step_check_error(context, expected_text):
    actual = context.login_page.get_error_text()
    assert expected_text.lower() in actual.lower(), f"Expected '{expected_text}' in '{actual}'"
```

### 6. Cleanup hook — `features/environment.py`

```python
def after_scenario(context, scenario):
    if hasattr(context, "driver"):
        context.driver.quit()
```

### 7. Run it

From inside `qa-tests/`:

```bash
behave
```

You should see all 3 scenarios pass, with each Gherkin step shown as it executes, that's the "living documentation" aspect of BDD: the test output itself reads like a plain-English spec.

---

## Time Budget (~2 hrs)

- 15 min — install JMeter, open the provided `.jmx`, run it.
- 15 min — read the Summary Report, write up findings, screenshot.
- 15 min — install behave, set up the folder structure.
- 45 min — add the Page Object, feature file, and step definitions; debug until `behave` runs green.
- 10 min — note anything BDD made clearer or more awkward compared to the plain pytest version, useful for the Week 4/5 capstone writeup.
