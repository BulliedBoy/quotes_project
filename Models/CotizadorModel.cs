using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace quotes_project.Models
{
    public class CotizadorModel
    {
        private readonly ApplicationDbContext? _context;

        // Constructor sin par�metros
        public CotizadorModel()
        {
            // Inicializa las listas
            Customers = new List<CustomerEntity>();
            Products = new List<LocalProductEntity>();
            Users = new List<UserEntity>();

            ProductDescription = ""; // Inicializa ProductDescription con una cadena vac�a
        }

        // Constructor que recibe ApplicationDbContext como argumento
        public CotizadorModel(ApplicationDbContext context)
        {
            _context = context;
            Customers = new List<CustomerEntity>(); // Inicializa las listas
            Products = new List<LocalProductEntity>();
            Users = new List<UserEntity>();

            ProductDescription = ""; // Inicializa ProductDescription con una cadena vac�a
        }

        // Constructor que recibe ApplicationDbContext y una entidad de cotizaci�n como argumento
        public CotizadorModel(ApplicationDbContext context, QuoteEntity quote)
        {
            _context = context;
            Customers = context.CustomerEntity.ToList();
            Products = context.LocalProductEntity.ToList();
            Users = context.UserEntity.ToList();

            // Inicializa las propiedades del modelo con los datos de la cotizaci�n
            Id = quote.Id; // Aqu� a�adimos la propiedad Id
            Id = quote.Id;
            Id = quote.Id;
            Amount = quote.Amount;
            DDate = quote.DDate;
            UserId = quote.Id;
            Description = quote.Description;
        }

        // Propiedades para los datos del formulario de cotizaci�n
        public int Id { get; set; } // A�adimos la propiedad Id

        [Required(ErrorMessage = "El campo Cliente es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Producto es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor que cero.")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DDate { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo Descripci�n es obligatorio.")]
        public string? ProductDescription { get; set; }

        // Propiedades para llenar los dropdowns en la vista
        public List<CustomerEntity> Customers { get; set; }
        public List<LocalProductEntity> Products { get; set; }
        public List<UserEntity> Users { get; set; }

        // M�todo para cargar los datos necesarios para el formulario
        public void LoadData()
        {
            if (_context != null)
            {
                Customers = _context.CustomerEntity.ToList();
                Products = _context.LocalProductEntity.ToList();
                Users = _context.UserEntity.ToList();
            }
        }
    }
}
