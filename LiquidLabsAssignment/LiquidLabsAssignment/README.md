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

# PreRequisites
install the following tools to run the application:
- Visual Studio 2022 or later
- SQL Server 2019 or later
- .NET 10 SDK
- git

verify the dotnet version by running the following command in the terminal:
```bash	dotnet --version
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
   - Open the `appsettings.json` file in the project.
   - Update the `ConnectionStrings` section with your SQL Server connection details. For example:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LiquidLabsDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

5. Third Party API Configuration:
   - The application fetches data from the JsonPlaceholder API.
   - BaseURl: https://jsonplaceholder.typicode.com/posts
   - This API is free to use and does not require any authentication or API key.
   - Update the `appsettings.json` file with the base URL of the JsonPlaceholder API.

6. Restore dependencies and build the project:
```bash dotnet restore  
```
---

7. Build the API project:
```bash dotnet build
```
---

8. Open Swagger UI to test the API endpoints:
   - Run the application using Visual Studio or the command line.
   - Once the application is running, open a web browser and navigate to `https://localhost:xxxx/swagger` (the appropriate URL based on your configuration).
   - You will see the Swagger UI where you can test the API endpoints.

9. API Endpoints:
   - GET /api/posts: Retrieves all posts from the database. If the database is empty, it fetches data from the JsonPlaceholder API, stores it in the database, and returns the data.
   - GET /api/posts/{id}: Retrieves a specific post by ID from the database. If the post is not found in the database, it fetches data from the JsonPlaceholder API, stores it in the database, and returns the data.

10. Local caching behavior:
   - The application implements a simple caching mechanism by storing the fetched data in the SQL Server database. 
   - When a request is made to retrieve posts, the application first checks if the data is already present in the database. If it is, it returns the cached data. If not, it fetches the data from the JsonPlaceholder API, stores it in the database, and then returns it to the user.

11. Error Handling:
   - unexpected errors are handled by global exception handling middleware.
   - expected errors such as invalid post ID or database connection issues are handled gracefully and appropriate error messages are returned to the user.

  