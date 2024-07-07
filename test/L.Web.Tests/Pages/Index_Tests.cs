using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace L.Pages;

public class Index_Tests : LWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
