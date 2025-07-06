using System;
using System.Linq;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseManager.Models;
using DatabaseManager.Repositories;
using System.Collections.Generic;

namespace StepDefinitions
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly UserRepository _repo = new UserRepository();
        private User _user;
        private List<User> _allUsers;

        [Given(@"I have user data with FirstName ""(.*)"", LastName ""(.*)"", and Email ""(.*)""")]
        public void GivenIHaveUserData(string firstName, string lastName, string email)
        {
            _user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }

        [When(@"I insert the user into the database")]
        public void WhenIInsertUserIntoDatabase()
        {
            _repo.InsertUser(_user);
        }

        [Then(@"the user should be present in the database")]
        public void ThenUserShouldBePresentInDatabase()
        {
            var users = _repo.GetAllUsers();
            var found = users.Any(u => u.Email == _user.Email);
            Assert.IsTrue(found, "Inserted user was not found.");
        }

        [Given(@"I fetch user with Id (.*)")]
        public void GivenIFetchUserById(int id)
        {
            _user = _repo.GetUserById(id);
            Assert.IsNotNull(_user, $"User with Id {id} was not found.");
        }

        [When(@"I update the LastName to ""(.*)""")]
        public void WhenIUpdateLastName(string newLastName)
        {
            _user.LastName = newLastName;
            _repo.UpdateUser(_user);
        }

        [Then(@"the updated LastName should be ""(.*)""")]
        public void ThenLastNameShouldBe(string expected)
        {
            var updatedUser = _repo.GetUserById(_user.Id);
            Assert.AreEqual(expected, updatedUser.LastName, "Last name was not updated.");
        }

        [When(@"I delete the user")]
        public void WhenIDeleteUser()
        {
            _repo.DeleteUser(_user.Id);
        }

        [Then(@"the user should not exist in the database")]
        public void ThenUserShouldNotExist()
        {
            var deleted = _repo.GetUserById(_user.Id);
            Assert.IsNull(deleted, "User was not deleted.");
        }

        [When(@"I fetch all users")]
        public void WhenIFetchAllUsers()
        {
            _allUsers = _repo.GetAllUsers();
        }

        [Then(@"the list of users should not be empty")]
        public void ThenListShouldNotBeEmpty()
        {
            Assert.IsTrue(_allUsers.Any(), "User list is empty.");
        }
    }
}
