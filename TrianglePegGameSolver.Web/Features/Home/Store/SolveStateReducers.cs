using Fluxor;
using System.Linq;
using TrianglePegGameSolver.Web.Features.Home.Store.Actions;

namespace TrianglePegGameSolver.Web.Features.Home.Store;

public static class SolveStateReducers
{
    [ReducerMethod]
    public static SolveState OnMoveSelected(SolveState state, MoveSelectedAction action)
    {
        return state with
        {
            CurrentMove = action.Move,
            CurrentMoveIndex = state.Moves.IndexOf(action.Move),
            Board = action.Move.Board
        };
    }

    [ReducerMethod(typeof(NextMoveAction))]
    public static SolveState OnNextMove(SolveState state)
    {
        var nextMoveIndex = state.CurrentMoveIndex + 1;

        bool isIndexValid = state.Moves.Count > nextMoveIndex;
        if (!isIndexValid)
        {
            return state;
        }

        var move = state.Moves[nextMoveIndex];
        return state with
        {
            CurrentMove = move,
            CurrentMoveIndex = nextMoveIndex,
            Board = move.Board
        };
    }

    [ReducerMethod(typeof(PreviousMoveAction))]
    public static SolveState OnPreviousMove(SolveState state)
    {
        var prevMoveIndex = state.CurrentMoveIndex - 1;

        bool isIndexValid = prevMoveIndex >= 0;
        if (!isIndexValid)
        {
            return state;
        }

        var move = state.Moves[prevMoveIndex];
        return state with
        {
            CurrentMove = move,
            CurrentMoveIndex = prevMoveIndex,
            Board = move.Board
        };
    }

    [ReducerMethod(typeof(ResetAction))]
    public static SolveState OnReset(SolveState _)
    {
        return GetResetSolveState();
    }

    [ReducerMethod(typeof(SolveAction))]
    public static SolveState OnSolveAction(SolveState _)
    {
        var state = GetResetSolveState();
        return state with
        {
            IsLoading = true
        };
    }

    [ReducerMethod(typeof(FailedToSolveResultAction))]
    public static SolveState OnFailedToSolveResult(SolveState _)
    {
        var state = GetResetSolveState();
        return state with
        {
            SolutionAttempted = true,
            FoundSolution = false
        };
    }

    [ReducerMethod]
    public static SolveState OnSolvedResultAction(SolveState state, SolvedResultAction action)
    {
        var moves = action.Moves;
        if (moves != null && moves.Any())
        {
            var move = moves.First();
            return state with
            {
                CurrentMove = move,
                CurrentMoveIndex = 0,
                Board = move.Board,
                IsLoading = false,
                Moves = moves,
                SolutionAttempted = true,
                FoundSolution = true
            };
        }

        return GetResetSolveState();
    }

    private static SolveState GetResetSolveState()
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