using CompreApp.Api.Endpoints;
using CompreApp.Application;
using CompreApp.Infra;
using CompreApp.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Microsoft.OpenApi.Any;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using CompreApp.Infra.GatewayPagamento;
using CompreApp.Infra.Data.SeedData;
using CompreApp.Infra.Common;
using CompreApp.Infra.GatewayPagamento.ApiPagamento;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddApplicationServices();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.Configure<ConfigSettings>(builder.Configuration.GetSection("ConfigSettings"));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("SeqConnection"))
    .CreateLogger();

Log.Information("Iniciando aplicação");

builder.Host.UseSerilog(Log.Logger);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ConfigSettings:JwtChave"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(option =>
{
    option.EnableAnnotations();
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "CompreApp API", Version = "v1", Description = "API de compra de aplicativos" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Informar apenas o 'token' gerado na autenticação",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    option.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    });
});

var app = builder.Build();

var migrationService = app.Services.GetRequiredService<MigracaoData>();
migrationService.AplicarMigracao();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature != null)
        {
            await context.Response.WriteAsJsonAsync(new ErroGlobal(context.Response.StatusCode, "Erro interno do servidor"));
        }

    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapProdutoEndpoints();
app.MapClienteEndpoints();
app.MapPedidoEndpoints();

app.Run();