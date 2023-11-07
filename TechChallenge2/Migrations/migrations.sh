cd ./NoticiasAPI

dotnet ef migrations add InitialCreate --context NoticiasContext --output-dir Migrations/NoticiasMigrations
dotnet ef migrations add InitialCreate --context IdentityContext --output-dir Migrations/IdentityMigrations

dotnet ef database update --context NoticiasContext
dotnet ef database update --context IdentityContext