using Fluxor;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrianglePegGameSolver.Application.Play.Queries.GetAvailableMoves;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store.Effects
{
    public class MoveMadeEffect : Effect<MoveMadeAction>
    {
        private readonly ILogger<MoveMadeEffect> _logger;
        private readonly IMediator _mediator;

        public MoveMadeEffect(ILogger<MoveMadeEffect> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task HandleAsync(MoveMadeAction action, IDispatcher dispatcher)
        {
            try
            {
                var result = await _mediator.Send(new GetAvailableMovesQuery
                {
                    Board = action.NewBoard
                });

                dispatcher.Dispatch(new SetAvailableMovesAction { MoveCount = result });
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting Available moves, reason: {e.Message}");
            }
        }
    }
}