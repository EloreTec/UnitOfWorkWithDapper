using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfWorkWithDapper.Sample.Core.Domain;

namespace UnitOfWorkWithDapper.Sample.Core.AppService
{
    public interface IProductApp
    {
        IList<Product> GetAll();

        Product Save(Product product);

        Product GetById(int id);
    }
}