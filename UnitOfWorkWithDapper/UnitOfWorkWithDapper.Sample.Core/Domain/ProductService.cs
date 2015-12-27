using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWorkWithDapper.Sample.Core.Domain
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product Save(Product product)
        {
            // if is new product
            if (product.Id > 0)
            {
                // update data
                _productRepository.Update(product);
            }
            else
            {
                // insert data
                _productRepository.Insert(product);
            }

            return product;
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }
    }
}