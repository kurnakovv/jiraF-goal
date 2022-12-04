#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Goal/src/jiraf.Goal.API/jiraF.Goal.API.csproj", "Goal/src/jiraf.Goal.API/"]
RUN dotnet restore "Goal/src/jiraf.Goal.API/jiraF.Goal.API.csproj"
COPY . .
WORKDIR "/src/Goal/src/jiraf.Goal.API"
RUN dotnet build "jiraF.Goal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jiraF.Goal.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jiraF.Goal.API.dll"]