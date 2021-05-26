using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Application.Play.Command.MakeMove
{
    public class MakeMoveCommandResponse
    {
        public bool IsValidMove { get; set; }
        public PegBoard NewBoard { get; set; }
    }
}