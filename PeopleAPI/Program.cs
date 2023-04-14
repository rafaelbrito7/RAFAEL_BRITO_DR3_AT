
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Repository;
using Services;
using Services.Image;

namespace FriendAPI
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
            builder.Services.AddScoped<PersonRepository, PersonRepository>();
            builder.Services.AddScoped<FriendshipRepository, FriendshipRepository>();
            builder.Services.AddScoped<ImageRepository, ImageRepository>();

            builder.Services.AddScoped<PersonService, PersonService>();
            builder.Services.AddScoped<FriendshipService, FriendshipService>();
            builder.Services.AddScoped<ImageService, ImageService>();
            

            builder.Services.AddAzureClients(client =>
            {
                client.AddBlobServiceClient(builder.Configuration["ConnectionStrings:Storage:blob2"]);
            });

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}