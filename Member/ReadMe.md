# User
Microservice with users logic.

# How to setup db (not required)
* Install [postgreSQL](https://www.postgresql.org/) 
* Use this [script](https://github.com/KurnakovMaksim/jiraF/blob/main/User/db.sql)
* Configure connection string
``` ps
dotnet user-secrets set "ConnectionString" "Server=localhost;Port=5432;Database=jiraf_memUsermber Id=postgres;Password=yourPassword;" --projectUsermber\src\jiUsermber.API\"
```
* Edit program file from
``` cs
builder.Services.AddDbContext<AppDbContext>(options =>
{
#if DEBUG // TODO: Delete this line, now if we do this, tests be broken.
    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
#else
    options.UseInMemoryDatabase("TestData");
    //options.UseNpgsql(builder.Configuration["ConnectionString"]);
#endif
});
```
to
``` cs
builder.Services.AddDbContext<AppDbContext>(options =>
{
#if DEBUG // TODO: Delete this line, now if we do this, tests be broken.
    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
#else
    //options.UseInMemoryDatabase("TestData");
    options.UseNpgsql(builder.Configuration["ConnectionString"]);
#endif
});
```