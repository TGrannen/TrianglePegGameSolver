using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TrianglePegGameSolver.Application.UnitTests.Shared;
using TrianglePegGameSolver.Web.Application.Play.Command.MakeMove;
using TrianglePegGameSolver.Web.Domain;

namespace TrianglePegGameSolver.Application.UnitTests;

public class MakeMoveCommandTests
{
    private ApplicationFixture _appFixture;

    [SetUp]
    public void Setup()
    {
        _appFixture = new ApplicationFixture();
    }

    [Test]
    public async Task ShouldBeValidMove_WhenOnePegIsRemoved()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes[0].Filled = false;

        var result = await _appFixture.SendAsync(new MakeMoveCommand
        {
            PegBoard = pegBoard,
            From = pegBoard.Holes[3],
            To = pegBoard.Holes[0]
        });

        result.Should().NotBeNull();
        result.IsValidMove.Should().BeTrue();
    }

    [Test]
    public async Task ShouldHaveNewBoardWithRemovedPeg_WhenOnePegIsRemoved()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes[0].Filled = false;

        var result = await _appFixture.SendAsync(new MakeMoveCommand
        {
            PegBoard = pegBoard,
            From = pegBoard.Holes[3],
            To = pegBoard.Holes[0]
        });

        result.Should().NotBeNull();
        result.NewBoard.Holes[0].Filled.Should().BeTrue();
        result.NewBoard.Holes[1].Filled.Should().BeFalse();
        result.NewBoard.Holes[3].Filled.Should().BeFalse();
    }
}
