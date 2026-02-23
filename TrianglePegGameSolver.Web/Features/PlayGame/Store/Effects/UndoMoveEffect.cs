using System;
using System.Threading.Tasks;
using Fluxor;
using MediatR;
using Microsoft.Extensions.Logging;
using TrianglePegGameSolver.Web.Application.Play.Queries.GetAvailableMoves;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store.Effects;

public class UndoMoveEffect : Effect<UndoMoveAction>
{
    private readonly ILogger<UndoMoveEffect> _logger;
    private readonly IMediator _mediator;
    private readonly IState<PlayGameState> _state;

    public UndoMoveEffect(ILogger<UndoMoveEffect> logger, IMediator mediator, IState<PlayGameState> state)
    {
        _logger = logger;
        _mediator = mediator;
        _state = state;
    }

    public override async Task HandleAsync(UndoMoveAction action, IDispatcher dispatcher)
    {
        try
        {
            var result = await _mediator.Send(new GetAvailableMovesQuery
            {
                Board = _state.Value.Board
            });

            dispatcher.Dispatch(new SetAvailableMovesAction { MoveCount = result });
        }
        catch (Exception e)
        {
            _logger.LogError($"Error getting Available moves during undo, reason: {e.Message}");
        }
    }
}