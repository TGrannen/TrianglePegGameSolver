using TrianglePegGameSolver.Web.Domain;

namespace TrianglePegGameSolver.Web.Application.Play.Command.MakeMove;

public class MakeMoveCommandResponse
{
    public bool IsValidMove { get; set; }
    public PegBoard NewBoard { get; set; }
}