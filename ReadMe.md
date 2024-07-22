[![Main repo](https://img.shields.io/static/v1?label=&message=MainRepo&color=orange)](https://github.com/KurnakovMaksim/jiraF/)
[![Hits-of-Code](https://hitsofcode.com/github/KurnakovMaksim/jiraF-goal?branch=main)](https://hitsofcode.com/github/KurnakovMaksim/jiraF-goal/view?branch=main)
[![Render](https://img.shields.io/static/v1?label=&message=Render&color=grey&logo=render)](https://jiraf-goal.onrender.com/ping)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=KurnakovMaksim_jiraF&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=KurnakovMaksim_jiraF)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=KurnakovMaksim_jiraF&metric=coverage)](https://sonarcloud.io/summary/new_code?id=KurnakovMaksim_jiraF)
[![CodeQL](https://github.com/KurnakovMaksim/jiraF-goal/workflows/CodeQL/badge.svg)](https://github.com/KurnakovMaksim/jiraF-goal/actions?query=workflow%3ACodeQL)
[![Codecov](https://codecov.io/gh/KurnakovMaksim/jiraF/branch/main/graph/badge.svg)](https://codecov.io/gh/KurnakovMaksim/jiraF)

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
* Use this [script](./Goal/db.sql)
* Configure connection string
``` ps
dotnet user-secrets set "DefaultConnection" "Server=localhost;Port=5432;Database=jiraf_goal;User Id=postgres;Password=yourPassword;" --project ".\Goal\src\jiraF.Goal.API\"
```
* Edit program file from
``` cs
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase(TestVariables.IsWorkNow
        ? Guid.NewGuid().ToString()
        : "TestData");
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
to
``` cs
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseInMemoryDatabase(TestVariables.IsWorkNow
    //     ? Guid.NewGuid().ToString()
    //     : "TestData");
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
