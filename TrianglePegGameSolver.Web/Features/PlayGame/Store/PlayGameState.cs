using Fluxor;
using System.Collections.Generic;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store
{
    public record PlayGameState
    {
        public Domain.PegBoard Board { get; init; }
        public Stack<PegMoveWithBoard> Moves { get; init; }
        public PegHole From { get; init; }
        public PegHole To { get; init; }
        public bool StartingHoleSelected { get; init; }
    }

    public class PlayGameFeature : Feature<PlayGameState>
    {
        public override string GetName() => "PlayGame";

        protected override PlayGameState GetInitialState()
        {
            return new PlayGameState
            {
                Board = new Domain.PegBoard(),
                Moves = new Stack<PegMoveWithBoard>(),
                From = null,
                To = null,
                StartingHoleSelected = false,
            };
        }
    }
}