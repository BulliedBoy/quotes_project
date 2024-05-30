using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;
using System;
using System.Linq;

public static class SeedDb
{
    public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(), configuration))
        {
            // Ensures the database is created
            context.Database.EnsureCreated();

            // Verificar si ya existen datos
            if (context.UserEntity.Any() || context.ProductEntity.Any() || context.CustomerEntity.Any())
            {
                return; // La DB ya ha sido poblada
            }

            // Insertar datos estáticos
            context.UserEntity.AddRange(
                new UserEntity { Username = "Rainier Rios" },
                new UserEntity { Username = "Daniel Ortiz" },
                new UserEntity { Username = "Maritza Bonilla" },
                new UserEntity { Username = "Ricardo Cogley" },
                new UserEntity { Username = "Massiel Moreno" },
                new UserEntity { Username = "Ariadni Cerrud" },
                new UserEntity { Username = "Jose Hernandez" },
                new UserEntity { Username = "Lourine Troncoso" },
                new UserEntity { Username = "Christopher Vanegas" },
                new UserEntity { Username = "Luz Pedrosa" },
                new UserEntity { Username = "Adrian Valor" },
                new UserEntity { Username = "Edwin Delgado" },
                new UserEntity { Username = "Jose Sieiro" },
                new UserEntity { Username = "Alcides Sanches" },
                new UserEntity { Username = "Alejandro Sosa" },
                new UserEntity { Username = "Erick Hernandez" }
            );

            // Insertar datos estáticos en ProductoEntity
            context.ProductEntity.AddRange(
                new ProductEntity { ProductName = "ACH EMPLEADOS", AmountNormal = 315.00, AmountOutsourcing = 400.00 }
                // Agregar más productos aquí
            );

            // Insertar datos estáticos en CustomerEntity
            context.CustomerEntity.AddRange(
                new CustomerEntity { CustomerName = "RICAMAR", CustomerType = "Normal"},
                new CustomerEntity { CustomerName = "DELOITTE", CustomerType = "Normal" },
                new CustomerEntity { CustomerName = "DIC, S.A.", CustomerType = "Normal" },
                new CustomerEntity { CustomerName = "PRICE WATERHOUSE COOPERS", CustomerType = "Outsourcing" },
                new CustomerEntity { CustomerName = "BDO", CustomerType = "Outsourcing" },
                new CustomerEntity { CustomerName = "ESTRATEGO", CustomerType = "Outsourcing" }
                // Agregar más clientes aquí
            );

            // Guardar cambios en UserEntity, ProductoEntity y CustomerEntity
            context.SaveChanges();
        }
    }
}
