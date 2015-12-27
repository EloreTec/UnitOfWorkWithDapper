using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWorkWithDapper.Core
{
    /// <summary>
    /// The interface of factory of unit of work.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates an unit of work.
        /// </summary>
        /// <returns>The unit of work.</returns>
        IUnitOfWork Create();
    }
}