
# PetShopProject

PetShopProject is an ASP.Net Core web application for browsing Pets uploaded by users.

The project is build in monolith architecture, MVC application using C# .Net Core, RazorEngine, Entity Framework Core & Sql Server.
The Application uses dependency injection to inject database context for single use, and injects repositories with a scoped lifetime throughout the app's life cycle, and consists of four projects:
  **Client, Data , Service, UnitTest**

### Client

Client Project is responsible for creating controllers, views, viewmodels and static files needed for FE Development to work consistently.
### Data

Data Project is in charge of connecting to sql server by Instantiating (Entity Framework Core) context, Describing Models and Generic Interfaces.
### Service 

Service Project manages the models repostiories and logics for creating needed queries.
### UnitTest 

UnitTest Project run tests for PetShopProject's logic.



## Installation

!!! This Application is not containerized with docker and therefore cannot be installed easily, rather the application's code is meant to be observed. !!!

  if you ought to run and install the application anyways, those are the instructions:

  * Make sure .Net SDK 6 and Sql server is installed in your computer.
  * Open PetShopProject.sln with your IDE and :
    * open appsettings.json located in PetShop.Client and replace PetShopDataConnection Value With Your Sql Connection String.
    * restore nuget packages in solution - right click solution and click 'restore nuget packages' OR write in terminal - 'dotnet restore'
  * Build Solution and Run ( Make Sure Running PetShop.Client)


