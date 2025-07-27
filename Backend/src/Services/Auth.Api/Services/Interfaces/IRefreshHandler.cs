namespace Auth.Api.Services.Interfaces
{
    public interface IRefreshHandler
    {
        Task<string> GenerateToken(string Username);
    }
}
