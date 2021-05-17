using System.Collections.Generic;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard
{
    public class SolvePegBoardQueryResponse
    {
        public bool SuccessfullySolved { get; set; }
        public List<PegMoveWithBoard> Moves { get; set; }

        public class PegMoveWithBoard
        {
            public PegMove Move { get; set; }
            public PegBoard Board { get; set; }
        }
    }
}