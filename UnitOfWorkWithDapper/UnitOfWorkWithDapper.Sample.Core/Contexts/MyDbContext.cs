using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using UnitOfWorkWithDapper.Core;

namespace UnitOfWorkWithDapper.Sample.Core.Contexts
{
    /// <summary>
    /// Represents a database context implementation.
    /// </summary>
    public class MyDbContext : DapperContext
    {
        /// <summary>
        /// Creates new instance of context, initiating a new connection to the database
        /// </summary>
        public MyDbContext()
        {
        }

        /// <summary>
        /// Creates connection object to database. This method is called in creating the instance of context.
        /// </summary>
        /// <returns>The connection object.</returns>
        protected override IDbConnection CreateConnection()
        {
            var config = ConfigurationManager.ConnectionStrings["Default"];
            var factory = DbProviderFactories.GetFactory(config.ProviderName);

            var cnn = factory.CreateConnection();
            cnn.ConnectionString = config.ConnectionString;

            // opens connection
            cnn.Open();

            return cnn;
        }
    }
}