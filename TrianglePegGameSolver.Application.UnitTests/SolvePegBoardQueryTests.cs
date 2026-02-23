using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TrianglePegGameSolver.Application.UnitTests.Shared;
using TrianglePegGameSolver.Web.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Web.Domain;

namespace TrianglePegGameSolver.Application.UnitTests;

public class SolvePegBoardQueryTests
{
    private ApplicationFixture _appFixture;

    [SetUp]
    public void Setup()
    {
        _appFixture = new ApplicationFixture();
    }

    [Test]
    public async Task ShouldHaveBoardsOnMoves_WhenTheBoardIsFilled()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes[0].Filled = false;

        var result = await _appFixture.SendAsync(new SolvePegBoardQuery
        {
            PegBoard = pegBoard,
        });

        result.Should().NotBeNull();
        result.Moves.Should().NotBeNullOrEmpty();
        result.Moves.ForEach(x => x.Board.Should().NotBeNull());
    }

    [Test]
    public async Task ShouldIndicateSuccessfulSolve_WhenBoardIsFilled()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes[0].Filled = false;

        var result = await _appFixture.SendAsync(new SolvePegBoardQuery
        {
            PegBoard = pegBoard,
        });

        result.Should().NotBeNull();
        result.SuccessfullySolved.Should().BeTrue();
    }

    [Test]
    public async Task ShouldReturnTheCorrectNumberOfMoves_WhenTheBoardIsFilled()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes[0].Filled = false;

        var result = await _appFixture.SendAsync(new SolvePegBoardQuery
        {
            PegBoard = pegBoard,
        });

        result.Should().NotBeNull();
        result.Moves.Should().NotBeNullOrEmpty();
        result.Moves.Should().HaveCount(13);
    }

    [Test]
    public async Task ShouldReturnWithFailedToSolve_WhenTheBoardIsUnSolvable()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes.ForEach(x => x.Filled = false);
        pegBoard.Holes[2].Filled = true;
        pegBoard.Holes[3].Filled = true;
        pegBoard.Holes[12].Filled = true;
        pegBoard.Holes[14].Filled = true;

        var result = await _appFixture.SendAsync(new SolvePegBoardQuery
        {
            PegBoard = pegBoard,
        });

        result.Should().NotBeNull();
        result.Moves.Should().BeEmpty();
        result.SuccessfullySolved.Should().BeFalse();
    }

    [Test]
    public async Task ShouldReturnSolvedMoves_WhenBoardIsInProgress()
    {
        var pegBoard = new PegBoard();
        pegBoard.Holes[7].Filled = false;
        pegBoard.Holes[11].Filled = false;

        var result = await _appFixture.SendAsync(new SolvePegBoardQuery
        {
            PegBoard = pegBoard,
        });

        result.Should().NotBeNull();
        result.Moves.Should().NotBeNullOrEmpty();
        result.Moves.Should().HaveCount(12);
    }
}
