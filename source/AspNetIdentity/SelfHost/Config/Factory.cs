﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.Services.InMemory;
using Thinktecture.IdentityServer.Host.Config;

namespace SelfHost.Config
{
    class Factory
    {
        public static IdentityServerServiceFactory Configure(string connString)
        {
            var factory = new IdentityServerServiceFactory();

            factory.UserService = Registration<IUserService>.RegisterFactory((resolver) => AspNetIdentityUserServiceFactory.Factory(connString));

            var scopeStore = new InMemoryScopeStore(Scopes.Get());
            factory.ScopeStore = Registration.RegisterFactory<IScopeStore>((resolver) => scopeStore);
            var clientStore = new InMemoryClientStore(Clients.Get());
            factory.ClientStore = Registration.RegisterFactory<IClientStore>((resolver) => clientStore);

            return factory;
        }
    }
}
