# Software Testing Foundations & Agile Testing — Reference Notes

---

## PART 1: Software Testing Foundations — Fundamentals

### 1. What is Software Testing?

Software testing is the process of evaluating a system or component to determine whether it satisfies specified requirements, and to identify defects before the software reaches the customer.

Testing aims to:

- Verify that the software does what it is supposed to do.
- Find defects before release.
- Provide confidence in the quality of the product.
- Reduce the risk of failure in production.

**Key Point:** Testing does not "prove" software is correct — it reduces risk by finding as many relevant defects as practical, within the time and budget available.

---

### 2. Why Testing is Necessary: Error → Defect → Failure

| Term                   | Meaning                                                                                           |
| ---------------------- | ------------------------------------------------------------------------------------------------- |
| **Error (Mistake)**    | A human action that produces an incorrect result (e.g., a developer misunderstands a requirement) |
| **Defect (Bug/Fault)** | A flaw in the code or design caused by an error                                                   |
| **Failure**            | Actual, observable incorrect behaviour of the system caused by a defect                           |

**Cost of Defects — the earlier a defect is found, the cheaper it is to fix:**

Requirements → Design → Coding → Testing → Production
(Cost of fixing a defect increases sharply at each later stage)

**Key Takeaway:** Early testing (starting from requirements review) saves significant time and cost compared to finding defects after release.

---

### 3. Testing vs. Debugging

| Testing                            | Debugging                                 |
| ---------------------------------- | ----------------------------------------- |
| Finds defects/failures             | Finds the root cause and fixes the defect |
| Performed by testers               | Performed by developers                   |
| Confirms presence of problems      | Removes the problem                       |
| Dynamic/static evaluation activity | Diagnostic + corrective activity          |

---

### 4. The Seven Testing Principles

1. **Testing shows the presence of defects, not their absence** — testing can prove defects exist, but never that none exist.
2. **Exhaustive testing is impossible** — except for trivial cases; testing must be prioritised using risk and focus.
3. **Early testing saves time and money** — test activities should start as early as possible (shift-left).
4. **Defects cluster together** — a small number of modules usually contain most of the defects (Pareto principle).
5. **Pesticide paradox** — repeating the same tests eventually stops finding new defects; tests must be reviewed and varied.
6. **Testing is context-dependent** — testing a banking app differs from testing a mobile game.
7. **Absence-of-errors is a fallacy** — a bug-free system that doesn't meet user needs is still unusable.

---

### 5. The Fundamental Test Process

```
Test Planning → Monitoring & Control → Test Analysis → Test Design →
Test Implementation → Test Execution → Test Completion
```

| Activity             | Purpose                                                               |
| -------------------- | --------------------------------------------------------------------- |
| Planning             | Define objectives, scope, approach, resources                         |
| Monitoring & Control | Track progress vs. plan; take corrective action                       |
| Analysis             | Analyse the test basis to identify what to test (test conditions)     |
| Design               | Turn test conditions into test cases and test data                    |
| Implementation       | Organise test cases into test procedures/scripts; prepare environment |
| Execution            | Run tests, log results, compare actual vs. expected                   |
| Completion           | Report results, archive artefacts, capture lessons learned            |

---

### 6. Test Levels

| Level              | Focus                                        | Performed By          |
| ------------------ | -------------------------------------------- | --------------------- |
| **Unit/Component** | Individual functions/modules                 | Developers            |
| **Integration**    | Interfaces between components/systems        | Developers/Testers    |
| **System**         | End-to-end behaviour of the complete system  | Test team             |
| **Acceptance**     | Whether the system meets business/user needs | Users/Stakeholders/BA |

---

### 7. Test Types

| Type                       | Checks                                                                                       |
| -------------------------- | -------------------------------------------------------------------------------------------- |
| **Functional**             | What the system does (features, business rules)                                              |
| **Non-functional**         | How well it does it (performance, security, usability, reliability)                          |
| **Structural (White-box)** | Internal structure/code paths                                                                |
| **Change-related**         | Confirmation testing (re-testing a fix) and Regression testing (checking nothing else broke) |

---

### 8. Psychology of Testing & Independence

Testers need a mindset of curiosity, professional pessimism, and attention to detail — similar in spirit to a Business Analyst's investigative mindset, but focused on _breaking_ the system to find weaknesses rather than only understanding it.

| Level of Independence                         | Description                                              |
| --------------------------------------------- | -------------------------------------------------------- |
| Tests by the author (lowest)                  | Developer tests own code                                 |
| Tests by another developer                    | Peer testing                                             |
| Tests by a dedicated tester                   | Independent test team                                    |
| Tests by an outside team/specialist (highest) | Independent test organisation, e.g. security specialists |

**Key Takeaway:** Testing exists because humans make errors that become defects, which can cause failures. Following the seven principles and a structured test process helps testers find the most important defects efficiently, as early as possible, within realistic constraints.

---

## PART 2: Software Testing Foundations — Test Techniques

### 1. Categories of Test Techniques

| Category             | Based On                                          | Also Called         |
| -------------------- | ------------------------------------------------- | ------------------- |
| **Black-box**        | Requirements/specifications, no knowledge of code | Specification-based |
| **White-box**        | Internal code structure                           | Structure-based     |
| **Experience-based** | Tester's skill, intuition, and experience         | —                   |

---

### 2. Black-Box Techniques

**a) Equivalence Partitioning (EP)**
Divide input data into partitions where all values are expected to be treated the same way; test one representative value per partition instead of every possible value.
_Example:_ Age field valid 18–65 → partitions: <18 (invalid), 18–65 (valid), >65 (invalid).

**b) Boundary Value Analysis (BVA)**
Most defects occur at the edges of partitions, so test the boundary values directly.
_Example:_ For 18–65 → test 17, 18, 65, 66.

**c) Decision Table Testing**
Used when the system's behaviour depends on a combination of conditions/business rules.

| Condition                 | Rule 1 | Rule 2 | Rule 3 |
| ------------------------- | ------ | ------ | ------ |
| Premium customer?         | Y      | Y      | N      |
| Order > $100?             | Y      | N      | Y      |
| **Action: Free shipping** | Y      | N      | N      |

**d) State Transition Testing**
Used when the system behaves differently depending on its current state and events that trigger changes.
_Example:_ Order status: `Placed → Shipped → Delivered → Returned`. Tests check valid and invalid transitions (e.g., can an order go directly from "Placed" to "Delivered"?).

**e) Use Case Testing**
Tests are derived from real user interactions/business processes end-to-end, including alternative and exception flows.

---

### 3. White-Box Techniques

| Technique                    | Coverage Goal                                                 |
| ---------------------------- | ------------------------------------------------------------- |
| **Statement Coverage**       | Every executable line of code is run at least once            |
| **Decision/Branch Coverage** | Every decision outcome (True/False) of every branch is tested |

Coverage is usually expressed as a percentage:
`Coverage % = (Items exercised / Total items) × 100`

Higher coverage gives more confidence but does not guarantee correctness (relates back to Principle #1 and #2).

---

### 4. Experience-Based Techniques

| Technique                   | Description                                                                                       |
| --------------------------- | ------------------------------------------------------------------------------------------------- |
| **Error Guessing**          | Tester uses experience to anticipate likely defect areas (e.g., empty fields, special characters) |
| **Exploratory Testing**     | Simultaneous learning, test design, and execution — no predefined scripts                         |
| **Checklist-Based Testing** | Tester uses a checklist of items/rules/standards to guide test coverage                           |

---

### 5. Choosing the Right Technique

| Consider                    | Because                                                                  |
| --------------------------- | ------------------------------------------------------------------------ |
| Type of system/risk level   | High-risk features need more rigorous techniques (e.g., decision tables) |
| Regulatory/compliance needs | May require formally documented coverage (white-box)                     |
| Time & budget               | Exploratory/error guessing are fast; formal techniques take longer       |
| Available information       | Well-documented rules → decision tables; unclear areas → exploratory     |
| Team skill/experience       | Experience-based techniques rely on skilled testers                      |

**Key Takeaway:** No single technique finds every defect. Combining black-box (business logic), white-box (code coverage), and experience-based (tester intuition) techniques gives the most thorough and efficient defect detection, chosen according to risk and constraints.

---

## PART 3: Agile Testing

### 1. What is Agile Testing?

Agile testing is a testing practice that follows the principles of Agile software development: testing happens **continuously, collaboratively, and iteratively** throughout the sprint/iteration, rather than as a separate phase at the end.

**Key emphasis:**

- Whole-team responsibility for quality (not just "the testers").
- Frequent, fast feedback.
- Tight collaboration between developers, testers, and Product Owner.
- Automation to support frequent releases.

---

### 2. Core Agile Testing Principles

- **Whole Team Approach** — quality is a shared responsibility of the entire team (dev, tester, PO, BA).
- **Continuous/Early Testing** — testing starts from story refinement, not after coding is "done."
- **Fast Feedback** — short iterations allow defects to be found and fixed quickly.
- **Face-to-Face Communication** — reduces ambiguity compared to heavy documentation.
- **Test Automation** — regression and unit tests are automated to support frequent, reliable releases.
- **Adapting to Change** — tests evolve as requirements evolve across iterations.

---

### 3. Role of the Tester in Agile

The Agile tester:

- Participates in backlog refinement and sprint planning.
- Helps clarify **acceptance criteria** with the Product Owner and BA.
- Writes/executes tests in parallel with development (not only after).
- Performs exploratory testing to complement automated checks.
- Contributes to automation and CI/CD pipelines.
- Acts as a "quality coach," not a gatekeeper.

---

### 4. Agile Testing Quadrants (Brian Marick model)

|                       | Supports the Team                                      | Critiques the Product                                   |
| --------------------- | ------------------------------------------------------ | ------------------------------------------------------- |
| **Technology-facing** | Q1: Unit tests, Component tests (automated)            | Q4: Performance, Security, Reliability (non-functional) |
| **Business-facing**   | Q2: Functional tests, Story tests, Examples/Prototypes | Q3: Exploratory testing, Usability testing, UAT         |

This model helps teams ensure they cover both technical correctness and business value, using both automated and manual/exploratory testing.

---

### 5. Test-First Approaches

| Approach                                      | Focus                                               | Style                                                        |
| --------------------------------------------- | --------------------------------------------------- | ------------------------------------------------------------ |
| **TDD** (Test-Driven Development)             | Unit-level code correctness                         | Write a failing unit test → write code to pass it → refactor |
| **ATDD** (Acceptance Test-Driven Development) | Whole team agrees on acceptance tests before coding | Business/dev/test collaborate on examples upfront            |
| **BDD** (Behaviour-Driven Development)        | Describes behaviour in business language            | `Given / When / Then` scenarios                              |

_Example (BDD style):_

```
Given a customer has $50 in their account
When they try to withdraw $100
Then the transaction should be declined
```

---

### 6. Definition of Ready (DoR) & Definition of Done (DoD)

| Definition of Ready                                                         | Definition of Done                                                                                             |
| --------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| Story is clear, sized, and has acceptance criteria before entering a sprint | Code is written, reviewed, tested (unit + functional), automated where possible, and meets acceptance criteria |
| Ensures the team doesn't start work on vague requirements                   | Ensures a story is truly complete, not just "coded"                                                            |

---

### 7. The Test Automation Pyramid

```
        UI / End-to-End Tests   (few — slow, expensive, brittle)
      Service / Integration Tests (some — moderate speed/cost)
    Unit Tests                    (many — fast, cheap, stable)
```

**Key Point:** Agile teams aim for a large base of fast automated unit tests, a moderate layer of integration tests, and a small number of full end-to-end UI tests — this keeps regression testing fast enough to support frequent releases.

---

### 8. Continuous Testing & CI/CD

- Tests (especially unit and integration) are run automatically on every code commit via a CI/CD pipeline.
- Enables **continuous feedback** — defects are caught within minutes/hours, not weeks.
- Reduces regression risk as the codebase changes frequently.
- Exploratory and usability testing still require human judgement and are done manually alongside automation.

**Key Takeaway:** Agile Testing shifts quality left and spreads it across the whole team. Instead of testing being a final gate before release, it becomes a continuous activity woven into every story, every sprint, and every commit — balancing automated technical checks (Q1/Q4) with business-facing collaboration and exploration (Q2/Q3).

---

## Quick Comparison: Traditional vs. Agile Testing

| Traditional (Predictive)                      | Agile                                                      |
| --------------------------------------------- | ---------------------------------------------------------- |
| Testing is a separate phase after development | Testing happens continuously throughout the sprint         |
| Heavy, formal test documentation              | Lightweight documentation; conversations + automated tests |
| Dedicated test team, late involvement         | Whole-team ownership, testers involved from day one        |
| Long feedback loops                           | Fast, frequent feedback (often within the same day)        |
| Regression testing is manual/periodic         | Regression testing is largely automated and continuous     |
