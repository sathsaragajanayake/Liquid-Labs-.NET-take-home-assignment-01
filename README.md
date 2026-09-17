# Liquid-Labs-.NET-take-home-assignment-01

# Overview
This is a simple .NET 10 Web API project that fetches POST data from the JsonPlaceholder API and store in MS SQL Server Database.
The Application Provides RESTful endpoints to retreive posts. if requested dta already in the databse 
then it will return the data from the database otherwise it will fetch the data from the JsonPlaceholder API and store in the database and return the data to the user.

# Technologies Used
- ASP .NET CORE WEB API
- .NET 10
- C#
- SQL SERVER 
- ADO.NET

# Libraries
- Swashbuckle.AspNetCore - to visualize and test the API endpoints 
- Microsoft.Data.SqlClient - Used for direct communication with SQL Server
- CtorMock.Moq - For Unit Tests, Used to mock the repository dependency

# PreRequisites
install the following tools to run the application:
- Visual Studio 2022 or later
- SQL Server 2019 or later
- .NET 10 SDK
- git

verify the dotnet version by running the following command in the terminal:
```bash	
dotnet --version
```
---


1. Clone the repository using the following command:
```bash 
git clone https://github.com/sathsaragajanayake/Liquid-Labs-.NET-take-home-assignment-01.git
cd Liquid-Labs-.NET-take-home-assignment-01
```
2. Open the solution file in Visual Studio 2022 or later.

3. Create a new database:
     Database schema is available in the `LiquidLabsAssignment/Database/DatabaseSchema.sql` file. You can run this script in SQL Server Management Studio (SSMS) to create the necessary database and tables.
     it will create a database named `LiquidLabsDB` and a table named `Posts` with the required columns.
     posts table is initially empty, the application will populate the table when the API is called for the first time.

4. Configure Database Connection String:
   - The database connection string is not stored in `appsettings.json`.
   - Set the following environment variable before running the application: `ConnectionStrings__DefaultConnection`
   - For Windows PowerShell, set the environment variable using:
  ```bash 
   $env:ConnectionStrings__DefaultConnection="Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=LiquidLabsDB;Trusted_Connection=True;TrustServerCertificate=True;"
  ```
   - Replace `YOUR_SERVER_NAME` with your SQL Server instance name.
   - To verify that the environment variable is set correctly, you can run the following command in PowerShell:
   ```bash
   $env:ConnectionStrings__DefaultConnection
   ```

5. Third Party API Configuration:
   - The application fetches data from the JsonPlaceholder API.
   - BaseURl: https://jsonplaceholder.typicode.com/posts
   - This API is free to use and does not require any authentication or API key.
   - Update the `appsettings.json` file with the JsonPlaceholder API URL.

6. Restore dependencies and build the project:
```bash 
dotnet restore  
```

7. Build the API project:
```bash 
dotnet build
```

8. Run the API project:
   - Make sure the database environment variable has been configured in the current PowerShell session
```bash
dotnet run
```
   - The terminal will display the URL where the application is running.

9. Open Swagger UI to test the API endpoints:
   - Once the application is running, open a web browser and navigate to `https://localhost:xxxx/swagger` (the appropriate URL based on your configuration).
   - You will see the Swagger UI where you can test the API endpoints.

10. API Endpoints:
   - GET /api/posts: Retrieves all posts from the database. If the database is empty, it fetches data from the JsonPlaceholder API, stores it in the database, and returns the data.
   - GET /api/posts/{id}: Retrieves a specific post by ID from the database. If the post is not found in the database, it fetches data from the JsonPlaceholder API, stores it in the database, and returns the data.

11. Local caching behavior:
   - The application implements a simple caching mechanism by storing the fetched data in the SQL Server database. 
   - When a request is made to retrieve posts, the application first checks if the data is already present in the database. If it is, it returns the cached data. If not, it fetches the data from the JsonPlaceholder API, stores it in the database, and then returns it to the user.

12. Error Handling:
   - unexpected errors are handled by global exception handling middleware.
   - expected errors such as invalid post ID or database connection issues are handled gracefully and appropriate error messages are returned to the user.

13. Build and Run Summary:
 1. ```bash
    dotnet restore
    ```
 2. ```bash
    dotnet build
    ```
 3. ```bash
    dotnet run
    ```
4. Open Swagger UI at `https://localhost:xxxx/swagger` to test the API endpoints.

5. Database connection string should be set as an environment variable before running the application. `ConnectionStrings__DefaultConnection`

# Unit Testing

- The project includes a separate xUnit test project called `LiquidLabsAssignment.Tests`
- xUnit - Used as the unit testing framework.

# To run the unit tests:
- From the solution directory, run:
  ```bash 
  dotnet test
  ```
- This will build the test project and run all available unit tests.

# To run tests using Visual Studio:

- Open the solution in Visual Studio.
- Build the solution.
- Open Test then Test Explorer.
- Select Run All.


