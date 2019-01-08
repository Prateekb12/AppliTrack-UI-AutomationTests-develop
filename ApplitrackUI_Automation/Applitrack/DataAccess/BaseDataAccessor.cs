using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recruit.Cache.Connections.Implementations;
using Recruit.Persistence;
using Recruit.Persistence.Implementations;

namespace ApplitrackUITests.DataAccess
{
    public abstract class BaseDataAccessor
    {
        protected readonly IReadWriteData _readWriteData;

        /// <summary>
        /// Provide the ability to perform CRUD operations for Notifications
        /// </summary>
        protected BaseDataAccessor()
        {
            var con = new ConnectionProvider();
            var constring = con.GetConnectionString("devraj"); //TODO: remove hardcoding
            _readWriteData = new OrmLiteRepositoryProvider(constring);
        }
    }
}
