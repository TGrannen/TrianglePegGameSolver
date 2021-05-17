namespace TrianglePegGameSolver.Domain
{
    public class PegHole
    {
        public int Row { get; set; } = -1;
        public int Column { get; set; } = -1;
        public int Number { get; set; } = -1;
        public bool Filled { get; set; }

        public override string ToString()
        {
            return (Row + "," + Column);
        }
    }

}