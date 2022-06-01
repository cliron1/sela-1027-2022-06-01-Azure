using SignalRChat.Hubs;
using Microsoft.Extensions.Azure;
using Azure.Storage.Blobs;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
    builder.Configuration.AddAzureKeyVault(new Uri(Environment.GetEnvironmentVariable("VaultUri")), new DefaultAzureCredential());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddAzureClients(clientBuilder => {
    clientBuilder.AddBlobServiceClient(builder.Configuration.GetConnectionString("storage"));
    //clientBuilder.AddQueueServiceClient(builder.Configuration.GetConnectionString("storage"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

app.Run();
