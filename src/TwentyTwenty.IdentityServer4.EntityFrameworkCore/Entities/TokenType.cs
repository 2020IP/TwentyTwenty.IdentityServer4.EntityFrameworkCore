namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public enum TokenType : short
    {
        AuthorizationCode = 1,
        TokenHandle = 2,
        RefreshToken = 3
    }
}