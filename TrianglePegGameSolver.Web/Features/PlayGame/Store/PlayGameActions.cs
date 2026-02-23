using TrianglePegGameSolver.Web.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Web.Domain;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store;

public class RestartAction
{
}

public class SelectPegAction
{
    public PegHole PegHole { get; set; }
}

public class SelectStartingHoleAction
{
    public PegHole PegHole { get; set; }
}

public class MakeMoveAction
{
    public Domain.PegBoard Board { get; set; }
    public PegHole From { get; set; }
    public PegHole To { get; set; }
}

public class MoveMadeAction
{
    public PegMoveWithBoard MoveWithBoard { get; set; }
    public Domain.PegBoard NewBoard { get; set; }
}

public class UndoMoveAction
{
}

public class SetAvailableMovesAction
{
    public int MoveCount { get; set; }
}

public class SetShowPegNumbersAction
{
    public bool ShowNumbers { get; set; }
}