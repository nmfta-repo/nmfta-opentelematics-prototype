# nmfta-opentelematics-prototype
Open Telematics Prototype

1) The Prototype.Telematics.BridgeServer database project has all the SQL Scripts to setup the SQL Server Database
2) To Initialize the database 
  a) Create a new SQL Server Database called OpenTelematics <= The Prototype Database was tested in SQL Server 2017
  b) Execute scripts under Tables, Stored Procedures and Data folders
3) The Prototype OTAPI was implemented with ASP.NET Core 2.2
4) Open nmfta-opentelematics-prototype.sln n Visual Studio 2019
5) Check the Connection and API Launch settings in launchSettngs.json and appsettings.json
6) Set the Default Project to Prototype.OpenTelematics.Api
7) Launch API Server by Pressing F5 (Debug) or CTRL+F5 (Start without Debug)
