using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWorkWithDapper.Core
{
    /// <summary>
    /// The interface of unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save changes into context.
        /// </summary>
        void SaveChanges();
    }
}