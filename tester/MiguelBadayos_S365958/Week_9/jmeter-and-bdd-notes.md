# Week 4 Notes: Performance Testing (JMeter) & BDD Testing (Cucumber/Gherkin)

_Note: the "Security & Vulnerability Testing Fundamentals (OWASP Top 10 / OWASP ZAP)" course listed for this week is already covered by the Extras work — see `bonus_security_testing_and_pentesting_notes.md` and `extras_security_testing_practice_plan.md`. No need to duplicate that here._

---

## PART 1: Performance & Load Testing with Apache JMeter

### 1. What is Performance/Load Testing?

Where functional testing asks "does it work?", performance testing asks **"does it still work well when many people use it at once?"** JMeter simulates many simultaneous users hitting an application or API and measures how it holds up.

### 2. Key Concepts

| Term                             | Meaning                                                                                                                                                       |
| -------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Virtual User (Thread)**        | One simulated user. JMeter runs many of these in parallel to mimic real traffic.                                                                              |
| **Concurrency**                  | How many virtual users are active at the same time.                                                                                                           |
| **Ramp-up Time**                 | How long JMeter takes to start _all_ virtual users. A 50-user test with a 10-second ramp-up starts 5 new users per second, rather than firing all 50 at once. |
| **Load Profiling**               | Deciding a realistic pattern of load (e.g., steady load vs. a sudden spike) to test against.                                                                  |
| **Throughput**                   | Requests processed per unit of time (e.g., requests/second) — a measure of capacity.                                                                          |
| **Response Latency**             | How long each individual request takes to come back — a measure of user-facing speed.                                                                         |
| **Parameterized Stress Testing** | Feeding different input data (e.g., from a CSV file) into each virtual user's requests, rather than every user sending an identical request.                  |

**Throughput vs. Latency — why both matter:** A system can have high throughput but poor latency (handles a lot of traffic, but each individual user waits a while), or the reverse. Neither number alone tells the full story.

### 3. JMeter's Core Building Blocks

```
Test Plan
 └── Thread Group (defines: how many users, ramp-up time, how many loops)
      └── Sampler (the actual request, e.g., HTTP Request)
      └── Listener (where results are shown/recorded, e.g., Summary Report)
```

| Element                                                | Purpose                                                                     |
| ------------------------------------------------------ | --------------------------------------------------------------------------- |
| **Thread Group**                                       | Configures the virtual users: number of threads, ramp-up period, loop count |
| **Sampler** (e.g., HTTP Request)                       | The actual action being performed — e.g., a GET request to an endpoint      |
| **Listener** (e.g., Summary Report, View Results Tree) | Displays/records the results: response times, error rates, throughput       |

### 4. Key Metrics to Read From Results

| Metric                                | What it tells you                                               |
| ------------------------------------- | --------------------------------------------------------------- |
| **# Samples**                         | How many requests were actually sent                            |
| **Average / Min / Max response time** | Speed of responses, and how much they vary                      |
| **Error %**                           | Percentage of requests that failed (timeouts, 5xx errors, etc.) |
| **Throughput**                        | Requests per second the system actually handled                 |

**Key Takeaway:** JMeter doesn't test _correctness_ (that's functional testing) — it tests _capacity and speed under load_. A feature can pass every functional test and still fail under load if response times or error rates spike as concurrency increases.

---

## PART 2: BDD Testing with Cucumber & Gherkin

### 1. What is Behaviour-Driven Development (BDD)?

BDD is a way of writing tests in plain, structured language that both technical and non-technical people (developers, testers, product owners) can read and agree on — closing the gap between "what the business wants" and "what the tests check."

### 2. Gherkin Syntax

Gherkin is the plain-language format BDD scenarios are written in:

```gherkin
Feature: Login
  Scenario: Valid login succeeds
    Given I am on the login page
    When I log in with valid credentials
    Then I should be redirected to the dashboard
```

| Keyword       | Role                                      |
| ------------- | ----------------------------------------- |
| **Feature**   | The overall functionality being described |
| **Scenario**  | One specific example/test case            |
| **Given**     | The starting state/context                |
| **When**      | The action being taken                    |
| **Then**      | The expected outcome                      |
| **And / But** | Extends the previous step                 |

### 3. Cucumber vs. behave

**Cucumber** is the original BDD tool (Java/Ruby-native) that popularized Gherkin. Since your existing automation is in Python, the direct equivalent tool is **behave** — it reads the exact same Gherkin syntax and connects each line to Python code.

|               | Cucumber             | behave                                           |
| ------------- | -------------------- | ------------------------------------------------ |
| Language      | Java / Ruby / JS     | Python                                           |
| Feature files | `.feature` (Gherkin) | `.feature` (same Gherkin)                        |
| Glue code     | "Step Definitions"   | "Step Definitions" (same concept, Python syntax) |

### 4. How the Pieces Connect

```
.feature file (Gherkin, plain English)
        ↓ matched by
step_definitions (Python functions using @given/@when/@then)
        ↓ calls into
Page Object classes (the actual Selenium interactions)
```

### 5. Page Object Model (POM) — quick recap

Instead of writing raw Selenium calls (`find_element`, `click`, etc.) directly inside test/step code, POM puts all of a page's elements and actions into one reusable class. If the login page's HTML changes, you update `LoginPage` once — not every test that touches login.

**Key Takeaway:** BDD doesn't replace Selenium, it sits on top of it. Gherkin describes _what_ should happen in language stakeholders can read; step definitions translate that into _how_ it happens by calling into Page Object classes that do the actual browser automation.
