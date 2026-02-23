using System.Collections.Generic;
using System.Linq;

namespace TrianglePegGameSolver.Web.Domain;

public class PegBoard
{
    public PegBoard()
    {
        Holes = Enumerable.Range(1, 15).Select(x => new PegHole { Filled = true, Number = x }).ToList();
    }

    public List<PegHole> Holes { get; }

    public int PegsLeft => Holes.Count(x => x.Filled);

    public PegBoard Clone()
    {
        var temp = new PegBoard();
        foreach (var hole in Holes.Select((x, i) => new { x.Filled, Index = i }))
        {
            temp.Holes[hole.Index].Filled = hole.Filled;
        }

        return temp;
    }
}