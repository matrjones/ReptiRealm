namespace ReptiRealm_API.Application.Interfaces.Entity
{
    public class AccessRestrictionDescriptor
    {
        public object? Instance { get; }
        public Type? ServiceType { get; }
        public AccessRestrictionDescriptor(object? instance)
            => Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        public AccessRestrictionDescriptor(Type serviceType)
            => ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
    }
}
