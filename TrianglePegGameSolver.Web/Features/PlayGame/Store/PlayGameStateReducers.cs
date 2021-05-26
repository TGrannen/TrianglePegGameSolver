using Fluxor;
using System.Collections.Generic;
using System.Linq;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store
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
        public static PlayGameState OnRestart(PlayGameState state)
        {
            return state with
            {
                Board = new Domain.PegBoard(),
                Moves = new Stack<PegMoveWithBoard>(),
                From = null,
                To = null,
                StartingHoleSelected = false
            };
        }

        [ReducerMethod]
        public static PlayGameState OnSetShowPegNumbersAction(PlayGameState state, SetShowPegNumbersAction action)
        {
            return state with
            {
                ShowPegNumbers = action.ShowNumbers
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

        [ReducerMethod]
        public static PlayGameState SetAvailableMovesAction(PlayGameState state, SetAvailableMovesAction action)
        {
            return state with
            {
                AvailableMoves = action.MoveCount
            };
        }

        [ReducerMethod(typeof(UndoMoveAction))]
        public static PlayGameState UndoMoveAction(PlayGameState state)
        {
            if (state.Moves.Count > 0)
            {
                if (state.Moves.TryPop(out PegMoveWithBoard result))
                {
                    return state with
                    {
                        From = null,
                        To = null,
                        Board = result.Board,
                        Moves = state.Moves
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