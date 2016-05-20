namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class Consent
    {
        public virtual string SubjectId { get; set; }
        
        public virtual string ClientId { get; set; }

        public virtual string Scopes { get; set; }
    }
}