# Goal
Microservice with goals logic.

# How to start
* Setup ApiKey
``` ps
dotnet member-secrets set "GoalApiKey" "yourApiKey" --project ".\Goal\src\jiraF.Goal.API\"
```
* Start project
``` ps
dotnet run --property:Configuration=Release --project .\Goal\src\jiraF.Goal.API\
```
Project reference
https://localhost:7079/swagger/index.html

# How to setup db (not required)
* Install [postgreSQL](https://www.postgresql.org/) 
* Use this [script](https://github.com/KurnakovMaksim/jiraF/blob/main/Goal/db.sql)
* Configure connection string
``` ps
dotnet member-secrets set "DefaultConnection" "Server=localhost;Port=5432;Database=jiraf_goal;Member Id=postgres;Password=yourPassword;" --project ".\Goal\src\jiraF.Goal.API\"
```
* Edit program file from
``` cs
builder.Services.AddDbContext<AppDbContext>(options =>
{
#if DEBUG // TODO: Delete this line, now if we do this, tests be broken.
    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
#else
    options.UseInMemoryDatabase("TestData");
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
#endif
});
```
to
``` cs
#if DEBUG // TODO: Delete this line, now if we do this, tests be broken.
    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
#else
    //options.UseInMemoryDatabase("TestData");
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
#endif
```