# SQL Essential Training — Practice Notes

**Yuan Li — S390310 — Report Writer role**
**Course:** SQL Essential Training (Role-Based Task, Week 1 — Reporting Fundamentals: Excel / SQL / Power BI)

## Course summary

Completed the SQL Essential Training course covering core querying skills used for
reporting and data analysis: `SELECT` statements, filtering with `WHERE`, sorting
with `ORDER BY`, aggregate functions (`SUM`, `COUNT`, `AVG`), grouping with
`GROUP BY` / `HAVING`, table joins (`JOIN ... ON`), and subqueries.

## Hands-on practice

To make the practice concrete, I loaded the same Superstore dataset used for the
Excel reporting exercise (`Superstore_Report_YuanLi_S390310.xlsx`, 220 orders) into
a small SQLite database and wrote queries answering similar business questions —
this let me compare the SQL results against the Excel PivotTable-style results as
a sanity check.

Tables used:
- `Orders` — one row per order (customer, region, category, sales, profit, etc.), loaded from the Excel `Data` sheet
- `Regional_Managers` — a small lookup table (region → manager name) created to practice `JOIN`

### 1. SELECT + WHERE + ORDER BY

Find high-value Technology orders.

```sql
SELECT order_id, customer_name, category, sales
FROM Orders
WHERE category = 'Technology' AND sales > 500
ORDER BY sales DESC
LIMIT 5;
```

| order_id | customer_name | category | sales |
|---|---|---|---|
| US-2025-100188 | Quentin Ross | Technology | 3740.06 |
| US-2025-100064 | Olivia James | Technology | 3044.81 |
| US-2025-100184 | Ryan Cole | Technology | 2989.94 |
| US-2025-100011 | Diego Ramirez | Technology | 2345.46 |
| US-2025-100141 | Frank Ocean | Technology | 2285.17 |

### 2. Aggregate + GROUP BY

Total sales by category (matches Q1 in the Excel report).

```sql
SELECT category, ROUND(SUM(sales), 2) AS total_sales
FROM Orders
GROUP BY category
ORDER BY total_sales DESC;
```

| category | total_sales |
|---|---|
| Technology | 71222.30 |
| Furniture | 48273.83 |
| Office Supplies | 11855.33 |

### 3. GROUP BY + HAVING

Regions where total profit exceeds $2,000.

```sql
SELECT region, ROUND(SUM(sales),2) AS total_sales, ROUND(SUM(profit),2) AS total_profit
FROM Orders
GROUP BY region
HAVING SUM(profit) > 2000
ORDER BY total_sales DESC;
```

| region | total_sales | total_profit |
|---|---|---|
| East | 38588.07 | 3270.85 |
| South | 38288.73 | 3037.84 |
| Central | 25943.95 | 2681.27 |

(West is excluded — its profit is below the $2,000 threshold, which lines up with
the Excel report showing West as the weakest-performing region.)

### 4. JOIN

Joining `Orders` to the `Regional_Managers` lookup table.

```sql
SELECT o.region, rm.manager_name, ROUND(SUM(o.sales),2) AS total_sales
FROM Orders o
JOIN Regional_Managers rm ON o.region = rm.region
GROUP BY o.region, rm.manager_name
ORDER BY total_sales DESC;
```

| region | manager_name | total_sales |
|---|---|---|
| East | Grace Liu | 38588.07 |
| South | Mark Anderson | 38288.73 |
| West | Daniel Osei | 28530.71 |
| Central | Priya Menon | 25943.95 |

### 5. Subquery

Customers whose total sales are above the average customer's total sales.

```sql
SELECT customer_name, ROUND(SUM(sales),2) AS total_sales
FROM Orders
GROUP BY customer_name
HAVING SUM(sales) > (
    SELECT AVG(cust_total) FROM (
        SELECT SUM(sales) AS cust_total FROM Orders GROUP BY customer_name
    )
)
ORDER BY total_sales DESC
LIMIT 5;
```

| customer_name | total_sales |
|---|---|
| Maya Patel | 12136.86 |
| Ryan Cole | 12006.52 |
| Olivia James | 11856.45 |
| Diego Ramirez | 11018.14 |
| Harold Pham | 9205.70 |

This top-5 list matches the Q4 "Top 5 customers" ranking from the Excel report
exactly, which cross-checks both pieces of work against each other.

## Key takeaways

- `WHERE` filters rows before grouping; `HAVING` filters groups after aggregation — easy to mix up at first.
- `JOIN ... ON` behaves like Excel's `VLOOKUP`/`INDEX-MATCH`, but works in both directions and can join on multiple keys.
- Subqueries are useful for comparing a group against an overall average, similar to how the Excel report used `LARGE()`/`INDEX()`/`MATCH()` for ranking.
- Practicing SQL against the same dataset as the Excel report was a good way to double-check both — the numbers matched, which gave more confidence in the results.
