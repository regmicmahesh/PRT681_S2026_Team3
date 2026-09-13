# Week 3 Notes: CI/CD & Test Management

---

## PART 1: CI/CD Fundamentals (Jenkins & GitHub Actions)

### 1. What is CI/CD?

| Term | Meaning |
|---|---|
| **Continuous Integration (CI)** | Code changes are merged and automatically built/tested frequently (often on every push), catching integration problems early |
| **Continuous Delivery (CD)** | Code that passes CI is automatically prepared for release (but a human still approves the final deploy) |
| **Continuous Deployment (CD)** | Code that passes CI is automatically released to production with no manual step |

**Why it matters for testers:** CI/CD is what makes "continuous testing" (from Week 2's Agile Testing notes) actually possible — instead of running your Selenium/API tests manually, a pipeline runs them automatically every time code changes, and tells the team immediately if something broke.

```
Developer pushes code → Pipeline triggers → Install deps → Run tests → Report pass/fail
```

---

### 2. Jenkins

Jenkins is a free, open-source automation server — one of the oldest and most widely used CI/CD tools, especially common in on-premise/enterprise environments.

| Concept | Description |
|---|---|
| **Job/Project** | A defined task Jenkins can run (e.g., "run test suite") |
| **Pipeline** | A series of stages (Build → Test → Deploy) defined in code, usually a `Jenkinsfile` (written in Groovy) |
| **Agent/Node** | A machine that actually executes the pipeline steps |
| **Plugins** | Jenkins' ecosystem — plugins add support for almost anything (Git, Docker, Slack notifications, test reporting) |

Jenkins requires you to host/maintain the server yourself (or use a managed instance) — this is its main trade-off against GitHub Actions.

---

### 3. GitHub Actions

GitHub Actions is CI/CD built directly into GitHub — no separate server to install. This is what you'll use for the Week 3 practice.

| Concept | Description |
|---|---|
| **Workflow** | A YAML file in `.github/workflows/` describing what to run and when |
| **Trigger (`on:`)** | What causes the workflow to run — e.g., `push`, `pull_request`, `schedule` |
| **Job** | A set of steps that run on a fresh virtual machine ("runner") |
| **Step** | A single action — install Python, run a script, upload a report |
| **Runner** | The virtual machine the job executes on (e.g., `ubuntu-latest`) — free for public repos |
| **Marketplace Actions** | Reusable pre-built steps anyone can use, e.g. `actions/checkout`, `actions/setup-python` |

### 4. Jenkins vs. GitHub Actions

| | Jenkins | GitHub Actions |
|---|---|---|
| Hosting | Self-hosted (you maintain the server) | Fully hosted by GitHub |
| Setup effort | Higher — install, configure, secure | Lower — just add a YAML file to your repo |
| Config language | Groovy (`Jenkinsfile`) | YAML |
| Best for | Enterprises with existing infrastructure/complex pipelines | Fast setup, open-source projects, small teams |

**Key Takeaway:** Both tools do the same fundamental job — run your tests automatically. Jenkins gives more control at the cost of setup/maintenance; GitHub Actions trades some flexibility for near-zero setup, which is why it's the better starting point for this practice.

---

## PART 2: Jira Essentials

### 1. Jira vs. Trello

| Trello | Jira |
|---|---|
| Simple cards on a board | Structured "issues" with defined types, fields, and workflows |
| Great for small/simple tracking | Built specifically for software teams — sprints, backlogs, releases |
| Minimal setup | More configuration, but far more reporting/traceability power |

Jira is the industry-standard tool most real QA/dev teams use daily — this is why it's worth learning even though Trello was "enough" for Week 1.

### 2. Issue Types

| Type | Use |
|---|---|
| **Epic** | A large body of work, made up of several stories |
| **Story** | A feature or piece of user-facing functionality |
| **Task** | A general to-do item |
| **Bug** | A defect — this is the one you'll use most this week |
| **Sub-task** | A smaller piece of a Story/Task/Bug |

### 3. Key Fields for Bug Tracking

| Field | Purpose |
|---|---|
| **Priority** | How urgently it should be fixed (Highest → Lowest) — a business/scheduling decision |
| **Severity** | How badly it breaks the system (Blocker/Critical/Major/Minor/Trivial) — a technical/impact fact. *(Not built in by default — often added as a custom field.)* |
| **Status** | Where it is in the workflow (To Do / In Progress / Done, customizable) |
| **Assignee / Reporter** | Who's fixing it / who found it |

### 4. Boards

| Board Type | Best For |
|---|---|
| **Kanban** | Continuous flow of work, no fixed time boxes — simplest to start with |
| **Scrum** | Work planned and delivered in fixed-length sprints |

**Jira's free plan** supports up to 10 users with unlimited projects/issues, Scrum and Kanban boards, and a backlog — more than enough for this practice.

**Key Takeaway:** Jira separates *what type of work this is* (issue type), *how bad it is* (severity), and *how soon it needs fixing* (priority) — giving much more structure than a Trello card, which is exactly why real teams rely on it for defect tracking at scale.

---

## PART 3: Bug Reports & Test Cases

### 1. Anatomy of a Good Bug Report

| Element | Why it matters |
|---|---|
| **Title** | Short, specific, and searchable — "Login fails" is bad; "Login rejects valid credentials for locked_out_user with wrong error message" is good |
| **Steps to Reproduce** | Numbered, exact steps — anyone should be able to follow them and see the same bug |
| **Expected Result** | What *should* happen |
| **Actual Result** | What *actually* happened |
| **Severity** | Technical impact (does it crash the app? corrupt data? or just look slightly off?) |
| **Priority** | How urgently the business wants it fixed |
| **Environment** | Browser, OS, build/version — bugs are sometimes environment-specific |
| **Evidence** | Screenshot, log, or video — removes ambiguity |

### 2. Severity vs. Priority — the classic distinction

These are *not* the same thing, and mixing them up is one of the most common mistakes new testers make:

| | Severity | Priority |
|---|---|---|
| Measures | Technical impact on the system | Business urgency to fix |
| Set by | Usually the tester | Usually the product owner/manager |
| Example | A typo in the footer = **Low severity** | ...but if it's on the homepage before a big client demo = **High priority** |
| Example 2 | A crash on a rarely-used admin page = **High severity** | ...but if almost no one uses that page = **Low priority** |

A bug can be high severity + low priority, or low severity + high priority — they move independently.

### 3. Good Test Case Structure (recap from Week 1, now feeding Jira/traceability)

| Field | Example |
|---|---|
| **ID** | TC01 |
| **Title** | Valid login succeeds |
| **Preconditions** | User account exists and is active |
| **Steps** | 1. Go to login page. 2. Enter valid username/password. 3. Click Login. |
| **Expected Result** | Redirected to inventory/dashboard page |
| **Linked Bug (if failed)** | e.g., BUG-04 |

**Key Takeaway:** A well-written bug report removes back-and-forth ("what browser were you using?" "can you reproduce it?") by including everything a developer needs the first time. Severity and Priority are two separate judgment calls, not one field — and Jira is built to track exactly that separation across an entire project.
