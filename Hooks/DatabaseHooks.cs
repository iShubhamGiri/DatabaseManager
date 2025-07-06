using SeleniumSpecflowDatabaseProject.DatabaseManager.Models;
using SeleniumSpecflowDatabaseProject.DatabaseManager.Repositories;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SeleniumSpecflowDatabaseProject.Hooks
{
    [Binding]
    public class DatabaseHooks
    {
        private readonly ScenarioContext _context;
        private readonly UserRepository _repo;

        public DatabaseHooks(ScenarioContext context)
        {
            _context = context;
            _repo = new UserRepository();
        }

        [BeforeScenario("@insert", Order = 1)]
        public void CleanUpBeforeInsertScenario()
        {
            // Clean users with same email if pre-exists (helps re-run same scenario)
            var email = "john@example.com";
            var allUsers = _repo.GetAllUsers();

            foreach (var user in allUsers)
            {
                if (user.Email == email)
                {
                    _repo.DeleteUser(user.Id);
                }
            }
        }

        [AfterScenario("@insert", Order = 1)]
        public void CleanUpAfterInsertScenario()
        {
            // Delete inserted user to keep DB clean
            if (_context.TryGetValue("NewUser", out object newUserObj) && newUserObj is User newUser)
            {
                var allUsers = _repo.GetAllUsers();

                foreach (var user in allUsers)
                {
                    if (user.Email == newUser.Email)
                    {
                        _repo.DeleteUser(user.Id);
                    }
                }
            }
        }

        [AfterScenario("@delete", Order = 1)]
        public void RestoreDeletedUser()
        {
            // Reinsert dummy user if needed for test continuity
            var dummyUser = new User
            {
                FirstName = "Temp",
                LastName = "Deleted",
                Email = "temp.deleted@example.com"
            };

            _repo.InsertUser(dummyUser);
        }
    }
}
