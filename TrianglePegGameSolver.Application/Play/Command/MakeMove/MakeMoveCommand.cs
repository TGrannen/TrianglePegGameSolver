using LegacyTrianglePegGame;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Helpers;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Application.Play.Command.MakeMove
{
    public class MakeMoveCommand : IRequest<MakeMoveCommandResponse>
    {
        public PegBoard PegBoard { get; set; }
        public PegHole From { get; set; }
        public PegHole To { get; set; }
    }

    public class MakeMoveCommandHandler : IRequestHandler<MakeMoveCommand, MakeMoveCommandResponse>
    {
        private static readonly RowColConversion Conversion = new RowColConversion();

        public Task<MakeMoveCommandResponse> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
        {
            LegacyPegGame game = new LegacyPegGame();

            game.InitGame();

            foreach (var hole in request.PegBoard.Holes.Where(x => !x.Filled))
            {
                var (row, col) = Conversion.ConvertToGridLocation(hole.Number);
                game.board.EmptyPeg(row, col);
            }

            var availableMoves = game.GetMovesOnBoard();

            var move = availableMoves.FirstOrDefault(x =>
            {
                var from = Conversion.ConvertToHoleNumber(x.fromLocation);
                var to = Conversion.ConvertToHoleNumber(x.toLocation);
                return from == request.From.Number && to == request.To.Number;
            });

            if (move == null)
            {
                return Task.FromResult(new MakeMoveCommandResponse
                {
                    IsValidMove = false,
                    NewBoard = request.PegBoard
                });
            }

            var newBoard = request.PegBoard.Clone();
            MakeMove(newBoard, move);

            return Task.FromResult(new MakeMoveCommandResponse
            {
                IsValidMove = true,
                NewBoard = newBoard
            });
        }

        private static void MakeMove(PegBoard board, LegacyPegMove historicalMove)
        {
            var fromNumber = Conversion.ConvertToHoleNumber(historicalMove.fromLocation);
            var toNumber = Conversion.ConvertToHoleNumber(historicalMove.toLocation);
            var middleNumber = Conversion.ConvertToHoleNumber(historicalMove.middleLocation);
            SetHoleFilled(board, fromNumber, false);
            SetHoleFilled(board, toNumber, true);
            SetHoleFilled(board, middleNumber, false);
        }

        private static void SetHoleFilled(PegBoard board, int fromNumber, bool filled)
        {
            var hole = board.Holes.FirstOrDefault(x => x.Number == fromNumber);
            if (hole != null) hole.Filled = filled;
        }
    }
}