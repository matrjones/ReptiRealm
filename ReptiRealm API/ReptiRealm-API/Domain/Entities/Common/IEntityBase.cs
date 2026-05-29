namespace ReptiRealm_API.Domain.Entities.Common
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        string? CreatedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
        string? LastModifiedBy { get; set; }
    }
}