using EvMa.CatalogService.Data.Models;

namespace EvMa.CatalogService.Data.ApplicationModels
{
    public class AppProduct : Product<ProductAttribute, AppAttributeSet, AppAttributeValue, Price, Image> { }
}
