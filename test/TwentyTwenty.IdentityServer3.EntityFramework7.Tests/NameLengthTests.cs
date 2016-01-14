using System.Linq;
using System.Reflection;
using TwentyTwenty.IdentityServer3.EntityFramework7.DbContexts;
using Xunit;

namespace TwentyTwenty.IdentityServer3.EntityFramework7.Tests
{
    public class NameLengthTests
    {
        [Fact]
        public void NamesAreNotMoreThan30Chars()
        {
            var assembly = Assembly.GetAssembly(typeof(ClientConfigurationContext<>));
            var query =
                from t in assembly.GetTypes()
                where t.Namespace == "IdentityServer3.EntityFramework.Entities"
                select t;
            foreach (var type in query)
            {
                Assert.True(type.Name.Length <= 30, type.Name);

                foreach (var prop in type.GetProperties())
                {
                    Assert.True(prop.Name.Length <= 30, type.Name + ". " + prop.Name);
                }
            }

        }
    }
}
