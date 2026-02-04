namespace ReptiRealm_WebApp.Models
{
    public class User
    {
        public required string Username { get; set; }
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        public virtual ICollection<Species> Species { get; set; } = new List<Species>();
        public virtual ICollection<FoodType> FoodTypes { get; set; } = new List<FoodType>();
    }
}
