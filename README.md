# AssessmentTask
The assessment consists of an API to be used for opening a new “current account” of already existing  customers.
#Start the Assessment by creating the tables(Customers,Accounts,Transactions) in the database attached.
For the backend:
-----------------
Step 1 : Install Required NuGet Packages 
•Dapper
•System.Data.SqlClient
Step 2 : put the connection string in appsettings.json file to link the databse to the backend project
Step 3 : Create DapperContext class for the creation of the database connection using Dapper and the connection string retrieved from the application's configuration.
Step 4: Update the Program.cs File configuring the dependency injection 
Step 5 : Create the models and repositories 
Step 6 : step4
Step 7 : Create the controller to handle HTTP requests related to customer operations.
 It provides endpoints for retrieving customer information, opening new accounts, and retrieving user information.

for the frontend:
------------------
Step 1 :create the models and services that will help retreiving and posting data using apis url created in the backend
Step 2 :Create a homecomponent to be able to open the account where the request info are sent by a selector option and input
using data binding another component to print the user info in a table
Step 3 : Add routing in AppRoutingModule for both components created
