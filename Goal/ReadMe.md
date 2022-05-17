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