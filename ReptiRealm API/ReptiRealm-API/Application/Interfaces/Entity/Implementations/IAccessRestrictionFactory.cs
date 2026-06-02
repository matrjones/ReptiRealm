using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;

namespace ReptiRealm_API.Application.Interfaces.Entity.Implementations
{
    public interface IAccessRestrictionFactory
    {
        IAccessRestrictionMethod<T> Create<T>(AccessRestrictionDescriptor accessRestriction) where T : class;
    }
}
