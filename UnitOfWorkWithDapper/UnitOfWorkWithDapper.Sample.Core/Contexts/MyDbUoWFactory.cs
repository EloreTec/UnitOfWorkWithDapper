using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfWorkWithDapper.Core;

namespace UnitOfWorkWithDapper.Sample.Core.Contexts
{
    /// <summary>
    /// Represents a factory of unit of work implementation.
    /// </summary>
    public class MyDbUoWFactory : IUnitOfWorkFactory
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// Creates a new instance, with context object.
        /// </summary>
        /// <param name="context">The context object.</param>
        public MyDbUoWFactory(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an unit of work.
        /// </summary>
        /// <returns>The unit of work.</returns>
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_context);
        }
    }
}