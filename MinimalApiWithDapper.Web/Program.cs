
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using MinimalApiWithDapper.Web.Data;
using MinimalApiWithDapper.Web.Data.Entites;
using MinimalApiWithDapper.Web.Extentions;
using System.Security.Cryptography.Xml;

namespace MinimalApiWithDapper.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            webApplicationBuilder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

         
           

            var app = webApplicationBuilder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.EmployeeEndpoints(); // employee Endpoints



            app.Run();
        }
    }
}
