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
                new UserEntity { Username = "Rainier Rios", Position = "Asistente - Sistemas e Infraestructura" },
                new UserEntity { Username = "Daniel Ortiz", Position = "Soporte Tecnico" },
                new UserEntity { Username = "Maritza Bonilla" , Position = "Gerente de Ventas" },
                new UserEntity { Username = "Ricardo Cogley" , Position = "Asistente de Ventas" },
                new UserEntity { Username = "Massiel Moreno" , Position = "Soporte Tecnico" },
                new UserEntity { Username = "Ariadni Cerrud" , Position = "Soporte Tecnico" },
                new UserEntity { Username = "Jose Hernandez" , Position = "Soporte Tecnico" },
                new UserEntity { Username = "Lourine Troncoso" , Position = "Gerente de Atencion al Cliente" },
                new UserEntity { Username = "Christopher Vanegas" , Position = "Director de Operaciones e Infraestructura" },
                new UserEntity { Username = "Luz Pedrosa" , Position = "Gerente Administrativa" },
                new UserEntity { Username = "Adrian Valor" , Position = "Analista Programador" },
                new UserEntity { Username = "Edwin Delgado" , Position = "Analista Programador" },
                new UserEntity { Username = "Jose Sieiro" , Position = "Analista Programador" },
                new UserEntity { Username = "Alcides Sanches" , Position = "Director de Desarrollo e Innovacion" },
                new UserEntity { Username = "Alejandro Sosa" , Position = "Analista Programador" },
                new UserEntity { Username = "Erick Hernandez" , Position = "Gerente General" }
            );

            // Esta tabla debe ser modificada para 1 tipo de cotizacion el cual le llamaremos cotizacion principal
            context.LocalProductEntity.AddRange(
                //new LocalProductEntity { ProductName = "Licencia Basica", AmountNormal = 2995.00M, AmountOutsourcing = 3620.50M, Description = "Licencia basica 5 Cias, 1 usuario local, 3 personas para capacitacion y sin limite de empelados." },
                //new LocalProductEntity { ProductName = "Licencia Sencilla", AmountNormal = 1995.00M, AmountOutsourcing = 0.00M, Description = "Licencia basica 1 Cias, 1 usuario local, 1 persona para capacitacion y sin limite de empelados." },
                //new LocalProductEntity { ProductName = "Contrato de Mantenimiento", AmountNormal = 960.00M, AmountOutsourcing = 1440.00M, Description = "Contrato de mantenimiento de PayDay - Obligatorio el primer año de licenciamiento." },
                new LocalProductEntity { ProductName = "Capacitacion de uso de PayDay", AmountNormal = 0.00M, AmountOutsourcing = 0.00M, ProductDescription = "Doce (12) horas de Capacitación presencial en el uso de PayDay, para una (1) persona, la cual se dictará en cuatro (4) sesiones de tres (3) horas cada una, en las oficinas de Sosa y Cía., S. A., de acuerdo con la fecha y horario estipulados." },
                new LocalProductEntity { ProductName = "Interfaz ACH Empleados", AmountNormal = 315.00M, AmountOutsourcing = 472.50M, ProductDescription = "Interfaz de pago a los empleados por ACH." },
                new LocalProductEntity { ProductName = "Interfaz ACH Acreedores", AmountNormal = 315.00M, AmountOutsourcing = 472.50M, ProductDescription = "Interfaz de pago a los acreedores por medio de ACH." },
                //new LocalProductEntity { ProductName = "Enlace con el Mayor General", AmountNormal = 0.00M, AmountOutsourcing = 0.00M, Description = "" },
                new LocalProductEntity { ProductName = "Interfaz Reloj de Marcacion", AmountNormal = 525.00M, AmountOutsourcing = 787.00M, ProductDescription = "Interfaz con el Reloj de Marcación o Software de control de Asistencia en formato Resumen o diario." },
                new LocalProductEntity { ProductName = "Modulo de Construccion", AmountNormal = 995.00M, AmountOutsourcing = 1293.50M, ProductDescription = "Módulo de Construcción / Desglose de costos por Actividades. " },
                new LocalProductEntity { ProductName = "Modulo de Desglose de Costos Por Actividades", AmountNormal = 550.00M, AmountOutsourcing = 715.00M, ProductDescription = "Desglose de costos por Actividades, que permite desglosar la jornada laboral de los colaboradores pactados por hora (el desglose, es exclusivo para aquellos empleados que estén pactados por hora y se desee desglosar el día de trabajo o las jornadas a diferentes proyectos y/o fases)." },
                new LocalProductEntity { ProductName = "Modulo Visa Sem", AmountNormal = 1545.00M, AmountOutsourcing = 2008.50M, ProductDescription = "Módulo VISA SEM, que incluye:\r\n• Pago de Planillas Regulares, Vacaciones, XIII Mes, Liquidaciones y Extraordinarias, como se establece en artículo 34 de la Ley 41 de 24 de agosto de 2007 según gaceta oficial No. 25864. El ISR donde el personal extranjero de la empresa SEM con Visa de Personal Permanente de SEM no generará impuesto sobre la renta, siempre y cuando reciban sus ingresos directamente de sus casas matrices en el extranjero.\r\n• Voucher especial.\r\n• Informe especial con el detalle de algunos de los ingresos percibidos en las planillas por rango de fechas." },
                new LocalProductEntity { ProductName = "Usuario Local", AmountNormal = 595.00M, AmountOutsourcing = 892.50M, ProductDescription = "Licencia para incluir un usuario (1PC) para ejecutar PayDay en la terminal local." },
                new LocalProductEntity { ProductName = "Alquiler de Usuario Remoto", AmountNormal = 0.00M, AmountOutsourcing = 0.00M, ProductDescription = " Una (1) licencia de acceso remoto temporal adicional, para ejecutar PayDay a través de terminal server o similar. Este periodo de alquiler será por X (00) meses a partir de la fecha de la configuración de la llave de PayDay." },
                new LocalProductEntity { ProductName = "Compañia Adicional", AmountNormal = 495.00M, AmountOutsourcing = 742.50M, ProductDescription = "Licencia para incluir una (1) compañía adicional, sin límite de empleados en PayDay." },
                new LocalProductEntity { ProductName = "Reutilizar Compañia", AmountNormal = 0.00M, AmountOutsourcing = 135.00M, ProductDescription = "• Proceso especial de reasignar el espacio de la compañía XXXXXXXXXXX, para ser utilizado por una nueva compañía, que se llamará XXXXXXXXXXX, S. A.\r\n• El cliente acepta que no tendrá acceso para revisar los datos de la compañía XXXXXXXXXXXXX, a menos que vuelva a solicitar este mismo servicio, cuyo precio puede variar. \r\n• El Cliente realizará un respaldo de archivos a la compañía XXXXXXXXXXX antes de la conexión remota." },
                new LocalProductEntity { ProductName = "Restauracion de oportunidades de Llave", AmountNormal = 195.00M, AmountOutsourcing = 253.50M, ProductDescription = "Restaurar cambios de oportunidades de la llave HASP de Puerto USB." },
                new LocalProductEntity { ProductName = "Cambio de Llave Paralela a USB", AmountNormal = 395.00M, AmountOutsourcing = 493.75M, ProductDescription = "Cambio de llave HASP de puerto paralelo a USB." },
                new LocalProductEntity { ProductName = "Reemplazo de Llave USB Robada", AmountNormal = 545.00M, AmountOutsourcing = 708.50M, ProductDescription = "Reemplazo de llave HASP USB robada.(El cliente debe traer la denuncia, sino debe pagar la licencia completa)" },
                new LocalProductEntity { ProductName = "Reemplazo de Llave USB Deteriorada", AmountNormal = 395.00M, AmountOutsourcing = 513.50M, ProductDescription = "Reemplazo de llave HASP USB deteriorada.(Por daño. EL cliente debe traer la llave deteriorada, sino debe pagar la licencia completa)" },
                //new LocalProductEntity { ProductName = "Alquiler de Llave Mensual", AmountNormal = 395.00M, AmountOutsourcing = 0.00M, ProductDescription = "Aqluiler de llave mensual." },
                new LocalProductEntity { ProductName = "Importacion Recurrente de Otros Ingresos", AmountNormal = 295.00M, AmountOutsourcing = 395.00M, ProductDescription = "Importación Recurrente de Otros Ingresos en planillas regulares y extraordinarias, de acuerdo con archivo adjunto." },
                new LocalProductEntity { ProductName = "Importacion de Cuentas Bancarias", AmountNormal = 295.00M, AmountOutsourcing = 295.00M, ProductDescription = "Proceso especial para una (0) compañía, en donde se importará al campo de número de cuenta de los datos generales de empleados, el número de cuenta bancaria de cada colaborador, de acuerdo con el listado enviado por el cliente que contendrá:\r\n- Código del empleado\r\n- Número de cuenta bancaria del empleado\r\n- Ruta/Tránsito del Banco del empleado." },
                new LocalProductEntity { ProductName = "Importacion Inicial de Datos", AmountNormal = 425.00M, AmountOutsourcing = 552.50M, ProductDescription = "Importación de datos iniciales e historia anterior para una (0) compañía, a partir del formato proporcionado al cliente (Empleados e Ingresos)." },
                new LocalProductEntity { ProductName = "Importacion Recurrente de Empleados", AmountNormal = 695.00M, AmountOutsourcing = 903.50M, ProductDescription = "Proceso especial para importar empleados de forma recurrente hacia PayDay por medio de un archivo de texto o CSV, de acuerdo con la estructura entregada por Sosa y Cia., S. A. El usuario cargará el archivo a la plataforma de PayDay, previo a ejecutar la opción desde PayDay." },
                new LocalProductEntity { ProductName = "Importacion de Cuentas Contables y sus Asignaciones de Centro de Costo", AmountNormal = 495.00M, AmountOutsourcing = 487.50M, ProductDescription = "Importación de cuentas contables y asignación a los centros de costos para 0 Compañías \r\nLos datos deben ser entregados por el Cliente de acuerdo con estructura y tipo de archivo suministrado por Sosa y Cía., S. A.." },
                new LocalProductEntity { ProductName = "Cambio de Codigos de Empleado", AmountNormal = 395.00M, AmountOutsourcing = 595.50M, ProductDescription = "Proceso especial para cambiar el código de empleado a todos los colaborador activos de una compañía, de acuerdo con archivo proporcionado por el cliente." },
                new LocalProductEntity { ProductName = "Proceso de trasladar empleados a una empresa nueva", AmountNormal = 495.00M, AmountOutsourcing = 642.50M, ProductDescription = "Proceso especial para trasladar información de trabajadores de tres compañías existentes hacia tres (0) nuevas compañías, con su historial de salarios, deudas y acumulados." },
                new LocalProductEntity { ProductName = "Proceso de trasladar empleados a una empresa existente", AmountNormal = 895.00M, AmountOutsourcing = 1163.50M, ProductDescription = "Proceso especial para trasladar la información de algunos trabajadores de la compañía XXXXXXXXXXXX, hacia otra existente con su historial de salarios y acumulados." },
                new LocalProductEntity { ProductName = "Visita", AmountNormal = 0.00M, AmountOutsourcing = 0.00M, ProductDescription = "Dos (2) horas de visita en las oficinas de XXXXXXXXXXXX, para asesoría en el sistema de PayDay ." },
                //new LocalProductEntity { ProductName = "Conexion Remota", AmountNormal = 65.00M, AmountOutsourcing = 65.00M, Description = "" },
                //new LocalProductEntity { ProductName = "Llamada por incidencia", AmountNormal = 45.00M, AmountOutsourcing = 45.00M, Description = "" },
                //new LocalProductEntity { ProductName = "Restauracion de contraseña de ususario", AmountNormal = 65.00M, AmountOutsourcing = 65.00M, ProductDescription = "Restauacion de la contraseña de un usuario de PayDay" },
                new LocalProductEntity { ProductName = "Reinstalacion", AmountNormal = 195.00M, AmountOutsourcing = 195.00M, ProductDescription = "Servicio de reinstalación de PayDay en un equipo local. (Para realizar este proceso el Cliente contará con una copia íntegra de los datos y con la llave conectada al nuevo equipo.)" }
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