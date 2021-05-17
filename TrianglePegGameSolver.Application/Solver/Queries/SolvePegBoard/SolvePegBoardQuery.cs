using LegacyTrianglePegGame;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
            PegGame game = new PegGame();
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

            return Task.FromResult(new SolvePegBoardQueryResponse { SuccessfullySolved = true });
        }
    }
}