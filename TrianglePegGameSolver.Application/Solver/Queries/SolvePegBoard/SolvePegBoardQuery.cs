using LegacyTrianglePegGame;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Helpers;
using TrianglePegGameSolver.Domain;
using PegBoard = TrianglePegGameSolver.Domain.PegBoard;

namespace TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard
{
    public class SolvePegBoardQuery : IRequest<SolvePegBoardQueryResponse>
    {
        public PegBoard PegBoard { get; set; }
    }

    public class SolvePegBoardQueryHandler : IRequestHandler<SolvePegBoardQuery, SolvePegBoardQueryResponse>
    {
        private static readonly RowColConversion Conversion = new RowColConversion();

        public async Task<SolvePegBoardQueryResponse> Handle(SolvePegBoardQuery request, CancellationToken cancellationToken)
        {
            var moves = await Task.Run(() =>
            {
                List<HistoricalMove> moves = new List<HistoricalMove>();

                LegacyPegGame game = new LegacyPegGame();

                game.InitGame();

                foreach (var hole in request.PegBoard.Holes.Where(x => !x.Filled))
                {
                    var (row, col) = Conversion.ConvertToGridLocation(hole.Number);
                    game.board.EmptyPeg(row, col);
                }

                game.EvalBoard(moves);
                return moves;
            }, cancellationToken);

            return new SolvePegBoardQueryResponse
            {
                SuccessfullySolved = true,
                Moves = GetPegMoveWithBoards(moves, request.PegBoard.Clone())
            };
        }

        private static PegMove ConvertFromLegacy(LegacyPegMove move)
        {
            return new PegMove
            {
                From = new PegHole
                {
                    Number = Conversion.ConvertToHoleNumber(move.fromLocation),
                    Filled = move.fromLocation.filled,
                },
                Middle = new PegHole
                {
                    Number = Conversion.ConvertToHoleNumber(move.middleLocation),
                    Filled = move.middleLocation.filled,
                },
                To = new PegHole
                {
                    Number = Conversion.ConvertToHoleNumber(move.toLocation),
                    Filled = move.toLocation.filled,
                },
            };
        }

        private static List<PegMoveWithBoard> GetPegMoveWithBoards(List<HistoricalMove> moves, PegBoard tempBoard)
        {
            var resultList = new List<PegMoveWithBoard>();
            foreach (HistoricalMove historicalMove in moves.OrderBy(x => x.order))
            {
                resultList.Add(new PegMoveWithBoard
                {
                    Move = ConvertFromLegacy(historicalMove.move),
                    Board = tempBoard
                });

                tempBoard = tempBoard.Clone();
                MakeMove(tempBoard, historicalMove);
            }

            return resultList;
        }

        private static void MakeMove(PegBoard board, HistoricalMove historicalMove)
        {
            var fromNumber = Conversion.ConvertToHoleNumber(historicalMove.move.fromLocation);
            var toNumber = Conversion.ConvertToHoleNumber(historicalMove.move.toLocation);
            var middleNumber = Conversion.ConvertToHoleNumber(historicalMove.move.middleLocation);
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