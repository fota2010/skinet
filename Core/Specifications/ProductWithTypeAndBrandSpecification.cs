using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
      
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams) 
                    :base ( x=> 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            AddInclude(p=>p.ProductType);
            AddInclude(p=>p.ProductBrand);
            AddOrderBy(p => p.Name);
            ApplyPagging(productParams.PageSize * (productParams.PageIndex -1) , productParams.PageSize );

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    
                    case "priceDesc":
                        AddOrderDesc(p=>p.Price);
                        break;
                    
                    default:
                     AddOrderBy(p => p.Name);
                     break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id) 
         : base(p=>p.Id == id)
        {
            AddInclude(p=>p.ProductType);
            AddInclude(p=>p.ProductBrand);
        }
    }
}