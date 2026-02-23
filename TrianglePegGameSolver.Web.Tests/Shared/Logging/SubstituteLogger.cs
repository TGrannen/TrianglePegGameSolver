using System;
using Microsoft.Extensions.Logging;

namespace TrianglePegGameSolver.Web.Tests.Shared.Logging;

public abstract class SubstituteLogger : ILogger
{
    public abstract IDisposable BeginScope<TState>(TState state);

    public virtual bool IsEnabled(LogLevel logLevel) => true;

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        => Log(logLevel, formatter(state, exception));

    public abstract void Log(LogLevel logLevel, string message);
}
