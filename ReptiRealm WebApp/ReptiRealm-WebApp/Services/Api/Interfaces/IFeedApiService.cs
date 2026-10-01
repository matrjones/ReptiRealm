using ReptiRealm_WebApp.Models.DTOs;

namespace ReptiRealm_WebApp.Services.Api.Interfaces;

public interface IFeedApiService
{
    Task<List<string>?> GetAllAnimalTypes();
    Task<List<FoodType>?> GetAllAnimalTypeSizes(string animalType);
}
