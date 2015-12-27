using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfWorkWithDapper.Sample.Core.Contexts;
using UnitOfWorkWithDapper.Sample.Core.Domain;

namespace UnitOfWorkWithDapper.Sample.Core.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public IList<Product> GetAll()
        {
            return _context.Query<Product>("SELECT * FROM Products").ToList();
        }

        public void Insert(Product product)
        {
            _context.Execute("INSERT INTO Products VALUES (@Name, @Price)", product);
        }

        public void Update(Product product)
        {
            _context.Execute("UPDATE Products SET Name=@Name, Price=@Price WHERE Id=@Id", product);
        }

        public Product GetById(int id)
        {
            return _context.Query<Product>("SELECT * FROM Products WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }
    }
}