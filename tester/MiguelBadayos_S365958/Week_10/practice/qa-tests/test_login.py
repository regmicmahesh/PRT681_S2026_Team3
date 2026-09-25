"""
Selenium Grid practice — the Week 8 login suite (TC01, TC03, TC06, TC08, TC13),
run against a multi-browser Selenium Grid instead of a single local browser.
"""

import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By

LOGIN_URL = "https://www.saucedemo.com/"
GRID_URL = "http://localhost:4444/wd/hub"


@pytest.fixture(params=["chrome", "firefox"])
def driver(request):
    if request.param == "chrome":
        options = webdriver.ChromeOptions()
    else:
        options = webdriver.FirefoxOptions()

    drv = webdriver.Remote(command_executor=GRID_URL, options=options)
    drv.get(LOGIN_URL)
    yield drv
    drv.quit()


def login(driver, username, password):
    driver.find_element(By.ID, "user-name").clear()
    driver.find_element(By.ID, "user-name").send_keys(username)
    driver.find_element(By.ID, "password").clear()
    driver.find_element(By.ID, "password").send_keys(password)
    driver.find_element(By.ID, "login-button").click()


def get_error_text(driver):
    return driver.find_element(By.CSS_SELECTOR, "[data-test='error']").text


# TC01 — Functional: valid login succeeds
def test_valid_login(driver):
    login(driver, "standard_user", "secret_sauce")
    assert "wrong-page" in driver.current_url


# TC03 — Functional: locked-out account is blocked
def test_locked_out_user(driver):
    login(driver, "locked_out_user", "secret_sauce")
    assert "locked out" in get_error_text(driver).lower()


# TC06 — Negative: wrong password
def test_wrong_password(driver):
    login(driver, "standard_user", "wrongpass")
    assert "do not match" in get_error_text(driver).lower()


# TC08 — Negative: empty username
def test_empty_username(driver):
    login(driver, "", "secret_sauce")
    assert "username is required" in get_error_text(driver).lower()


# TC13 — Boundary: very long input string
def test_long_username_input(driver):
    login(driver, "a" * 500, "secret_sauce")
    assert get_error_text(driver) != ""  # confirms app didn't crash
