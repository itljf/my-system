using L.Samples;
using Xunit;

namespace L.EntityFrameworkCore.Domains;

[Collection(LTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LEntityFrameworkCoreTestModule>
{

}
