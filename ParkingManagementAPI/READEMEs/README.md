Assumptions:
------------
- .NET 8 (LTS)
- No auth for endpoints. If it was Azure solution for some company I would have went with something like Microsoft Identity token based authentication
- main endpoint controller url was supposed to be https://localhost:<port>/parking
- No requirement for seeding db. Especially number of parking spots. Based on that chose .HasData()
- Vehicle registration format is verified in frontend app
- Charges only apply for full minutes that vehicle was parked. Example 5min 30s is a billing for 5min.
- The requirements for error handling & unit tests did not expect full code coverage. Only to showcase basic understanding
- Designed the project to be flexible for possible additions during live coding. Otherwise factories and splitting configurations in such small project is not that meaningful

Databases:
----------
- MSSQL local db for API with .HasData() seeding. Migration already created. "Update-database" command in package manager console should create tables & seed them with mock data
- InMemoryDatabase for Tests so they can run in pipeline. Should work on its own.


Steps:
------
- Clone repository
- Add appsettings.json and paste following code
- Run Update-database command in package manager console (make sure it runs for ParkingManagementAPI and not Tests)
- Build & run API
- Test endpoints with something like Postman

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": { "localDb": "Server=(localdb)\\mssqllocaldb;Database=CarParking;Trusted_Connection=True;ConnectRetryCount=0"}
}
```

DISCLAIMER: Never put connection strings/secrets in repo. This however doesn't contain credentials as its for localdb, so for the purporse of the excercise I included it in readme rather than pushing appsettings.json

Endpoints:
-----------------------
- GET <api url>/parking/ping
- GET <api url>/parking
- POST <api url>/parking
- POST <api url>/parking/exit

| Type | Route | Query Params | Body | Return | Notes |
| ---- | ----- | ------------ | ---- | ------ | ----- |
| POST | /parking | | {VehicleReg: string, VehicleType: int} | {VehicleReg: string, SpaceNumber: int, TimeIn: DateTime} | Parks a given vehicle in the first available space and returns the vehicle and its space number |
| GET | /parking | | | {AvailableSpaces: int, OccupiedSpaces: int} | Gets available and occupied number of spaces |
| POST | /parking/exit | | {VehicleReg: string} | {VehicleReg: string, VehicleCharge: double TimeIn: DateTime, TimeOut: DateTime} | Should free up this vehicles space and return its final charge from its parking time until now |
