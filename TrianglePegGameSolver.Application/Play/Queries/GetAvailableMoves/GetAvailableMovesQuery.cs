using LegacyTrianglePegGame;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Helpers;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Application.Play.Queries.GetAvailableMoves
{
    public class GetAvailableMovesQuery : IRequest<int>
    {
        public PegBoard Board { get; set; }
    }

    public class GetAvailableMovesQueryHandler : IRequestHandler<GetAvailableMovesQuery, int>
    {
        private static readonly RowColConversion Conversion = new RowColConversion();

        public Task<int> Handle(GetAvailableMovesQuery request, CancellationToken cancellationToken)
        {
            LegacyPegGame game = new LegacyPegGame();

            game.InitGame();

            foreach (var hole in request.Board.Holes.Where(x => !x.Filled))
            {
                var (row, col) = Conversion.ConvertToGridLocation(hole.Number);
                game.board.EmptyPeg(row, col);
            }

            var availableMoves = game.GetMovesOnBoard();
            return Task.FromResult(availableMoves.Count);
        }
    }
}