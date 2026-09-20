# OWASP Juice Shop — target setup

Source cloned for reference at `~/juice-shop` (kept out of this repo — it's a third-party target app, not a deliverable). This folder just holds the script to run it.

## run it

```bash
docker compose up -d
```

Then open http://localhost:3000. Runs the official `bkimminich/juice-shop` image — same one everyone testing against Juice Shop uses.

## stop it

```bash
docker compose down
```
