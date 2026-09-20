# Security Testing & Penetration Testing Fundamentals

---

### 1. What is Security Testing?

Security testing checks whether a system protects data and access as intended — even when someone deliberately tries to misuse it. Unlike functional testing ("does the feature work?"), security testing asks "**can this be broken, bypassed, or abused?**"

| Security Testing                                                | Penetration Testing                                          |
| --------------------------------------------------------------- | ------------------------------------------------------------ |
| Broad, ongoing, often part of the regular QA/dev cycle          | Deep, adversarial, simulates a real attacker                 |
| Checks for known vulnerability classes (often partly automated) | Manual + tool-assisted, tries to actually exploit weaknesses |
| Done by QA/dev teams as part of normal testing                  | Usually done by specialists, periodically or for compliance  |

Think of it as a spectrum: a tester running a few injection/negative test cases (like Week 1's TC11) is doing basic security testing. A dedicated penetration test goes much further — actively trying to chain vulnerabilities together to compromise the system.

---

### 2. Three Approaches: SAST, DAST, IAST

| Type                   | How it works                                                                                             | When it runs                            |
| ---------------------- | -------------------------------------------------------------------------------------------------------- | --------------------------------------- |
| **SAST** (Static)      | Scans source code without running it — finds issues like hardcoded secrets or unsafe functions           | Early (shift-left), during development  |
| **DAST** (Dynamic)     | Attacks the _running_ application from the outside, like a real attacker would — no code access needed   | Later, against a deployed/staging build |
| **IAST** (Interactive) | Instruments the running app to watch what happens internally as it's tested — combines strengths of both | During test execution                   |

---

### 3. OWASP Top 10 (2025) — the industry-standard risk list

OWASP (Open Web Application Security Project) publishes the most widely referenced ranking of web application security risks. The 2025 edition:

| #   | Risk                                  | In plain terms                                                                             |
| --- | ------------------------------------- | ------------------------------------------------------------------------------------------ |
| A01 | Broken Access Control                 | Users can access data/actions they shouldn't be able to (still the #1 risk)                |
| A02 | Security Misconfiguration             | Insecure default settings, exposed cloud/config, weak headers                              |
| A03 | Software Supply Chain Failures        | Compromised dependencies, build systems, or third-party components (new/expanded category) |
| A04 | Cryptographic Failures                | Weak or missing encryption, exposing sensitive data                                        |
| A05 | Injection                             | SQL, command, or script injection (e.g., SQL injection, XSS)                               |
| A06 | Insecure Design                       | Security flaws baked into the architecture itself, not just the code                       |
| A07 | Authentication Failures               | Weak login/session handling (renamed from "Identification and Authentication Failures")    |
| A08 | Software or Data Integrity Failures   | Trusting unverified code/data (e.g., insecure deserialization, unsigned updates)           |
| A09 | Security Logging & Alerting Failures  | Attacks go unnoticed because nothing logs or alerts on them                                |
| A10 | Mishandling of Exceptional Conditions | Poor error handling that leaks info or fails unsafely (new category for 2025)              |

_(This replaces the 2021 list — Broken Access Control stays #1, but Security Misconfiguration jumped from #5 to #2, and two categories are brand new.)_

---

### 4. Types of Penetration Testing (by how much the tester knows)

| Type          | Tester's knowledge                            | Simulates                                                     |
| ------------- | --------------------------------------------- | ------------------------------------------------------------- |
| **Black box** | None — no source code, no credentials         | An outside attacker with zero insider info                    |
| **White box** | Full — source code, architecture, credentials | A deep audit, most thorough                                   |
| **Gray box**  | Partial — e.g., a normal user account         | An insider or an attacker who's already gained limited access |

---

### 5. A Simplified Pen Test Methodology

```
Reconnaissance → Scanning → Exploitation → Post-Exploitation → Reporting
```

- **Reconnaissance** — gather information about the target (technology stack, endpoints, exposed services)
- **Scanning** — identify potential weaknesses (automated tools + manual probing)
- **Exploitation** — actually attempt to break in / trigger the vulnerability
- **Post-Exploitation** — assess what damage/access a real attacker could achieve from there
- **Reporting** — document findings with severity, evidence, and remediation advice (same bug-report skills from Week 3, applied to security findings)

---

### 6. Common Tools

| Tool           | Purpose                                                                                          | Cost                                      |
| -------------- | ------------------------------------------------------------------------------------------------ | ----------------------------------------- |
| **Burp Suite** | The industry-standard tool for intercepting/manipulating web traffic to test for vulnerabilities | Free Community Edition; paid Professional |
| **OWASP ZAP**  | Free, open-source alternative to Burp — automated scanner + manual testing proxy                 | Free                                      |
| **Nmap**       | Network/port scanning — what services/ports are exposed                                          | Free                                      |
| **sqlmap**     | Automates detection and exploitation of SQL injection                                            | Free                                      |

---

### 7. How a QA Tester (Not a Full Pentester) Can Start

You don't need to become a penetration tester to add security awareness to regular testing:

- Try injection-style payloads in input fields as negative test cases (you already did this in Week 1, TC11).
- Check API responses (Week 2) for over-exposed data — does a response leak fields it shouldn't (passwords, internal IDs, stack traces)?
- Verify the site uses HTTPS and cookies are marked `Secure`/`HttpOnly`.
- Check that error messages don't reveal internal details (stack traces, server versions) — ties directly to A10 above.
- Run an automated baseline scan with OWASP ZAP against a _practice_ target and review what it flags.

---

### 8. Legal & Ethical Note

**Only test systems you own, or that are explicitly built for this purpose** (e.g., OWASP Juice Shop, DVWA, PortSwigger's labs). Scanning or attacking a real company's production site without permission — even "just to learn" — can be illegal, regardless of intent. This is the same principle already noted for the Week 1 practice sites.

**Key Takeaway:** Security testing is functional testing's more adversarial sibling — same mindset (try to break it), aimed specifically at protecting access and data rather than just correctness. The OWASP Top 10 is the standard reference for _what_ to look for; SAST/DAST/pen testing are different _ways_ of looking.

