using System;

namespace TrianglePegGameSolver.Web.Tests.Shared;

public class MediatorFixtureConfigurationException : Exception
{
    public MediatorFixtureConfigurationException() : base("MediatR was not properly initialized in the service collection")
    {
    }
}
