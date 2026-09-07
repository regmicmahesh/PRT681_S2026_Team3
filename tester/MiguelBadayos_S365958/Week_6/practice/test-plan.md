# Practice Exercise Template — Login Feature Test Plan & Test Cases
**Site under test:** https://www.saucedemo.com/
**Feature under test:** Login

Use this as a worked example. Copy the structure and swap in your own site/feature.

---

## 1. Test Plan

**Objective**
Verify that the SauceDemo login feature correctly authenticates valid users, rejects invalid credentials, handles edge cases gracefully, and behaves consistently across the known test accounts.

**Scope**
- In scope: username/password field validation, login button behaviour, error messaging, session redirect after login, known test-account behaviour.
- Out of scope: checkout flow, cart functionality, performance/load testing.

**Test Items**
- Login form (username field, password field, "Login" button, error message area)

**Approach**
Manual, exploratory + scripted black-box functional testing. Test using equivalence partitioning (valid/invalid credential types) and boundary value analysis (empty fields, max-length input).

**Environment**
- Browser: Chrome (latest)
- URL: https://www.saucedemo.com/
- Test accounts provided by the site: `standard_user`, `locked_out_user`, `problem_user`, `performance_glitch_user`, `error_user`, `visual_user` — all with password `secret_sauce`

**Entry Criteria:** Login page loads successfully.
**Exit Criteria:** All 15 test cases executed and results logged; all found defects logged in tracker.

**Deliverables:** This test plan, test case log with pass/fail results, bug log (min. 3 defects).

---

## 2. Test Cases (15)

| ID | Type | Title | Steps | Test Data | Expected Result |
|----|------|-------|-------|-----------|------------------|
| TC01 | Functional | Valid login | Enter username + password, click Login | `standard_user` / `secret_sauce` | User is redirected to the Products/inventory page |
| TC02 | Functional | Logout returns to login page | Log in, then log out via menu | `standard_user` | User is returned to the login page |
| TC03 | Functional | Locked-out account is blocked | Enter username + password, click Login | `locked_out_user` / `secret_sauce` | Error message: "Sorry, this user has been locked out." |
| TC04 | Functional | Error message clears on retry | Trigger an error, then re-enter valid credentials | invalid → then `standard_user` | Error message disappears once valid login is attempted |
| TC05 | Functional | Field labels/placeholders correct | Load login page | — | Username field shows "Username" placeholder; password field shows "Password" placeholder |
| TC06 | Negative | Wrong password | Enter valid username, wrong password | `standard_user` / `wrongpass` | Error: "Username and password do not match any user in this service" |
| TC07 | Negative | Wrong username | Enter invalid username, valid password | `standard_userX` / `secret_sauce` | Error: "Username and password do not match any user in this service" |
| TC08 | Negative | Empty username | Leave username blank, enter password, click Login | "" / `secret_sauce` | Error: "Username is required" |
| TC09 | Negative | Empty password | Enter username, leave password blank, click Login | `standard_user` / "" | Error: "Password is required" |
| TC10 | Negative | Both fields empty | Click Login with both fields blank | "" / "" | Error: "Username is required" (first field validated) |
| TC11 | Negative | SQL-injection-style input | Enter `' OR '1'='1` in both fields | — | Login rejected; no crash, no data exposure, standard error shown |
| TC12 | Negative | Case sensitivity check | Enter valid username in different case | `Standard_User` / `secret_sauce` | Login should fail if system is case-sensitive (verify actual behaviour) |
| TC13 | Boundary | Very long input string | Enter 500-character string into username field | random 500-char string | Field handles input without crashing; appropriate error shown |
| TC14 | Boundary | Leading/trailing whitespace | Enter `" standard_user "` (with spaces) | with spaces / `secret_sauce` | Verify whether app trims spaces or rejects login |
| TC15 | Boundary | Special characters only | Enter only special characters in both fields | `!@#$%^&*` / `!@#$%^&*` | Login rejected gracefully; no crash or broken UI |

---

## 3. Bug Log Template (log these in Trello or a tracker)

**Suggested Trello board columns:** `To Test` → `In Progress` → `Bug Found` → `Fixed/Closed`
Each bug = one card, with this info in the card description:

| Field | Example Entry 1 | Example Entry 2 | Example Entry 3 |
|---|---|---|---|
| **Bug ID** | BUG-01 | BUG-02 | BUG-03 |
| **Title** | `problem_user` sees mismatched product images | Login form allows case-mismatched username | No client-side error before server round-trip |
| **Related Test Case** | (exploratory, post-login) | TC12 | TC08 |
| **Steps to Reproduce** | 1. Log in as `problem_user`/`secret_sauce`. 2. View Products page. | 1. Enter `Standard_User` / `secret_sauce`. 2. Click Login. | 1. Leave username blank. 2. Click Login immediately. |
| **Expected Result** | Each product shows its own correct image | Login should fail (case-sensitive) or the rule should be documented | Field should show inline validation before hitting server |
| **Actual Result** | All products display the same image | Behaviour is inconsistent/undocumented | Error appears, but noticeably after a network delay |
| **Severity** | Medium (visual/functional, misleads users) | Low–Medium (security/consistency concern) | Low (UX polish) |
| **Environment** | Chrome, saucedemo.com | Chrome, saucedemo.com | Chrome, saucedemo.com |

> Note: `problem_user` is a real, reproducible SauceDemo bug — great as one of your 3 "intentional" bugs since you can point to it consistently. The other two can come from your own boundary/negative testing (TC08–TC15 area is usually where you'll find genuine inconsistencies).

---

## 4. How to Run This in ~1–2 Hours

1. **15 min** — Write/adjust the test plan for your chosen site/feature.
2. **45 min** — Execute the 15 test cases, marking Pass/Fail as you go (a simple spreadsheet or the table above works).
3. **20 min** — For any Fail (or odd behaviour worth flagging), write it up as a bug card.
4. **10 min** — Set up your Trello board (or use a free template like "Bug Tracking" in Trello's template gallery) and add your 3 bug cards.
