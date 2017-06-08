using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using HolographicShop.DataObjects;
using HolographicShop.Models;
using HolographicShop.Swagger;
using Owin;
using Swashbuckle.Application;

namespace HolographicShop
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            // Swagger
            var zumoApiHeader = new SwaggerHeaderParameters
            {
                Description = "The default header for app services defining their version",
                Key = "ZUMO-API-VERSION",
                Name = "ZUMO-API-VERSION",
                DefaultValue = "2.0.0"
            };
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            config.EnableSwagger(x =>
            {
                x.SingleApiVersion("v1", "HolographicSwagger");
                zumoApiHeader.Apply(x);
            }).EnableSwaggerUi();

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            //context.Categories.AddOrUpdate(c=> c.Id, 
            //    new Category {Title = "Laptop", Description = "Laptop"},
            //    new Category { Title = "Desktop", Description = "Desktop" },
            //    new Category { Title = "Tablet", Description = "Tablet" },
            //    new Category { Title = "Screen", Description = "Screen" });
            //context.Products.AddOrUpdate(p => p.Id,
            //   new Product { Title = "LENOVO Ideapad", Description = "Laptop",
            //       ModelURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG", ThumbnailURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG",
            //       Price = 449.99, Category = context.Categories.FirstOrDefault(x=>x.Title=="Laptop")
            //   },
            //   new Product { Title = "HP Pavilion", Description = "Desktop",
            //       ModelURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG", ThumbnailURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG",
            //       Price = 649.00, Category = context.Categories.FirstOrDefault(x => x.Title == "Desktop")
            //   },
            //   new Product { Title = "LENOVO Tab", Description = "Tablet",
            //       ModelURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG", ThumbnailURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG",
            //       Price = 74.90, Category = context.Categories.FirstOrDefault(x => x.Title == "Tablet")
            //   },
            //   new Product { Title = "PHILIPS 223V5LSB2", Description = "Screen",
            //       ModelURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG", ThumbnailURL = "https://holographicstorage.blob.core.windows.net/images/DSC00217.JPG",
            //       Price = 99.00, Category = context.Categories.FirstOrDefault(x => x.Title == "Screen")
            //   });
            //context.SaveChanges();
        }
    }
}

