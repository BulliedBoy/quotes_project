using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using quotes_project.Views.Home.Data.Entities;
using System;
using System.Linq;

namespace quotes_project.Views.Home.Data
{
    public static class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(), configuration);
            // Ensures the database is created
            context.Database.EnsureCreated();

            // Verificar si ya existen datos
            if (context.UserEntity.Any() || context.LocalProductEntity.Any() || context.CustomerEntity.Any())
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

            // Insertar datos estáticos en LocalProductoEntity
            context.LocalProductEntity.AddRange(
                new LocalProductEntity { ProductName = "Licencia Basica", AmountNormal = 2995.00, AmountOutsourcing = 3620.50 },
                new LocalProductEntity { ProductName = "Licencia Sencilla", AmountNormal = 1995.00, AmountOutsourcing = 00.00 },
                new LocalProductEntity { ProductName = "Contrato de Mantenimiento", AmountNormal = 960.00, AmountOutsourcing = 1440.00 },
                new LocalProductEntity { ProductName = "Capacitacion de uso de PayDay", AmountNormal = 00.00, AmountOutsourcing = 00.00 },
                new LocalProductEntity { ProductName = "Interfaz ACH Empleados", AmountNormal = 315.00, AmountOutsourcing = 472.50 },
                new LocalProductEntity { ProductName = "Interfaz ACH Acreedores", AmountNormal = 315.00, AmountOutsourcing = 472.50 },
                new LocalProductEntity { ProductName = "Enlace con el Mayor General", AmountNormal = 00.00, AmountOutsourcing = 00.00 },
                new LocalProductEntity { ProductName = "Interfaz Reloj de Marcacion", AmountNormal = 525.00, AmountOutsourcing = 787.00 },
                new LocalProductEntity { ProductName = "Modulo de Construccion", AmountNormal = 995.00, AmountOutsourcing = 1293.50 },
                new LocalProductEntity { ProductName = "Modulo de Desglose de Costos Por Actividades", AmountNormal = 550.00, AmountOutsourcing = 715.00 },
                new LocalProductEntity { ProductName = "Modulo Visa Sem", AmountNormal = 1545.00, AmountOutsourcing = 2008.50 },
                new LocalProductEntity { ProductName = "Usuario Local", AmountNormal = 595.00, AmountOutsourcing = 892.50 },
                new LocalProductEntity { ProductName = "Alquiler de Usuario Remoto", AmountNormal = 00.00, AmountOutsourcing = 00.00 },
                new LocalProductEntity { ProductName = "Compañia Adicional", AmountNormal = 495.00, AmountOutsourcing = 742.50 },
                new LocalProductEntity { ProductName = "Reutilizar Compañia", AmountNormal = 00.00, AmountOutsourcing = 135.00 },
                new LocalProductEntity { ProductName = "Restauracion de oportunidades de Llave", AmountNormal = 195.00, AmountOutsourcing = 253.50 },
                new LocalProductEntity { ProductName = "Cambio de Llave Paralela a USB", AmountNormal = 395.00, AmountOutsourcing = 493.75 },
                new LocalProductEntity { ProductName = "Reemplazo de Llave USB Robada", AmountNormal = 545.00, AmountOutsourcing = 708.50 },
                new LocalProductEntity { ProductName = "Reemplazo de Llave USB Deteriorada", AmountNormal = 395.00, AmountOutsourcing = 513.50 },
                new LocalProductEntity { ProductName = "Alquiler de Llave Mensual", AmountNormal = 395.00, AmountOutsourcing = 00.00 },
                new LocalProductEntity { ProductName = "Importacion Recurrente de Otros Ingresos", AmountNormal = 295.00, AmountOutsourcing = 395.00 },
                new LocalProductEntity { ProductName = "Importacion de Cuentas Bancarias", AmountNormal = 295.00, AmountOutsourcing = 295.00 },
                new LocalProductEntity { ProductName = "Importacion Inicial de Datos", AmountNormal = 425.00, AmountOutsourcing = 552.50 },
                new LocalProductEntity { ProductName = "Importacion Recurrente de Empleados", AmountNormal = 695.00, AmountOutsourcing = 903.50 },
                new LocalProductEntity { ProductName = "Importacion de Cuentas Contables y sus Asignaciones de Centro de Costo", AmountNormal = 495.00, AmountOutsourcing = 487.50 },
                new LocalProductEntity { ProductName = "Cambio de Codigos de Empleado", AmountNormal = 395.00, AmountOutsourcing = 595.50 },
                new LocalProductEntity { ProductName = "Proceso de trasladar empleados a una empresa nueva", AmountNormal = 495.00, AmountOutsourcing = 642.50 },
                new LocalProductEntity { ProductName = "Proceso de trasladar empleados a una empresa existente", AmountNormal = 895.00, AmountOutsourcing = 1163.50 },
                new LocalProductEntity { ProductName = "Visita en Oficinas del Cliente", AmountNormal = 00.00, AmountOutsourcing = 00.00 },
                new LocalProductEntity { ProductName = "Conexion Remota", AmountNormal = 65.00, AmountOutsourcing = 65.00 },
                new LocalProductEntity { ProductName = "Llamada por incidencia", AmountNormal = 45.00, AmountOutsourcing = 45.00 },
                new LocalProductEntity { ProductName = "Restauracion de contraseña de ususario", AmountNormal = 65.00, AmountOutsourcing = 65.00 },
                new LocalProductEntity { ProductName = "Reinstalacion", AmountNormal = 195.00, AmountOutsourcing = 195.00 }
            // Agregar más productos aquí
            );

            // Insertar datos estáticos en CustomerEntity
            context.CustomerEntity.AddRange(
                new CustomerEntity { CustomerName = "RICAMAR", CustomerType = "Normal", LicenceType = "Local" },
                new CustomerEntity { CustomerName = "DELOITTE", CustomerType = "Normal", LicenceType = "Local" },
                new CustomerEntity { CustomerName = "DIC, S.A.", CustomerType = "Normal", LicenceType = "Local" },
                new CustomerEntity { CustomerName = "PRICE WATERHOUSE COOPERS", CustomerType = "Outsourcing", LicenceType = "Local" },
                new CustomerEntity { CustomerName = "BDO", CustomerType = "Outsourcing", LicenceType = "Local" },
                new CustomerEntity { CustomerName = "ESTRATEGO", CustomerType = "Outsourcing", LicenceType = "Local" }
            // Agregar más clientes aquí
            );

            // Guardar cambios en UserEntity, LocalProductoEntity y CustomerEntity
            context.SaveChanges();
        }
    }
}