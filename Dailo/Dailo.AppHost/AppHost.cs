IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> db = builder
    .AddPostgres("dailo-db")
    .WithLifetime(ContainerLifetime.Persistent);

builder
    .AddProject<Projects.Dailo_Api>("api")
    .WithHttpsEndpoint(port: 5055)
    .WithReference(db)
    .WaitFor(db);

await builder.Build().RunAsync();
