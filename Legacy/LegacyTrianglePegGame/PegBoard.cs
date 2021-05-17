using System;
using System.Collections.Generic;

namespace LegacyTrianglePegGame
{
    public class PegBoard
    {
        public PegLocation[,] boardArray;
        public int pegsLeft = 14;

        public void InitBoard()
        {
            boardArray = new PegLocation[5, 5];
            PegLocation temp = null;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    temp = new PegLocation();
                    temp.location = new PegHole(i, j);
                    switch (j)
                    {
                        case 0:
                            temp.isValid = true;
                            break;

                        case 1:
                            if (i > 0)
                                temp.isValid = true;
                            break;

                        case 2:
                            if (i > 1)
                                temp.isValid = true;
                            break;

                        case 3:
                            if (i > 2)
                                temp.isValid = true;
                            break;

                        case 4:
                            if (i > 3)
                                temp.isValid = true;
                            break;
                    }
                    if (temp.isValid)
                    {
                        temp.filled = true;
                    }
                    boardArray[i, j] = temp;
                }
            }

            InitJumpLocations();
        }

        public void EmptyPeg(int row, int col)
        {
            if (!boardArray[row, col].isValid)
            {
                throw new Exception("That is an invalid cell!");
            }
            boardArray[row, col].filled = false;
        }

        #region Jumps

        private void InitJumpLocations()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (boardArray[i, j].isValid)
                        InitJumpLocationsForLoc(boardArray[i, j]);
                }
            }
        }

        private void InitJumpLocationsForLoc(PegLocation loc)
        {
            int row = loc.location.row;
            TestRow(row, loc);
            TestRow(row - 2, loc);
            TestRow(row + 2, loc);
        }

        private void TestRow(int row, PegLocation origLoc)
        {
            int column = origLoc.location.col - 2;
            TestAndAddJumpLocation(origLoc, row, column);

            column = origLoc.location.col + 2;
            TestAndAddJumpLocation(origLoc, row, column);

            column = origLoc.location.col;
            TestAndAddJumpLocation(origLoc, row, column);
        }

        private void TestAndAddJumpLocation(PegLocation origLoc, int row, int column)
        {
            if (TestBoardLocation(row, column))
            {
                AddJumpToLocation(origLoc, row, column);
            }
        }

        private bool TestBoardLocation(int row, int column)
        {
            return (row >= 0 && column >= 0 && row < 5 && column < 5 && boardArray[row, column].isValid);
        }

        private void AddJumpToLocation(PegLocation loc, int row, int column)
        {
            _AddJumpToLocation(loc, row, column);
        }

        private void _AddJumpToLocation(PegLocation loc, int toRow, int toCol)
        {
            if (loc.location.row == 2 && loc.location.col == 2 && toRow == 4 && toCol == 0)
            {
                return;
            }
            if (loc.location.row == 4 && loc.location.col == 0 && toRow == 2 && toCol == 2)
            {
                return;
            }

            if (loc.location.row == toRow && loc.location.col == toCol)
            {
                return;
            }

            PegMove move = new PegMove();
            move.fromLocation = loc;
            move.toLocation = boardArray[toRow, toCol];

            PegHole temp = move.CalcMiddle();
            move.middleLocation = boardArray[temp.row, temp.col];
            loc.places_can_jump_to.Add(move);
        }

        #endregion Jumps

        #region Moves

        public void GetPossibleMoves(PegLocation loc, List<PegMove> moves)
        {
            foreach (PegMove move in loc.places_can_jump_to)
            {
                if (move.middleLocation.isValid && move.middleLocation.filled && !move.toLocation.filled)
                {
                    moves.Add(move);
                }
            }
        }

        public void MakeAMove(PegMove move)
        {
            move.fromLocation.filled = false;
            move.middleLocation.filled = false;
            move.toLocation.filled = true;
            pegsLeft--;
        }

        public void UndoAMove(PegMove move)
        {
            move.fromLocation.filled = true;
            move.middleLocation.filled = true;
            move.toLocation.filled = false;
            pegsLeft++;
        }

        #endregion Moves

        #region Print Functions

        public string Space(int n)
        {
            string temp = "";
            for (int i = 0; i < n; i++)
            {
                temp += " ";
            }
            return temp;
        }

        #endregion Print Functions
    }
}