namespace ReptiRealm_WebApp.Services.Api.Interfaces;

public interface IReptileApiService
{
    Task<List<ReptileCardModel>?> GetAllReptiles();
}
