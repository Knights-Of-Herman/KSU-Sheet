
# Setup
## Setup Development Environment

1. Azure SQL Docker Container Installation:
    1. Setup this container
       - https://hub.docker.com/_/microsoft-mssql-server/
    2. Use the account details below.
    3. Run all of the Database setup scripts
2. Azure SQL Settings
   - Connection String
       '"Server=localhost;Database=KohDev2;User Id=SA;Password=@serverpassword123;TrustServerCertificate=True"'
   - String Locations:
       - KnightsOfherman.Backend.Server/appsettings.Development.json
       - KnightsOfHerman.XUnitTesting/AzureDatabase/DBFactory.cs 

3. Ensure the container is running.

## Setup Live Database Connection
1. Request Entrata access from your adminstrator
2. Login to the connection with your Entrata account

# Running Code
### Running a local session against local database
- Run multiple startup projects
  - KnightsOfHerman.Frontend.WebApp on Local
  - KnightsOfHerman.Backend.Server on Local
### Running a local session against live database
- Run multiple startup projects
  - KnightsOfHerman.Frontend.WebApp on Local
  - KnightsOfHerman.Backend.Server on Production
### Running a local frontend against live backend
- Run KnightsOfHerman.Frontend.WebApp on Local

# Unit Testing
Run the desired XUnit Tests from KnightsOfHerman.XUnitTesting. This will run the tested database operation against either the local db or a mock db depending on the test.
It is best to run these tests individualy as they cleanup the dev database afterthemselves and could cause false failures because of that.

# Adding Database Procedures
Add the procedure to ProcedurePermissions.sql in the format `GRANT EXECUTE ON [dbo].[PROCEDURE] TO [knightsofherman-backend];`

# Publishing Code
Use respective publishing profile for either the Backend or Webapp.

# Live App
Currently at: https://knightsofherman.azurewebsites.net 


