using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWorkWithDapper.Sample.Core.Domain
{
    public interface IProductRepository
    {
        IList<Product> GetAll();

        void Insert(Product product);

        void Update(Product product);

        Product GetById(int id);
    }
}