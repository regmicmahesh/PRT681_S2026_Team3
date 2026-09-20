# Extras — Security Testing Practice Plan

## 1. Set Up the Target (OWASP Juice Shop)

OWASP Juice Shop is a deliberately vulnerable e-commerce app, maintained by OWASP specifically for this kind of practice — legal and safe to attack because it's designed to be broken.

```bash
docker run --rm -p 3000:3000 bkimminich/juice-shop
```

Then open `http://localhost:3000` in your browser.

_(No Docker? Alternative: `npm install -g juice-shop-ctf-cli` route, or just install Docker Desktop first — it's the simplest path.)_

---

## 2. Manual Exploration (~20 min)

Try these categories of input directly in the app — same mindset as Week 6's TC11 (SQL-injection-style input), just applied more deliberately:

| Area to try                    | What to attempt                                                         | What you're looking for                                                                                                      |
| ------------------------------ | ----------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| Login form                     | Enter `' OR 1=1--` as the email, any password                           | Does it log you in without valid credentials? (SQL injection)                                                                |
| Search bar                     | Enter `<iframe src="javascript:alert('test')">`                         | Does a popup/alert fire? (Reflected XSS — the input wasn't sanitized)                                                        |
| Product review / feedback form | Submit unusual input (very long strings, HTML tags, special characters) | Does the app crash, misrender, or reflect the input unescaped?                                                               |
| Any error you can trigger      | Look at the error message shown                                         | Does it reveal a stack trace, file path, or internal framework detail? (Ties to A10 – Mishandling of Exceptional Conditions) |

You don't need to "win" every challenge — the goal is recognizing _what_ a vulnerability looks like when you trigger one, not achieving a full exploit chain.

---

## 3. Automated Scan (~15 min)

1. Install **OWASP ZAP** (free) — [zaproxy.org](https://www.zaproxy.org/download/).
2. Open ZAP → **Automated Scan** → target URL: `http://localhost:3000` → Attack.
3. Once it finishes, review the **Alerts** panel — ZAP will list issues by severity (e.g., missing security headers, cookie flags, etc.).

Take a screenshot of the alerts panel for your `results` folder, same pattern as previous weeks.

---

## 4. Log 3 Findings

Use whichever tracker you've already got running (Jira from Week 8, or the Trello/Excel bug log from Week 6) — tag these with a label like `security` or `extras` so they're distinguishable from functional bugs.

| Field                  | Example                                                                                             |
| ---------------------- | --------------------------------------------------------------------------------------------------- |
| **Title**              | Login form accepts SQL injection payload as valid credentials                                       |
| **Steps to Reproduce** | 1. Go to login page. 2. Enter `' OR 1=1--` as email, any password. 3. Click Login.                  |
| **Expected Result**    | Login rejected — invalid email format / no matching user                                            |
| **Actual Result**      | Logged in successfully, bypassing authentication                                                    |
| **Severity**           | Critical (authentication bypass)                                                                    |
| **Priority**           | (Set based on your own judgment — this is a training app, so treat it as if it were a real finding) |

Repeat for 2 more findings — one from your manual exploration, one from the ZAP automated scan.

---

## Suggested Folder Structure (matching your existing repo pattern)

```
Extras/
└── security-testing/
    ├── security-testing-and-pentesting-notes.md   (the notes doc)
    ├── practice-plan.md                            (this file)
    └── results/
        ├── zap-scan-results.png
        └── findings-log.png (or a screenshot of the Jira/Trello cards)
```

