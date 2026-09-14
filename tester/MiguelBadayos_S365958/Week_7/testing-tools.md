# Tools and Short Explanation — Testing Tools

## 1. Selenium (WebDriver)

Selenium is an open-source framework used to automate web browsers for
functional and regression testing. I would use it to write automated test
scripts that simulate user actions such as clicking, typing, and navigating, and
to run these tests repeatedly across builds.

**Main purpose:** _automating browser-based functional testing._

## 2. Postman

Postman is an API development and testing tool. I would use it to build and send
HTTP requests, validate responses, write automated assertions, and organise
requests into reusable collections.

**Main purpose:** _manual and automated API testing._

## 3. Swagger / OpenAPI

Swagger (based on the OpenAPI Specification) is a tool for documenting and
exploring APIs. I would use it to understand an API's available endpoints,
parameters, and expected responses before writing tests against it.

**Main purpose:** _API documentation and exploration._

## 4. Apache JMeter

JMeter is an open-source tool for performance and load testing. I would use it
to simulate multiple concurrent users hitting an application or API, to measure
response times and identify performance bottlenecks.

**Main purpose:** _performance and load testing._

## 5. SmartBear ReadyAPI / SoapUI

ReadyAPI (and its free counterpart SoapUI) are SmartBear tools for functional
and load testing of APIs, including SOAP and REST services. I would use them for
more advanced API test scenarios than Postman covers, particularly in enterprise
settings.

**Main purpose:** _enterprise-level API testing._

## 6. OWASP ZAP

ZAP (Zed Attack Proxy) is a free, open-source security testing tool. I would use
it to run automated scans against a web application to identify common
vulnerabilities such as missing security headers or injection flaws.

**Main purpose:** _automated web application security scanning._

## 7. Burp Suite

Burp Suite is the industry-standard tool for manual web security testing. I
would use it to intercept and modify HTTP requests/responses to test how an
application handles unexpected or malicious input.

**Main purpose:** _manual security / penetration testing._

## 8. TestRail

TestRail is a dedicated test case management tool. I would use it to organise
test cases, track execution status across test runs, and report on test
coverage.

**Main purpose:** _test case management and reporting._

## 9. Xray for Jira

Xray is a test management add-on for Jira. I would use it to link test cases and
test executions directly to Jira user stories and bugs, keeping requirements and
testing traceable in one place.

**Main purpose:** _test management integrated with Jira._

## 10. Cucumber (Gherkin/BDD)

Cucumber is a tool that allows tests to be written in plain-language
"Given/When/Then" syntax (Gherkin). I would use it so business stakeholders,
developers, and testers can all read and agree on the same test scenarios.

**Main purpose:** _behaviour-driven test specification._

## 11. pytest / JUnit / TestNG

These are test runner frameworks used to organise, execute, and report on
automated tests (pytest for Python, JUnit/TestNG for Java). I would use them to
structure test scripts, run them in bulk, and generate pass/fail reports.

**Main purpose:** _organising and executing automated test suites._

## 12. Jenkins

Jenkins is a self-hosted automation server used for CI/CD. I would use it to
automatically build and run a test suite whenever code changes, without needing
to trigger it manually.

**Main purpose:** _continuous integration / continuous delivery._

## 13. GitHub Actions

GitHub Actions is CI/CD built directly into GitHub. I would use it to
automatically run test suites (e.g. Selenium or API tests) whenever code is
pushed to a repository, without maintaining a separate server.

**Main purpose:** _automated CI/CD pipelines within GitHub._

## 14. Docker

Docker is a containerisation platform. I would use it to run pre-packaged
applications (e.g. a practice target like OWASP Juice Shop) or consistent test
environments without manually installing dependencies.

**Main purpose:** _consistent, portable environments for testing._
