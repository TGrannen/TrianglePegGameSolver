using Fluxor;
using System.Collections.Generic;
using System.Linq;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store.Reducers
{
    public static class PlayGameStateReducers
    {
        [ReducerMethod]
        public static PlayGameState MoveMadeAction(PlayGameState state, MoveMadeAction action)
        {
            state.Moves.Push(action.MoveWithBoard);

            return state with
            {
                Moves = state.Moves,
                Board = action.NewBoard,
                From = null,
                To = null
            };
        }

        [ReducerMethod(typeof(RestartAction))]
        public static PlayGameState OnRestart(PlayGameState _)
        {
            return new PlayGameState
            {
                Board = new Domain.PegBoard(),
                Moves = new Stack<PegMoveWithBoard>(),
                From = null,
                To = null,
                StartingHoleSelected = false
            };
        }

        [ReducerMethod]
        public static PlayGameState SelectPegAction(PlayGameState state, SelectPegAction action)
        {
            if (state.From == null)
            {
                return state with
                {
                    From = action.PegHole
                };
            }

            if (state.From.Number == action.PegHole.Number)
            {
                return state with
                {
                    From = null,
                    To = null
                };
            }

            return state with
            {
                To = action.PegHole
            };
        }

        [ReducerMethod]
        public static PlayGameState SelectStartingHoleAction(PlayGameState state, SelectStartingHoleAction action)
        {
            var pegHole = state.Board.Holes.FirstOrDefault(x => x.Number == action.PegHole.Number);
            if (pegHole != null)
            {
                pegHole.Filled = false;
            }

            return state with
            {
                Board = state.Board,
                StartingHoleSelected = true
            };
        }

        [ReducerMethod(typeof(UndoMoveAction))]
        public static PlayGameState UndoMoveAction(PlayGameState state)
        {
            if (state.Moves.Count > 1)
            {
                state.Moves.Pop();

                if (state.Moves.TryPeek(out PegMoveWithBoard result))
                {
                    return state with
                    {
                        From = null,
                        To = null,
                        Board = result.Board
                    };
                }
            }

            return state with
            {
                From = null,
                To = null
            };
        }
    }
}