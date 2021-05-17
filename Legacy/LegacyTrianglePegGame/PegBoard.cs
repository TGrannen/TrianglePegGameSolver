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

        public void PrintTutorialBoard_Row()
        {
            Logger.WriteToScreen("\n");
            for (int i = 0; i < 5; i++)
            {
                ConsoleColor color = ConsoleColor.Black;
                Logger.WriteToScreen("  " + i.ToString(), false);
                Logger.WriteToScreen(Space((5 - i) * 3), false);
                switch (i)
                {
                    case 0:
                        color = ConsoleColor.DarkGreen;
                        break;

                    case 1:
                        color = ConsoleColor.DarkBlue;
                        break;

                    case 2:
                        color = ConsoleColor.DarkMagenta;
                        break;

                    case 3:
                        color = ConsoleColor.DarkYellow;
                        break;

                    case 4:
                        color = ConsoleColor.DarkCyan;
                        break;
                }

                for (int j = 0; j < 5; j++)
                {
                    if (boardArray[i, j].isValid)
                    {
                        Logger.WriteToScreen(boardArray[i, j].ToStringShort(), false, color);
                        if (i == j)
                            Logger.WriteToScreen(Space(5), false, ConsoleColor.Black);
                        else
                            Logger.WriteToScreen(Space(5), false, color);
                    }
                }
                Logger.WriteToScreen("\n");
            }
        }

        public void PrintTutorialBoard_Col()
        {
            Logger.WriteToScreen("\n");
            for (int i = 0; i < 5; i++)
            {
                string row = "";
                LogWithColor(Space((5 - i) * 3), ConsoleColor.Black);
                for (int j = 0; j < 5; j++)
                {
                    if (boardArray[i, j].isValid)
                    {
                        ConsoleColor color = ConsoleColor.Black;
                        switch (j)
                        {
                            case 0:
                                color = ConsoleColor.DarkGreen;
                                break;

                            case 1:
                                color = ConsoleColor.DarkBlue;
                                break;

                            case 2:
                                color = ConsoleColor.DarkMagenta;
                                break;

                            case 3:
                                color = ConsoleColor.DarkYellow;
                                break;

                            case 4:
                                color = ConsoleColor.DarkCyan;
                                break;
                        }

                        if (i == j)
                        {
                            LogWithColor(boardArray[i, j].ToStringShort() + Space(1), color);
                            LogWithColor(row += i.ToString(), color);
                        }
                        else
                        {
                            LogWithColor(boardArray[i, j].ToStringShort() + Space(5), color);
                        }
                    }
                }
                Logger.WriteToScreen("\n");
            }
        }

        public void PrintBoard()
        {
            Logger l = new Logger();
            Logger.WriteToScreen("\n");
            for (int i = 0; i < 5; i++)
            {
                string row = "";
                row += Space((5 - i) * 3);
                for (int j = 0; j < 5; j++)
                {
                    if (boardArray[i, j].isValid)
                        row += boardArray[i, j].ToStringShort() + Space(5);
                }
                Logger.WriteToScreen(row);
                Logger.WriteToScreen("\n");
            }
        }

        public void PrintBoard(PegMove move)
        {
            Logger l = new Logger();
            Logger.WriteToScreen("\n");
            for (int i = 0; i < 5; i++)
            {
                Logger.WriteToScreen(Space((5 - i) * 3), false);
                for (int j = 0; j < 5; j++)
                {
                    if (boardArray[i, j].isValid)
                    {
                        if (boardArray[i, j].CompareCoordsToLocation(move.fromLocation.location.row, move.fromLocation.location.col))
                        {
                            LogWithColor("F", ConsoleColor.DarkYellow);
                        }
                        else if (boardArray[i, j].CompareCoordsToLocation(move.middleLocation.location.row, move.middleLocation.location.col))
                        {
                            LogWithColor("-", ConsoleColor.DarkRed);
                        }
                        else if (boardArray[i, j].CompareCoordsToLocation(move.toLocation.location.row, move.toLocation.location.col))
                        {
                            LogWithColor("T", ConsoleColor.DarkGreen);
                        }
                        else
                        {
                            Logger.WriteToScreen(boardArray[i, j].ToStringShort(), false);
                        }

                        Logger.WriteToScreen(Space(5), false);
                    }
                }
                Logger.WriteToScreen("\n");
            }
        }

        public string Space(int n)
        {
            string temp = "";
            for (int i = 0; i < n; i++)
            {
                temp += " ";
            }
            return temp;
        }

        public void LogWithColor(string message, ConsoleColor c)
        {
            Logger.WriteToScreen(message, false, c);
        }

        #endregion Print Functions
    }
}