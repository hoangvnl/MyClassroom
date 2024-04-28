using MyConfigurationServer.gRPC.Mapper;
using MyConfigurationServer.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(MapperProfile));


var DbSettingSection = builder.Configuration.GetSection("ClassroomConfigurationDatabase");
builder.Services.Configure<ClassroomConfigurationDatabaseSettings>(DbSettingSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ConfigurationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
