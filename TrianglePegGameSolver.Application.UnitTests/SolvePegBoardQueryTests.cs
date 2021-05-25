using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Application.UnitTests.Shared;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Application.UnitTests
{
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
    }
}