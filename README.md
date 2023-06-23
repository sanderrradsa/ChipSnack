# Project chipsnack

Welkom bij het lunchsite project van groep 3C (Jorrit de Haan, Bram Jonker, Sander Burger, Bas wijkstra)




## Database

### Setup
- [chipsnack.sql](WebdevProjectStarterTemplate/Database_Setup/chipsnack.sql) Maakt de database aan voor dit project (gaat er van uit dat er nog geen database is)
- [Required.sql](WebdevProjectStarterTemplate/Database_setup/Required.sql) vult de database met data die nodig is om te functioneren (gaat er van uit dat de database leeg is)
- [Dummy.sql](WebdevProjectStarterTemplate/Database_setup/Required.sql) Vult de database met test data (gaat er van uit dat de database leeg is)


### Credentials
Pas je connectionstring aan in [appsettings](WebdevProjectStarterTemplate/appsettings.json):
```json
"ConnectionStrings" : {
  "WebdevCourseRazorPages.Exercises.MySQL": "Server=127.0.0.1;Port=3306;Database=[DB_NAME];Uid=[USER];Pwd=[PASSWORD];"
}
```

## Project structuur (directories)

Het project bestaat uit de volgende directories:
* Models: Hierin staan de modellen (Categorie, Snack, etc) die gebruikt worden in de applicatie.
* Repositories: Hier staan de methodes die gebruikt worden om de modellen te synchroniseren met de databaase.
* Pages: Paginas


## Gebruikte technieken

De voglende technieken worden gebruikt in het project:
* [ASP.NET Core Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-5.0&tabs=visual-studio)
* [Dapper](https://github.com/DapperLib/Dapper) voor het mappen van SQL data op objecten. Voor documentatie zie [Dapper Tutorial](https://dapper-tutorial.net/dapper)
* [MySQL](https://dev.mysql.com/downloads/installer/)
* [Bootstrap](https://getbootstrap.com/docs/5.0/getting-started/introduction/)
* [jQuery](https://jquery.com/)

