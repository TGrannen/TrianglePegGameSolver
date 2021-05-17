using System.Collections.Generic;
using System.Linq;

namespace LegacyTrianglePegGame
{
    public class LegacyPegGame
    {
        public LegacyPegBoard board;

        public void InitGame()
        {
            board = new LegacyPegBoard();
            board.InitBoard();
        }

        public void InitGame(int row, int col)
        {
            InitGame();
            board.EmptyPeg(row, col);
        }

        public List<LegacyPegMove> GetMovesOnBoard()
        {
            List<LegacyPegMove> moves = new List<LegacyPegMove>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    LegacyPegLocation loc = board.boardArray[i, j];
                    if (loc.isValid && loc.filled)
                    {
                        board.GetPossibleMoves(loc, moves);
                    }
                }
            }
            return moves;
        }

        public void EvalBoard(List<HistoricalMove> pastMoves)
        {
            foreach (LegacyPegMove move in GetMovesOnBoard())
            {
                if (EvalBoard_rec(move, pastMoves))
                {
                    break;
                }
            }
        }

        private bool EvalBoard_rec(LegacyPegMove move, List<HistoricalMove> pastMoves)
        {
            board.MakeAMove(move);
            HistoricalMove hist = null;

            if (pastMoves.Count == 0)
                hist = new HistoricalMove(0, move);
            else
                hist = new HistoricalMove(pastMoves.Last().order + 1, move);

            pastMoves.Add(hist);

            if (board.pegsLeft == 1)
            {
                return true;
            }

            foreach (LegacyPegMove newMove in GetMovesOnBoard())
            {
                if (EvalBoard_rec(newMove, pastMoves))
                {
                    return true;
                }
            }
            pastMoves.Remove(hist);
            board.UndoAMove(move);
            return false;
        }
    }

    public class HistoricalMove
    {
        public HistoricalMove(int count, LegacyPegMove m)
        {
            order = count;
            move = m;
        }

        public int order;
        public LegacyPegMove move;
    }
}