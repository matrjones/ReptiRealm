using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;
using ReptiRealm_API.Application.Interfaces.Entity.Implementations;

namespace ReptiRealm_API.Application.Services.Entity.Implementations
{
    public class AccessRestrictionFactory(IServiceProvider serviceProvider) : IAccessRestrictionFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public IAccessRestrictionMethod<T> Create<T>(AccessRestrictionDescriptor accessRestriction) where T : class
            => (IAccessRestrictionMethod<T>)(accessRestriction.Instance ?? _serviceProvider.GetRequiredService(accessRestriction.ServiceType!));
    }
}
