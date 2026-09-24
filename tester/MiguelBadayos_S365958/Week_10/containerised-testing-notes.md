# Week 5 Notes: Containerised Test Automation & CI/CD Gating

_Note: BDD (Cucumber/SpecFlow) and Page Object Model were already covered in the Week 4 notes — not repeated here. GitHub Actions is the CI/CD tool already in use since Week 3; this week extends it rather than switching to Azure DevOps (same reasoning as sticking with `behave` instead of chasing "real Cucumber" — the existing tool already does the job)._

---

### 1. Why Selenium Grid?

Running Selenium tests against a single local browser doesn't tell you whether the app actually works the same way across browsers, and running everything sequentially on one machine doesn't scale as the suite grows. **Selenium Grid** solves both: it's a hub-and-node setup that lets tests run against multiple real browsers, potentially in parallel, without each browser being installed on the machine running the tests.

| Component | Role                                                                                                                            |
| --------- | ------------------------------------------------------------------------------------------------------------------------------- |
| **Hub**   | The entry point tests connect to. Receives a test's requested browser/capabilities and routes the session to an available node. |
| **Node**  | A container running one specific browser (e.g., Chrome or Firefox) that actually executes the session.                          |

Your test code doesn't talk to Chrome or Firefox directly anymore, it talks to the **hub**, using `webdriver.Remote(...)` instead of `webdriver.Chrome(...)`, and specifies which browser it wants via options.

### 2. Why Docker for This?

Installing multiple real browsers (plus matching driver versions) on every machine that runs tests is exactly the kind of environment inconsistency Docker exists to remove. Official Selenium images (`selenium/hub`, `selenium/node-chrome`, `selenium/node-firefox`) come with the browser and matching driver pre-installed and version-matched, so "works on my machine" stops being a factor.

### 3. Parallel Execution vs. Multi-Browser — two different things

- **Multi-browser**: running the _same_ test against different browsers (Chrome vs. Firefox) to catch browser-specific bugs. Grid enables this by letting you request different node types.
- **Parallel execution**: running _multiple tests at the same time_ to finish the suite faster. Grid enables this (multiple nodes can run sessions concurrently), but your test runner also needs to actually dispatch tests concurrently (e.g., `pytest-xdist` with `-n auto`) — Grid alone doesn't parallelise your test runner for you.

### 4. CI/CD Gating — "failing the build on regression"

This is less a new tool and more a property that's often _already true_ by default: most CI systems (GitHub Actions included) treat a non-zero exit code from a test command as a failed step, which fails the job, which shows as a red ❌ build. If `pytest` finds a failing test, it exits non-zero automatically, no special "gate" configuration is required to get basic fail-on-regression behaviour.

What _is_ worth deliberately setting up:

- Making sure the pipeline actually **runs on every push** (already true since Week 3).
- Making sure a failure is **visible and blocking** (e.g., required status checks on a pull request, if using PRs) rather than just quietly failing somewhere no one looks.
- Confirming this behaviour with a real test, not just assuming it: deliberately break a test once, confirm the pipeline goes red, then fix it.

### 5. Synthetic Transaction Monitoring (concept only)

A production monitoring technique where a scripted "fake user" (often literally a Selenium script) periodically performs a critical action (e.g., "log in", "complete checkout") against the _live_ production site on a schedule, alerting the team if it ever fails. It's the same automation skill as your test suite, aimed at production monitoring rather than pre-release testing. Not part of this week's hands-on practice, worth knowing the term exists and where it fits.

### 6. Automated Test Reporting (concept only)

Tools like **Allure** or **ExtentReports** turn raw pass/fail test output into a readable HTML report (trends over time, screenshots on failure, categorised failures). Not required for this week's practice, but worth knowing these exist as the next step up from reading raw `pytest -v` console output.

**Key Takeaway:** Selenium Grid decouples "what browser is this test running in" from "what machine is running the test," Docker makes that reproducible, and CI failing on regression is mostly about _proving_ behaviour that already exists rather than building something new from scratch.
