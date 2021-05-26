using Fluxor;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Web.Features.Home.Store.Actions;

namespace TrianglePegGameSolver.Web.Features.Home.Store.Effects
{
    public class SolveActionEffect : Effect<SolveAction>
    {
        private readonly ILogger<SolveActionEffect> _logger;
        private readonly IMediator _mediator;

        public SolveActionEffect(ILogger<SolveActionEffect> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task HandleAsync(SolveAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Solving Board...");

                var result = await _mediator.Send(new SolvePegBoardQuery { PegBoard = action.Board });

                if (result.SuccessfullySolved)
                {
                    _logger.LogInformation("Board Solved successfully!");
                    dispatcher.Dispatch(new SolvedResultAction { Moves = result.Moves });
                }
                else
                {
                    _logger.LogInformation("Board Failed to solve! {Request}", action);
                    dispatcher.Dispatch(new FailedToSolveResultAction());
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error solving board, reason: {e.Message}");
            }
        }
    }
}