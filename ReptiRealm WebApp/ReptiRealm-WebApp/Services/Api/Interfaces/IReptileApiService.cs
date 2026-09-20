using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Pages.Home.Models;
namespace ReptiRealm_WebApp.Services.Api.Interfaces;

public interface IReptileApiService
{
    Task<List<ReptileCardDto>?> GetAllReptiles();
    Task<Reptile> AddReptile(AddReptile reptile);
}
