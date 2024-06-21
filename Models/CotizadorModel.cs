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

        // Constructor sin parámetros
        public CotizadorModel()
        {
            // Inicializa las listas
            Customers = new List<CustomerEntity>();
            Products = new List<LocalProductEntity>();
            Users = new List<UserEntity>();

            ProductDescription = ""; // Inicializa ProductDescription con una cadena vacía
        }

        // Constructor que recibe ApplicationDbContext como argumento
        public CotizadorModel(ApplicationDbContext context)
        {
            _context = context;
            Customers = new List<CustomerEntity>(); // Inicializa las listas
            Products = new List<LocalProductEntity>();
            Users = new List<UserEntity>();

            ProductDescription = ""; // Inicializa ProductDescription con una cadena vacía
        }

        // Propiedades para los datos del formulario de cotización
        [Required(ErrorMessage = "El campo Cliente es obligatorio.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Producto es obligatorio.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo Monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor que cero.")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DDate { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        public string? ProductDescription { get; set; }

        // Propiedades para llenar los dropdowns en la vista
        public List<CustomerEntity> Customers { get; set; }
        public List<LocalProductEntity> Products { get; set; }
        public List<UserEntity> Users { get; set; }

        // Método para cargar los datos necesarios para el formulario
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