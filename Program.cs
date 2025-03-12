using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Adicionar DbContext com a string de conexão
builder.Services.AddDbContext<TesteCamposDealerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    // Código de configuração específico para o ambiente de desenvolvimento (ex.: Swagger)
}

app.UseHttpsRedirection();

app.Run();
