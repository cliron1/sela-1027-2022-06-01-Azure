using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

if (!builder.Environment.IsDevelopment())
    builder.Configuration.AddAzureKeyVault(new Uri(Environment.GetEnvironmentVariable("VaultUri")), new DefaultAzureCredential());

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
