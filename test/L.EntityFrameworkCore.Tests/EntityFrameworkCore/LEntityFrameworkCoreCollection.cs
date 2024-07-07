using Xunit;

namespace L.EntityFrameworkCore;

[CollectionDefinition(LTestConsts.CollectionDefinitionName)]
public class LEntityFrameworkCoreCollection : ICollectionFixture<LEntityFrameworkCoreFixture>
{

}
