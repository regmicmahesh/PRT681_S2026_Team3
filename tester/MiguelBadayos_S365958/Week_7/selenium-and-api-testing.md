# Week 2 Notes: Selenium, Test Automation & API Testing

---

## PART 1: Selenium Essential Training

### 1. What is Selenium?

Selenium is a free, open-source framework for automating web browsers. It lets a script open a browser, interact with elements (click, type, navigate), and check results — instead of a human doing it manually.

| Component              | Purpose                                                                                           |
| ---------------------- | ------------------------------------------------------------------------------------------------- |
| **Selenium WebDriver** | The core library — sends commands directly to the browser. This is what you'll use for scripting. |
| **Selenium IDE**       | A browser extension for recording/playback of simple tests, no coding required.                   |
| **Selenium Grid**      | Runs tests across multiple machines/browsers in parallel.                                         |

---

### 2. Locator Strategies

Selenium finds elements on a page using a **locator**. Choosing the right one matters — a fragile locator makes tests break often (relates to Principle #5, the Pesticide Paradox, and to test flakiness).

| Locator        | Example                                              | Notes                                      |
| -------------- | ---------------------------------------------------- | ------------------------------------------ |
| `id`           | `find_element(By.ID, "user-name")`                   | Fastest & most stable — use when available |
| `name`         | `find_element(By.NAME, "email")`                     | Good when `id` isn't present               |
| `css selector` | `find_element(By.CSS_SELECTOR, ".btn-primary")`      | Flexible, widely used                      |
| `xpath`        | `find_element(By.XPATH, "//button[text()='Login']")` | Powerful but slower & more brittle         |

---

### 3. Core WebDriver Commands

| Command              | Does                                  |
| -------------------- | ------------------------------------- |
| `driver.get(url)`    | Navigate to a page                    |
| `find_element(...)`  | Locate a single element               |
| `.click()`           | Click a button/link                   |
| `.send_keys("text")` | Type into a field                     |
| `.text`              | Read visible text from an element     |
| `driver.quit()`      | Close the browser and end the session |

---

### 4. Waits (avoiding flaky tests)

Web pages load asynchronously — if your script looks for an element before it exists, the test fails even though the app is fine.

| Wait Type         | Behaviour                                                                                                       |
| ----------------- | --------------------------------------------------------------------------------------------------------------- |
| **Implicit Wait** | Applies globally; tells the driver to poll for a set time before giving up                                      |
| **Explicit Wait** | Waits for a _specific condition_ (e.g., element becomes clickable) before proceeding — preferred, more reliable |

**Key Takeaway:** Selenium automates _browser actions_ — locating elements, interacting with them, and reading results — but the test logic (what to check, what counts as pass/fail) is still written by the tester, usually with a framework like `pytest` or `unittest`.

---

## PART 2: Learning Test Automation

### 1. What to Automate (and What Not To)

| Good candidates for automation               | Better left manual                             |
| -------------------------------------------- | ---------------------------------------------- |
| Repetitive regression tests                  | One-off/exploratory tests                      |
| Stable features (UI unlikely to change soon) | Features still being actively redesigned       |
| Data-driven tests (same steps, many inputs)  | Usability/visual "does this feel right" checks |
| Tests run frequently (every build)           | Rarely-used edge cases with low ROI            |

### 2. Recap: The Automation Pyramid

```
        UI Tests        (few — slow, expensive, brittle)
     API/Service Tests   (some)
   Unit Tests             (many — fast, cheap)
```

Automate low in the pyramid first. Selenium sits at the **UI layer** — powerful, but the slowest and most fragile layer, so it should test _user journeys_, not every possible detail (leave detail-level checks to API/unit tests where possible).

### 3. Basic Automation Framework Concepts

- **Test runner** — executes tests and reports results (e.g., `pytest`, `JUnit`, `TestNG`).
- **Assertions** — the actual pass/fail check (e.g., `assert "inventory" in driver.current_url`).
- **Page Object Model (POM)** — a design pattern where each page's elements/actions are stored in one reusable class, so if the UI changes, you update it in one place instead of every test. (Good to know exists — not required for a first script.)
- **Reporting** — tools like Allure or built-in pytest output show what passed/failed and why.

**Key Takeaway:** Automation isn't "automate everything" — it's choosing the _right_ stable, repetitive tests to automate so humans can focus on exploratory and judgment-based testing.

---

## PART 3: API Testing Foundations

### 1. What is API Testing?

API testing checks the application's logic at the API layer — sending requests directly to the backend (no UI) and verifying the response. It's faster and more stable than UI testing because it skips rendering, clicking, and page loads entirely.

### 2. Common HTTP Methods

| Method   | Purpose                      | Example                      |
| -------- | ---------------------------- | ---------------------------- |
| `GET`    | Retrieve data                | Get a list of users          |
| `POST`   | Create new data              | Register a new user          |
| `PUT`    | Replace/update existing data | Update a user's full profile |
| `PATCH`  | Partially update data        | Update just one field        |
| `DELETE` | Remove data                  | Delete a user                |

### 3. Common HTTP Status Codes

| Code                        | Meaning                                               |
| --------------------------- | ----------------------------------------------------- |
| `200 OK`                    | Request succeeded                                     |
| `201 Created`               | Resource created successfully (typical POST response) |
| `400 Bad Request`           | Client sent invalid data                              |
| `401 Unauthorized`          | Missing/invalid authentication                        |
| `404 Not Found`             | Resource doesn't exist                                |
| `500 Internal Server Error` | Server-side failure                                   |

### 4. Request/Response Structure

A request typically has:

- **Endpoint/URL** — e.g., `https://api.example.com/users/2`
- **Headers** — metadata like `Content-Type: application/json`, auth tokens
- **Body** (for POST/PUT/PATCH) — the data being sent, usually JSON
- **Query parameters** — e.g., `?page=2&limit=10`

A response typically has: a **status code**, **headers**, and a **body** (usually JSON) that the test asserts against.

### 5. What to Assert in an API Test

- **Status code** — did we get the expected code?
- **Response schema/fields** — does the JSON contain the expected keys (e.g., `id`, `email`)?
- **Field values** — does `id` actually equal what we expect?
- **Response time** — did it respond within an acceptable limit?
- **Error handling** — does an invalid request return the correct error code/message?

**Key Takeaway:** API testing applies the same functional/negative/boundary mindset from manual testing — just at the request/response level instead of the UI level, and it's usually faster and more stable to automate than the UI.

---

## PART 4: Tool Landscape — JMeter, Swagger, Postman, SmartBear

| Tool                    | What it's really for                                                                                                                                          | Not for                                                                         |
| ----------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| **Postman**             | Manual & automated **functional** API testing — build requests, save as collections, write assertions (`pm.test`), run entire suites                          | Load/performance testing (possible but not its strength)                        |
| **Swagger / OpenAPI**   | **Documents** an API's endpoints, parameters, and responses (a spec/contract) — often has "Swagger UI" to try endpoints in-browser                            | Not a testing tool by itself — it describes the API rather than validating it   |
| **JMeter**              | **Performance/load testing** — simulates many concurrent users hitting an API or website to measure speed and stability under load                            | Not designed for detailed functional assertions the way Postman is              |
| **SmartBear** (company) | Makes a _suite_ of tools: **SoapUI**/**ReadyAPI** (API testing), **TestComplete** (UI automation, alternative to Selenium), **Zephyr** (test case management) | One name covers several different products — check which specific tool is meant |

**How they fit together in a real project:**
`Swagger` documents what the API should do → `Postman` verifies it actually does that (functional testing) → `JMeter` checks it holds up under load → tools like `Zephyr`/Jira track which test cases have been run and passed.

**Key Takeaway:** Selenium and Postman automate the _checking_; Swagger describes the _contract_ being checked; JMeter checks _performance_ rather than correctness. Together they cover functional, structural, and non-functional testing across both UI and API layers.
