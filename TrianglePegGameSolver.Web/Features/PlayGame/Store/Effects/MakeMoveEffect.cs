using Fluxor;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrianglePegGameSolver.Web.Application.Play.Command.MakeMove;
using TrianglePegGameSolver.Web.Application.Solver.Queries.SolvePegBoard;
using TrianglePegGameSolver.Web.Domain;

namespace TrianglePegGameSolver.Web.Features.PlayGame.Store.Effects;

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

            var result = await _mediator.Send(new MakeMoveCommand
            {
                From = action.From,
                To = action.To,
                PegBoard = action.Board
            });

            if (result.IsValidMove)
            {
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
                    NewBoard = result.NewBoard
                });
            }
            else
            {
                _logger.LogWarning("Not a valid move! {@Move}", action);
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Error performing move on board, reason: {e.Message}");
        }
    }
}