namespace quotes_project.Views.Home.Data.Entities
{
    public class LocalProductEntity
    {
        public int Id { get; set; }

        public string? Product { get; set; }

        public decimal AmountNormal { get; set; }

        public decimal AmountOutsourcing { get; set; }

        public string? PDescription { get; set; }
    }
}