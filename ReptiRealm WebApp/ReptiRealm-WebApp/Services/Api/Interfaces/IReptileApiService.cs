using ReptiRealm_WebApp.Pages.Home.Models;
namespace ReptiRealm_WebApp.Services.Api.Interfaces;

public interface IReptileApiService
{
    Task<List<ReptileCardDto>?> GetAllReptiles();
}
