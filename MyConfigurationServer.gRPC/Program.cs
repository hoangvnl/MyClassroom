using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyClassroom.Contracts;
using MyConfigurationServer.gRPC.Interceptors;
using MyConfigurationServer.gRPC.Mapper;
using MyConfigurationServer.gRPC.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddGrpc(options =>
//{
//    options.Interceptors.Add<ServerTokenValidatorInterceptor>();
//});
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(MapperProfile));


var DbSettingSection = builder.Configuration.GetSection("ClassroomConfigurationDatabase");
builder.Services.Configure<ClassroomConfigurationDatabaseSettings>(DbSettingSection);

var apiSettingsSection = builder.Configuration.GetSection("APISettings:gRPCConfiguration");
var apiSettings = apiSettingsSection.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = apiSettings.ValidAudience,
        ValidIssuer = apiSettings.ValidIssuer,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<ConfigurationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
