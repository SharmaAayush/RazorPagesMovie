using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string ConnectionStringKey = "RazorPagesMovieContext";
if (!builder.Environment.IsDevelopment())
{
    ConnectionStringKey = "ProductionMovieContext";
}

builder.Services.AddDbContext<RazorPagesMovieContext>(
    options =>
    {
        if (String.IsNullOrEmpty(builder.Configuration.GetConnectionString(ConnectionStringKey)))
        {
            throw new InvalidOperationException($"Connection string {ConnectionStringKey} not found.");
        }
        options.UseSqlite(builder.Configuration.GetConnectionString(ConnectionStringKey));
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
