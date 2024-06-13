using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace quotes_project.Models
{
    public class CotizadorModel
    {
        private ApplicationDbContext context;

        public CotizadorModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<CustomerEntity> CustomerEntity { get; set; }
        public List<LocalProductEntity> LocalProductEntity { get; set; }
        public List<UserEntity> UserEntity { get; set; }

        // Propiedades para los datos del formulario de cotización
        [Required(ErrorMessage = "El campo Cliente es obligatorio.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Producto es obligatorio.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo Monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor que cero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        public DateTime DDate { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public int UserId { get; set; }

        internal void LoadData()
        {
            this.CustomerEntity = context.CustomerEntity.ToList();
            this.LocalProductEntity = context.LocalProductEntity.ToList();
            this.UserEntity = context.UserEntity.ToList();
        }

    }
}
