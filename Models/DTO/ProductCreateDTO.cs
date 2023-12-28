using System.ComponentModel.DataAnnotations;

namespace WEB_API.Models.DTO
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string IsActive { get; set; }
    }
}
