GameUp is a project for gamers to provide services to another gamers, such as in-game classes, match review, etc.

to add a new migration
dotnet ef migrations add <migration_name> --startup-project ./ --project ../InGameServices.Data
to run a migration
dotnet ef database update <migration_name> --startup-project ./ --project ../InGameServices.Data
