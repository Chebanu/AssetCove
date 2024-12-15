using AssetCove.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "AssetCove",
                Description = "AssetCove",
            });
        });

        builder.Services.AddControllers();

        builder.Services.AddDbContext<AssetCoveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseAuthentication();
        //app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}

public partial class Program { }