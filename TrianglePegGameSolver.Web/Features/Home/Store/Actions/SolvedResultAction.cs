using System.Collections.Generic;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;

namespace TrianglePegGameSolver.Web.Features.Home.Store.Actions
{
    public class SolvedResultAction
    {
        public List<PegMoveWithBoard> Moves { get; set; }
    }
}