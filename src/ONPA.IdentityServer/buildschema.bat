rmdir /S /Q "Data/Migrations"

dotnet ef migrations add Initial -c IdentityDbContext -o Data/Migrations/IdentityDB
dotnet ef migrations add Initial -c PersistedGrantDbContext -o Data/Migrations/PersistedGrantDb
dotnet ef migrations add Initial -c ConfigurationDbContext -o Data/Migrations/ConfigurationDb
