using Microsoft.AspNetCore.Builder;
using L;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<LWebTestModule>();

public partial class Program
{
}
