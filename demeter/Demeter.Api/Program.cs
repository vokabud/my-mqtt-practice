using Demeter.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureApp();

app.Run();
