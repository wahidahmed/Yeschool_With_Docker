using Auth.Api.Data;
using Auth.Api.Data.Entties;
using Auth.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace Auth.Api.Services
{
    public class RefreshHandler : IRefreshHandler
    {
        private readonly AppDbContext context;
        public RefreshHandler(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var Existtoken = this.context.RefreshTokens.FirstOrDefaultAsync(item => item.Userid == username).Result;
                if (Existtoken != null)
                {
                    Existtoken.Refreshtoken = refreshtoken;
                }
                else
                {
                    await this.context.RefreshTokens.AddAsync(new RefreshToken
                    {
                        Userid = username,
                        Tokenid = new Random().Next().ToString(),
                        Refreshtoken = refreshtoken
                    });
                }
                await this.context.SaveChangesAsync();

                return refreshtoken;

            }
        }
    }
}
