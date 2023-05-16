# Template voor zowel het project als de individiuele opdracht (NHLStenden Café).

Benodigheden:
* [.NET 7](https://dotnet.microsoft.com/download) of hoger, 
* [MySQL](https://dev.mysql.com/downloads/installer/)

## Database aanmaken
Om je database aan te maken voor de tabellen (Product & Category) en deze te vullen met data, 
is het aan te raden het bestand `MysqlCafe.sql` te gebruiken.

## Connectionstring aanpassen (indien nodig)
Pas je connectionstring aan in `appsettings.json` als de verbinding anders is dan die van mij (username, password, locatie, port, etc):
```json
"ConnectionStrings": {
  "WebdevCourseRazorPages.Exercises.MySQL": "Server=127.0.0.1;Port=3306;Database=WebdevProject;Uid=root;Pwd=Test@1234!;"
}
```

## Project structuur (directories)

Het project bestaat uit de volgende directories:
* Models: Hierin staan de modellen (Category, Product) die gebruikt worden in de applicatie.
* Repositories: Hier staan de repositories die gebruikt worden om de modellen op te slaan in de database.
* Pages/Categories: Hier staan de Razor Pages die gebruikt worden om de categorieën te beheren.


## Gebruikte technieken

De voglende technieken worden gebruikt in het project:
* [ASP.NET Core Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-5.0&tabs=visual-studio)
* [Dapper](https://github.com/DapperLib/Dapper) voor documentatie zie [Dapper Tutorial](https://dapper-tutorial.net/dapper)
* [MySQL](https://dev.mysql.com/downloads/installer/)
* [Bootstrap](https://getbootstrap.com/docs/5.0/getting-started/introduction/)
* en last but not least HTML & CSS :-)

