using System.Collections.Generic;
using System.Linq;

namespace TrianglePegGameSolver.Domain
{
    public class PegBoard
    {
        public PegHole[,] BoardArray;

        public IEnumerable<PegHole> PegHoles
        {
            get
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        var pegHole = BoardArray[i, j];
                        if (pegHole != null)
                        {
                            yield return pegHole;
                        }
                    }
                }
            }
        }

        public int PegsLeft
        {
            get
            {
                var temp = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (BoardArray[i, j].Filled)
                        {
                            temp++;
                        }
                    }
                }
                return temp;
            }
        }

        public void InitBoard()
        {
            BoardArray = new PegHole[5, 5];
            int number = 1;
            for (int i = 0; i < 5; i++)
            {
                foreach (int col in Enumerable.Range(0, 5).Where(col => IsCellValid(col, i)))
                {
                    var temp = new PegHole { Row = i, Column = col, Filled = true, Number = number };
                    BoardArray[i, col] = temp;
                    number++;
                }
            }
        }

        private static bool IsCellValid(int j, int i)
        {
            switch (j)
            {
                case 0:
                case 1 when i > 0:
                case 2 when i > 1:
                case 3 when i > 2:
                case 4 when i > 3:
                    return true;

                default:
                    return false;
            }
        }

        public PegBoard Clone()
        {
            var temp = new PegBoard();
            temp.InitBoard();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    temp.BoardArray[i, j].Filled = BoardArray[i, j].Filled;
                }
            }
            return temp;
        }
    }
}