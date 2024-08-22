//using FluentAssertions.Common;
//using MediatR;
//using Microsoft.Data.Sqlite;
//using Questao5.Domain.Repositories;
//using Questao5.Infrastructure.Database.CommandStore;
//using Questao5.Infrastructure.Sqlite;
//using System.Data;
//using System.Reflection;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//builder.Services.AddScoped<IDbConnection>(sp =>
//{
//    var connectionString = "Data Source=database.sqlite";
//    return new SqliteConnection(connectionString);
//});
//// sqlite
////builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
//builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

//builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
//builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
//builder.Services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//// sqlite
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//app.Services.GetService<IDatabaseBootstrap>().Setup();
//#pragma warning restore CS8602 // Dereference of a possibly null reference.

//app.Run();

//// Informações úteis:
//// Tipos do Sqlite - https://www.sqlite.org/datatype3.html

using FluentAssertions.Common;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Sqlite;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurando o MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Configurando a conexão com o SQLite
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = "Data Source=database.sqlite";
    return new SqliteConnection(connectionString);
});

// Configurando o DatabaseConfig
builder.Services.AddSingleton(new DatabaseConfig
{
    Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite")
});

// Registrando o DatabaseBootstrap
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Registrando os repositórios
builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
builder.Services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();

// Configurando Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurando o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicializando o banco de dados
var databaseBootstrap = app.Services.GetRequiredService<IDatabaseBootstrap>();
databaseBootstrap.Setup();

app.Run();