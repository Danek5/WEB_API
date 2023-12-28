using System.ComponentModel.DataAnnotations;

namespace WEB_API.Models.DTO
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
