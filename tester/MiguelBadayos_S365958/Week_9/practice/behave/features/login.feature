Feature: SauceDemo Login
  As a user of the SauceDemo store
  I want to log in with my credentials
  So that I can access the product inventory

  Scenario: Valid login succeeds
    Given I am on the SauceDemo login page
    When I log in with username "standard_user" and password "secret_sauce"
    Then I should be redirected to the inventory page

  Scenario: Login fails with wrong password
    Given I am on the SauceDemo login page
    When I log in with username "standard_user" and password "wrongpass"
    Then I should see an error message containing "do not match"

  Scenario: Login fails with empty username
    Given I am on the SauceDemo login page
    When I log in with username "" and password "secret_sauce"
    Then I should see an error message containing "Username is required"
