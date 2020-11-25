using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Sync;

namespace USP.Business.Extensions
{
    public class DBServerRegistrar : IServerRegistrar2
    {
        private readonly bool _isMaster;

        public DBServerRegistrar(bool isMaster)
        {
            _isMaster = isMaster;
        }

        public IEnumerable<IServerAddress> Registrations => Enumerable.Empty<IServerAddress>();

        public ServerRole GetCurrentServerRole()
        {
            return _isMaster ? ServerRole.Master : ServerRole.Slave;
        }

        public string GetCurrentServerUmbracoApplicationUrl()
        {
            return null;
        }
    }
}
