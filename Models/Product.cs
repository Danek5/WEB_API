namespace WEB_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created {  get; set; }
        public DateTime? Updated { get; set;}
    }
}
