using TrianglePegGameSolver.Web.Application;

namespace TrianglePegGameSolver.Application.UnitTests.Shared;

public class ApplicationFixture : MediatorFixture
{
    public ApplicationFixture()
    {
        OnConfigureServices += (_, services) =>
        {
            services.AddApplication();
        };
    }
}
