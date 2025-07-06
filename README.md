🧪 DatabaseManager
Professional C# + SpecFlow + MSTest Database Automation Framework (.NET 8)
This project demonstrates how to validate SQL database operations (CRUD) in a structured and maintainable test automation framework using SpecFlow.

📌 1. Technologies Used
✅ .NET 8

✅ SpecFlow (BDD with Gherkin)

✅ MSTest

✅ SQL Server

✅ ADO.NET (for direct DB access)

✅ Visual Studio 2022+

🗂 2. Folder Structure

DatabaseSpecflowFramework/
├── Features/            # .feature files for CRUD scenarios
├── StepDefinitions/     # SpecFlow step implementation
├── DatabaseManager/
│   ├── Models/          # User POCO
│   └── Repositories/    # UserRepository: all DB logic
├── Hooks/               # Setup/Cleanup logic using BeforeScenario/AfterScenario
├── App.config           # Connection string to SQL DB
└── DatabaseSpecflowFramework.csproj
⚙️ 3. Configuration
🔗 App.config
Add your SQL Server connection string inside App.config:

xml
<connectionStrings>
  <add name="DbConnection" 
       connectionString="Data Source=localhost;Initial Catalog=YourDbName;Integrated Security=True"
       providerName="System.Data.SqlClient"/>
</connectionStrings>


📦 4. Required NuGet Packages
Install via NuGet Package Manager in Visual Studio:

SpecFlow

SpecFlow.MSTest

SpecFlow.Tools.MsBuild.Generation

MSTest.TestFramework

System.Data.SqlClient

📋 5. Sample Feature File (BDD Scenarios)
Features/DatabaseOperations.feature:

🧪 6. Running Tests
Open the solution in Visual Studio

Use Test Explorer or right-click .feature → Run SpecFlow Scenarios

Supports tag-based execution: @insert, @update, @delete, @getall

🔄 7. Hooks: Data Cleanup & Preparation
In Hooks/DatabaseHooks.cs, hooks manage data state:

[BeforeScenario("@insert")]: Clean up existing user before inserting

[AfterScenario("@insert")]: Delete inserted user

[AfterScenario("@delete")]: Re-insert dummy user after deletion

This ensures clean test environments and re-runnable tests.

✅ 8. Example Usage
Inside your test class:

var repo = new UserRepository();

// Insert
repo.InsertUser(new User { FirstName = "John", LastName = "Doe", Email = "john@example.com" });
// Update
var user = repo.GetUserById(1);
user.LastName = "Smith";
repo.UpdateUser(user);
// Delete
repo.DeleteUser(3);
// Get All
var allUsers = repo.GetAllUsers();


🧠 9. Best Practices
Use .resx files or test data table for parameterization

Separate DB logic (Repositories/) from test steps

Reuse ScenarioContext to pass user info between steps

Validate by querying DB directly after each operation

📍 10. Notes
The database connection must be active and accessible.

Table Users must exist with at least: Id, FirstName, LastName, Email.

You can extend this to support stored procedures or EF Core.
