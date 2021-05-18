using LegacyTrianglePegGame;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly ILogger<SolvePegBoardQueryHandler> _logger;
        private static readonly RowColConversion Conversion = new RowColConversion();

        public SolvePegBoardQueryHandler(ILogger<SolvePegBoardQueryHandler> logger)
        {
            _logger = logger;
        }

        public Task<SolvePegBoardQueryResponse> Handle(SolvePegBoardQuery request, CancellationToken cancellationToken)
        {
            List<HistoricalMove> moves = new List<HistoricalMove>();

            try
            {
                LegacyPegGame game = new LegacyPegGame();
                game.InitGame();
                foreach (var hole in request.PegBoard.Holes.Where(x => !x.Filled))
                {
                    var (row, col) = Conversion.ConvertToGridLocation(hole.Number);
                    game.board.EmptyPeg(row, col);
                }

                game.EvalBoard(moves);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error solving board");
            }

            var result = new SolvePegBoardQueryResponse
            {
                SuccessfullySolved = true,
                Moves = moves.Select(x => new PegMoveWithBoard()
                {
                    Move = ConvertFromLegacy(x.move)
                }).ToList()
            };
            return Task.FromResult(result);
        }

        private PegMove ConvertFromLegacy(LegacyPegMove move)
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

        private class RowColConversion
        {
            private readonly Dictionary<int, (int, int)> _numberToGridDictionary = new Dictionary<int, (int, int)>();

            private readonly Dictionary<(int, int), int> _gridToNumberDictionary = new Dictionary<(int, int), int>();

            public RowColConversion()
            {
                var board = new LegacyPegBoard();
                board.InitBoard();
                int count = 1;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        LegacyPegLocation loc = board.boardArray[i, j];
                        if (loc.isValid)
                        {
                            _numberToGridDictionary.Add(count, (i, j));
                            _gridToNumberDictionary.Add((i, j), count);

                            count++;
                        }
                    }
                }
            }

            public int ConvertToHoleNumber(LegacyPegLocation location)
            {
                return _gridToNumberDictionary[(location.location.row, location.location.col)];
            }

            public (int, int) ConvertToGridLocation(int number)
            {
                return _numberToGridDictionary[number];
            }
        }
    }
}