from behave import use_step_matcher, given, when, then
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from pages.login_page import LoginPage

# The default "parse" matcher requires {username} to match at least one
# character, so it can't express the empty-username scenario. Switching to
# the regex matcher lets the capture groups match an empty string.
use_step_matcher("re")


@given('I am on the SauceDemo login page')
def step_open_login_page(context):
    options = Options()
    options.add_argument("--headless=new")
    options.add_argument("--window-size=1920,1080")
    context.driver = webdriver.Chrome(options=options)
    context.login_page = LoginPage(context.driver)
    context.login_page.load()


@when('I log in with username "(?P<username>.*)" and password "(?P<password>.*)"')
def step_login(context, username, password):
    context.login_page.login(username, password)


@then('I should be redirected to the inventory page')
def step_check_redirect(context):
    assert context.login_page.is_logged_in(), "Expected redirect to inventory page"


@then('I should see an error message containing "(?P<expected_text>.*)"')
def step_check_error(context, expected_text):
    actual = context.login_page.get_error_text()
    assert expected_text.lower() in actual.lower(), f"Expected '{expected_text}' in '{actual}'"
