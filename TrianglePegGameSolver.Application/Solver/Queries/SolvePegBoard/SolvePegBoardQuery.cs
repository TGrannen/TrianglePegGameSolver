using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard
{
    public class SolvePegBoardQuery : IRequest<SolvePegBoardQueryResponse>
    {
        public PegBoard PegBoard { get; set; }
    }

    public class SolvePegBoardQueryHandler : IRequestHandler<SolvePegBoardQuery, SolvePegBoardQueryResponse>
    {
        public PegHole CalcMiddle(PegMove pegMove)
        {
            PegHole temp = new PegHole
            {
                Row = pegMove.From.Row + ((pegMove.To.Row - pegMove.From.Row) / 2),
                Column = pegMove.From.Column + ((pegMove.To.Column - pegMove.From.Column) / 2)
            };
            return temp;
        }

        public Task<SolvePegBoardQueryResponse> Handle(SolvePegBoardQuery request, CancellationToken cancellationToken)
        {
            var pastMoves = new List<HistoricalMove>();
            foreach (PegMove move in GetMovesOnBoard(request.PegBoard))
            {
                if (EvalBoard_rec(move, pastMoves, request.PegBoard))
                {
                    break;
                }
            }

            return Task.FromResult(new SolvePegBoardQueryResponse { SuccessfullySolved = true });
        }

        private bool EvalBoard_rec(PegMove move, List<HistoricalMove> pastMoves, PegBoard board)
        {
            MakeAMove(move);
            var hist = pastMoves.Count == 0
                ? new HistoricalMove(0, move, board)
                : new HistoricalMove(pastMoves.Last().Order + 1, move, board);

            pastMoves.Add(hist);

            if (board.PegsLeft == 1)
            {
                return true;
            }

            foreach (PegMove newMove in GetMovesOnBoard(board))
            {
                if (EvalBoard_rec(newMove, pastMoves, board))
                {
                    return true;
                }
            }
            pastMoves.Remove(hist);
            UndoAMove(move);
            return false;
        }

        private List<PegMove> GetMovesOnBoard(PegBoard board)
        {
            List<PegMove> moves = new List<PegMove>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var hole = board.BoardArray[i, j];
                    if (hole != null && hole.Filled)
                    {
                        foreach (PegMove move in GetMovesForHole(hole, board))
                        {
                            if (move.Middle != null && move.Middle.Filled && !move.To.Filled)
                            {
                                moves.Add(move);
                            }
                        }
                    }
                }
            }
            return moves;
        }

        private IEnumerable<PegMove> GetMovesForHole(PegHole loc, PegBoard board)
        {
            int row = loc.Row;
            var list = TestRow(row, loc, board);
            list = list.Concat(TestRow(row - 2, loc, board)).ToList();
            list = list.Concat(TestRow(row + 2, loc, board)).ToList();
            return list;
        }

        private void MakeAMove(PegMove move)
        {
            move.From.Filled = false;
            move.Middle.Filled = false;
            move.To.Filled = true;
        }

        private IEnumerable<PegMove> TestAndAddJumpLocation(PegHole loc, int toRow, int toCol, PegBoard board)
        {
            if (TestBoardLocation(toRow, toCol, board))
            {
                if (loc.Row == 2 && loc.Column == 2 && toRow == 4 && toCol == 0)
                {
                    yield break;
                }
                if (loc.Row == 4 && loc.Column == 0 && toRow == 2 && toCol == 2)
                {
                    yield break;
                }

                if (loc.Row == toRow && loc.Column == toCol)
                {
                    yield break;
                }

                PegMove move = new PegMove { From = loc, To = board.BoardArray[toRow, toCol] };

                move.Middle = CalcMiddle(move);
                yield return move;
            }
        }

        private bool TestBoardLocation(int row, int column, PegBoard board)
        {
            return (row >= 0 && column >= 0 && row < 5 && column < 5 && board.BoardArray[row, column] != null);
        }

        private IEnumerable<PegMove> TestRow(int row, PegHole origLoc, PegBoard board)
        {
            var list = TestAndAddJumpLocation(origLoc, row, origLoc.Column - 2, board).ToList();
            list = list.Concat(TestAndAddJumpLocation(origLoc, row, origLoc.Column + 2, board)).ToList();
            list = list.Concat(TestAndAddJumpLocation(origLoc, row, origLoc.Column, board)).ToList();
            return list;
        }

        private void UndoAMove(PegMove move)
        {
            move.From.Filled = true;
            move.Middle.Filled = true;
            move.To.Filled = false;
        }

        private class HistoricalMove
        {
            public HistoricalMove(int count, PegMove m, PegBoard board)
            {
                Order = count;
                Move = m;
                BoardAfterMove = board.Clone();
            }

            public PegBoard BoardAfterMove { get; set; }
            public PegMove Move { get; set; }
            public int Order { get; set; }
        }
    }
}