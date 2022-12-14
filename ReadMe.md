[![Main repo](https://img.shields.io/static/v1?label=&message=MainRepo&color=orange)](https://github.com/KurnakovMaksim/jiraF/) 
[![Hits-of-Code](https://hitsofcode.com/github/KurnakovMaksim/jiraF-goal?branch=main)](https://hitsofcode.com/github/KurnakovMaksim/jiraF-goal/view?branch=main)
# Goal
Microservice with goals logic.

# How to start
* Setup ApiKey
``` ps
dotnet user-secrets set "GoalApiKey" "yourApiKey" --project ".\Goal\src\jiraF.Goal.API\"
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
dotnet user-secrets set "DefaultConnection" "Server=localhost;Port=5432;Database=jiraf_goal;User Id=postgres;Password=yourPassword;" --project ".\Goal\src\jiraF.Goal.API\"
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
