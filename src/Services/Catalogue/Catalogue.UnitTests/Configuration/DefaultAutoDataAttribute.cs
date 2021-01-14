using AutoFixture;
using AutoFixture.Xunit2;

namespace Catalogue.UnitTests.Configuration
{
    public class DefaultAutoDataAttribute : AutoDataAttribute
    {
        public DefaultAutoDataAttribute()
          : base(() => new Fixture().Customize(new IgnoreVirtualMembersCustomisation()))
        {
        }
    }
}
