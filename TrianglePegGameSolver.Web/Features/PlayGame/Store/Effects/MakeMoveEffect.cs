using Fluxor;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Domain;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store.Effects
{
    public class MakeMoveEffect : Effect<MakeMoveAction>
    {
        private readonly ILogger<MakeMoveEffect> _logger;
        private readonly IMediator _mediator;

        public MakeMoveEffect(ILogger<MakeMoveEffect> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task HandleAsync(MakeMoveAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Performing Move...");

                var result = await _mediator.Send(new SolvePegBoardQuery { PegBoard = action.Board });

                _logger.LogInformation("Move Performed successfully!");
                dispatcher.Dispatch(new MoveMadeAction
                {
                    MoveWithBoard = new PegMoveWithBoard
                    {
                        Board = action.Board,
                        Move = new PegMove
                        {
                            From = action.From,
                            To = action.To
                        }
                    },
                    NewBoard = new Domain.PegBoard() // TODO: get from query
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"Error performing move on board, reason: {e.Message}");
            }
        }
    }
}