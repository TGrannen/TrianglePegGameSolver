using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyTrianglePegGame
{
  class PegGame
  {
    public PegBoard board;

    public void InitGame()
    {
      board = new PegBoard();
      board.InitBoard();
    }
    public void InitGame(int row, int col)
    {
      InitGame();
      board.EmptyPeg(row, col);
    }

    public List<PegMove> GetMovesOnBoard()
    {
      List<PegMove> moves = new List<PegMove>();
      for (int i = 0; i < 5; i++)
      {
        for (int j = 0; j < 5; j++)
        {
          PegLocation loc = board.boardArray[i, j];
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
      foreach (PegMove move in GetMovesOnBoard())
      {
        if (EvalBoard_rec(move, pastMoves))
        {
          break;
        }
      }
    }
    private bool EvalBoard_rec(PegMove move, List<HistoricalMove> pastMoves)
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

      foreach (PegMove newMove in GetMovesOnBoard())
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


    public void DisplayAnswer(List<HistoricalMove> pastMoves)
    {
      if (pastMoves.Count == 0)
      {
        throw new Exception("Sorry, I couldn't figure that one out!");
      }

      pastMoves.OrderBy(h => h.order);
      for (int i = pastMoves.Count - 1; i >= 0; i--)
      {
        board.UndoAMove(pastMoves[i].move);
      }
      Console.ReadLine();
      foreach (HistoricalMove histMove in pastMoves)
      {
        board.MakeAMove(histMove.move);
        histMove.move.PrintMove();
        board.PrintBoard(histMove.move);
        Console.ReadLine();
      }
    }
  }


  public class HistoricalMove
  {
    public HistoricalMove(int count, PegMove m)
    {
      order = count;
      move = m;
    }
    public int order;
    public PegMove move;
  }
}
