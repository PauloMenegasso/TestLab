using Microsoft.AspNetCore.Mvc.Testing;

namespace PersonCRUD.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

        });

        builder.UseEnvironment("Tests");
    }
}