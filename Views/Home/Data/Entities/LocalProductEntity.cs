namespace quotes_project.Views.Home.Data.Entities
{
    public partial class LocalProductEntity
    {
        public int IdProduct { get; set; }

        public string? ProductName { get; set; }

        public decimal AmountNormal { get; set; }

        public decimal AmountOutsourcing { get; set; }

        public string? ProductDescription { get; set; } // Nueva propiedad para la descripción
    }
}