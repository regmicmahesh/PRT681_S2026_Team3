-- SQL Essential Training practice queries
-- Data: Superstore_Report_YuanLi_S390310.xlsx (Data sheet), loaded into SQLite

-- 1_select_where
SELECT order_id, customer_name, category, sales
FROM Orders
WHERE category = 'Technology' AND sales > 500
ORDER BY sales DESC
LIMIT 5;

-- 2_group_by_category
SELECT category, ROUND(SUM(sales), 2) AS total_sales
FROM Orders
GROUP BY category
ORDER BY total_sales DESC;

-- 3_group_by_region_having
SELECT region, ROUND(SUM(sales),2) AS total_sales, ROUND(SUM(profit),2) AS total_profit
FROM Orders
GROUP BY region
HAVING SUM(profit) > 2000
ORDER BY total_sales DESC;

-- 4_join_regional_managers
SELECT o.region, rm.manager_name, ROUND(SUM(o.sales),2) AS total_sales
FROM Orders o
JOIN Regional_Managers rm ON o.region = rm.region
GROUP BY o.region, rm.manager_name
ORDER BY total_sales DESC;

-- 5_subquery_top_customers
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

