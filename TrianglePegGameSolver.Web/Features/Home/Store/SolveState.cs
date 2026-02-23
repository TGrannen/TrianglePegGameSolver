using Fluxor;
using System.Collections.Generic;
using TrianglePegGameSolver.Web.Application.Solver.Queries.SolvePegBoard;

namespace TrianglePegGameSolver.Web.Features.Home.Store;

public record SolveState
{
    public int CurrentMoveIndex { get; init; }
    public Domain.PegBoard Board { get; init; }
    public List<PegMoveWithBoard> Moves { get; init; }
    public PegMoveWithBoard CurrentMove { get; init; }
    public bool SolutionAttempted { get; init; }
    public bool IsLoading { get; init; }
    public bool FoundSolution { get; init; }
}

public class SolveFeature : Feature<SolveState>
{
    public override string GetName() => "Solve";

    protected override SolveState GetInitialState()
    {
        return new SolveState
        {
            Board = new Domain.PegBoard(),
            CurrentMove = null,
            SolutionAttempted = false,
            Moves = null,
            CurrentMoveIndex = 0,
            IsLoading = false,
            FoundSolution = false
        };
    }
}