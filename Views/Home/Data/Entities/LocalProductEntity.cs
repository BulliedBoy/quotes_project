namespace quotes_project.Views.Home.Data.Entities
{
    public partial class LocalProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal AmountNormal { get; set; }
        public decimal AmountOutsourcing { get; set; }
        public string? Description { get; set; }
    }
}