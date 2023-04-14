
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Repository;
using Services;
using Services.Image;

namespace LocationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ATContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("ATConnection"));
            });
            builder.Services.AddScoped<ATContext, ATContext>();

            builder.Services.AddScoped<SqlConnection>(s => new SqlConnection(builder.Configuration.GetConnectionString("ATConnection")));
            builder.Services.AddScoped<CountryRepository, CountryRepository>();
            builder.Services.AddScoped<StateRepository, StateRepository>();
            
            builder.Services.AddScoped<ImageRepository, ImageRepository>();
            builder.Services.AddScoped<ImageService, ImageService>();


            builder.Services.AddScoped<CountryService, CountryService>();
            builder.Services.AddScoped<StateService, StateService>();

            builder.Services.AddAzureClients(client =>
            {
                client.AddBlobServiceClient(builder.Configuration["ConnectionStrings:Storage:blob2"]);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}