Bsynchro RJB Solution

I used the Clean Architecture to build the solution and you can test the API following these steps:
  1.	change the connection string of the database from the “appsettings.json” and “appsettings.Development.json” files.
  2.	When you run the solution, the swagger page will be shown to test the API.
  3.	Create a new customer
  4.	Create a new Account with initial credit or not
  5.	Insert a new Transaction
  6.	Get the customer full information using “/api/Customers/{id}/FullInformation”

I created two testing projects with some samples to test the API and you should change the IntegrationTesting project SqlConnection string from “ApiTestFixture.cs” before running Integration test.

Also, we can add some features to the API like:
  •	Logging using NLog
  •	Validating using FluentValidation
  •	Pagination to implement lazy loading for Customers, Accounts, Transactions.

Finally, we can build and test the project using CI pipeline in Azure DevOps and then deploy it to the staging environment using CD pipeline.