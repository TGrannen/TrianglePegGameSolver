using Fluxor;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store.Effects;

public class SelectPegEffect : Effect<SelectPegAction>
{
    private readonly ILogger<SelectPegEffect> _logger;
    private readonly IState<PlayGameState> _state;

    public SelectPegEffect(ILogger<SelectPegEffect> logger, IState<PlayGameState> state)
    {
        _logger = logger;
        _state = state;
    }

    public override Task HandleAsync(SelectPegAction action, IDispatcher dispatcher)
    {
        try
        {
            if (_state.Value.From != null && _state.Value.To != null)
            {
                dispatcher.Dispatch(new MakeMoveAction
                {
                    From = _state.Value.From,
                    To = action.PegHole,
                    Board = _state.Value.Board
                });
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Error performing move on board, reason: {e.Message}");
        }

        return Task.CompletedTask;
    }
}