using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWorkWithDapper.Sample.Core.Domain
{
    public interface IProductService
    {
        IList<Product> GetAll();

        Product Save(Product product);

        Product GetById(int id);
    }
}