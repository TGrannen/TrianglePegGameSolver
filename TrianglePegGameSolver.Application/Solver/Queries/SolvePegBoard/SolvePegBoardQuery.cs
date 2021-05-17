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

        public SolvePegBoardQueryHandler(ILogger<SolvePegBoardQueryHandler> logger)
        {
            _logger = logger;
        }

        public Task<SolvePegBoardQueryResponse> Handle(SolvePegBoardQuery request, CancellationToken cancellationToken)
        {
            LegacyPegGame game = new LegacyPegGame();
            List<HistoricalMove> moves = new List<HistoricalMove>();
            try
            {
                game.InitGame();
                game.board.EmptyPeg(0, 0);

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
                    Column = move.fromLocation.location.col,
                    Row = move.fromLocation.location.row,
                    Filled = move.fromLocation.filled,
                },
                Middle = new PegHole
                {
                    Column = move.middleLocation.location.col,
                    Row = move.middleLocation.location.row,
                    Filled = move.middleLocation.filled,
                },
                To = new PegHole
                {
                    Column = move.toLocation.location.col,
                    Row = move.toLocation.location.row,
                    Filled = move.toLocation.filled,
                },
            };
        }
    }
}