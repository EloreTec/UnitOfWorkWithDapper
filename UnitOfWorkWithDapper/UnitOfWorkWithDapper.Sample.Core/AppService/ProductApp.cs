using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfWorkWithDapper.Sample.Core.Contexts;
using UnitOfWorkWithDapper.Sample.Core.Domain;

namespace UnitOfWorkWithDapper.Sample.Core.AppService
{
    public class ProductApp : IProductApp
    {
        private readonly IProductService _productService;
        private readonly MyDbUoWFactory _uoWFactory;

        public ProductApp(IProductService productService, MyDbUoWFactory uoWFactory)
        {
            _productService = productService;
            _uoWFactory = uoWFactory;
        }

        public IList<Product> GetAll()
        {
            return _productService.GetAll();
        }

        public Product Save(Product product)
        {
            try
            {
                // Create unit of work (begins transaction)
                using (var uow = _uoWFactory.Create())
                {
                    // save product (with transaction)
                    _productService.Save(product);

                    // commit transation
                    // (ATTENTION: Call the SaveChanges method always the end of the scope of the unit.
                    // If not called, the Dispose of the unit of work will execute the transaction rollback.)
                    uow.SaveChanges();
                }

                return product;
            }
            catch (Exception ex)
            {
                // log error
                throw;
            }
        }

        public Product GetById(int id)
        {
            return _productService.GetById(id);
        }
    }
}