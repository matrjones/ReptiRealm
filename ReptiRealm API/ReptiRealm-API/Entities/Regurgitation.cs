using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class Regurgitation : BaseEntity
    {
        public required string Notes { get; set; }

        #region Navigation Properties
        public required virtual Feed Feeds { get; set; }
        #endregion
    }
}
