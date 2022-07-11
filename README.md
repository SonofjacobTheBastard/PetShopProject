

PetShopProject is an ASP.Net Core web application for browsing Pets uploaded by users.

The project is build in monolith architecture, MVC application using C# .Net Core, RazorEngine, Entity Framework Core, Sql Server and consists of four projects:

1. Client - Client Project is responsible for creating controllers, views, viewmodels and static files needed for FE Development to work consistently.
2. Data - Data Project is in charge of connecting to sql server by Instantiating (Entity Framework Core) context, Describing Models and Generic Interfaces.
3. Service - Service Project manages the models repostiories and logics for creating needed queries.
4. UnitTest - UnitTest Project run tests for PetShopProject's logic.

The Application uses dependency injection to inject database context for single use, and injects repositories with a scoped lifetime throughout the app's life cycle.


