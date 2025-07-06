Feature: Database Operations
  Validate basic CRUD operations on the Users table using SpecFlow and C#

  @insert
  Scenario: Insert a new user into the database
    Given I have user data with FirstName "John", LastName "Doe", and Email "john@example.com"
    When I insert the user into the database
    Then the user should be present in the database

  @update
  Scenario: Update an existing user in the database
    Given I fetch user with Id 1
    When I update the LastName to "Smith"
    Then the updated LastName should be "Smith"

  @delete
  Scenario: Delete a user from the database
    Given I fetch user with Id 2
    When I delete the user
    Then the user should not exist in the database

  @getall
  Scenario: Get all users from the database
    When I fetch all users
    Then the list of users should not be empty
