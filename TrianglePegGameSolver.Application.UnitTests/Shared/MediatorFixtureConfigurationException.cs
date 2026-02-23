using System;

namespace TrianglePegGameSolver.Application.UnitTests.Shared;

public class MediatorFixtureConfigurationException : Exception
{
    public MediatorFixtureConfigurationException() : base("MediatR was not properly initialized in the service collection")
    {
    }
}
