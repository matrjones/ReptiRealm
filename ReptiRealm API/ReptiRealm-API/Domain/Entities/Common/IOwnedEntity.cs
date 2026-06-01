namespace ReptiRealm_API.Domain.Entities.Common
{
    // Marker interface for entities that are owned by a user
    public interface IOwnedEntity
    {
        string UserId { get; set; }
    }
}
