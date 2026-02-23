using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TrianglePegGameSolver.Application.UnitTests.Shared.Logging;

namespace TrianglePegGameSolver.Application.UnitTests.Shared;

public class MediatorFixture
{
    public EventHandler<IServiceCollection> OnConfigureServices { get; set; }
    public ServiceProvider Provider { get; private set; }

    protected ServiceCollection Services { get; } = new ServiceCollection();

    public SubstituteLogger GetLogger<T>()
    {
        var mockedLoggerFactory = Provider.GetService<SubstituteLoggerFactory>();
        var mockLogger = mockedLoggerFactory.GetLogger<T>();
        return mockLogger;
    }

    public async Task<T> SendAsync<T>(IRequest<T> request)
    {
        if (Provider == null)
        {
            Services.AddSubstitutedLogging();
            OnConfigureServices?.Invoke(this, Services);
            Provider = Services.BuildServiceProvider();
        }

        var mediator = Provider.GetService<IMediator>();

        if (mediator == null)
        {
            throw new MediatorFixtureConfigurationException();
        }

        return await mediator.Send(request);
    }
}
