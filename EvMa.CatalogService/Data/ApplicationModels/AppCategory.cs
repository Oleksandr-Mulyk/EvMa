using EvMa.CatalogService.Data.Models;

namespace EvMa.CatalogService.Data.ApplicationModels
{
    public class AppCategory : Category<AppProduct, ProductAttribute, AppAttributeSet, AppAttributeValue, Price, Image> { }
}
