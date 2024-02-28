using JarmuKolcsonzo.Model;
using Microsoft.EntityFrameworkCore;

namespace JarmuKolcsonzo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<JKContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("JKDB"),
                    ServerVersion.Parse("10.4.24-mariadb")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}