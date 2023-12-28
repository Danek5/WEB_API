using WEB_API.Models;

namespace WEB_API.Data
{
    public static class ProductStore
    {
        public static List<Product> productList = new List<Product> { 
            new Product { Id = 1, Name="Apple", IsActive=true, Created=null, Updated=null},
            new Product { Id = 2, Name="Blueberry", IsActive=true, Created=null, Updated=null},
            new Product { Id = 3, Name="Citrus", IsActive=true, Created=null, Updated=null}

        };


    }
}
