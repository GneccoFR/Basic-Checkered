using System.Collections.Generic;
using BasicCheckeredBE.Networking;
using BasicCheckeredBE.Networking.DTOs;
using Core.EventBus;
using Core.EventBus.GameEvents;
using Core.Networking;
using Core.Service_Locator;
using Core.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using IGateway = BasicCheckeredBE.Networking.IGateway;

namespace Core.UseCases
{
    public class AttemptPieceMovementUseCase : IUseCase
    {
        private readonly IGateway _gateway;
        private readonly IEventBus _eventBus;
        private readonly Piece _pieceToMove;
        private readonly BoardSquare _originalSquare;
        private readonly BoardSquare _targetSquare;
        
        public AttemptPieceMovementUseCase(Piece pieceToMove, BoardSquare originalSquare, BoardSquare targetSquare)
        {
            _gateway = ServiceLocator.Instance.GetService<IGateway>();
            _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
            _pieceToMove = pieceToMove;
            _originalSquare = originalSquare;
            _targetSquare = targetSquare;
        }

        public async UniTask Execute()
        {
            Debug.Log("AttemptPieceMovementUseCase Execute called!");
            Debug.Log($"Original square Piece: {_originalSquare.Piece.PieceType}, Owner: {_originalSquare.Piece.Owner}");
            Debug.Log($"Target square Piece: {_targetSquare.Piece.PieceType}, Owner: {_targetSquare.Piece.Owner}");

            var movingPlayer = _originalSquare.Piece.Owner.MapToPlayerBEType();
            var targetSquarePlayer = _targetSquare.Piece.Owner.MapToPlayerBEType();
            var originalPieceDto = new PieceDTO((GlobalFields.PieceType)_pieceToMove.PieceType, movingPlayer);
            var targetPieceDto = new PieceDTO((GlobalFields.PieceType)_targetSquare.Piece.PieceType, targetSquarePlayer);
            var originalSquareDto = new SquareDTO(originalPieceDto, (int)_originalSquare.Coordinates.x, (int)_originalSquare.Coordinates.y);
            var targetSquareDto = new SquareDTO(targetPieceDto, (int)_targetSquare.Coordinates.x, (int)_targetSquare.Coordinates.y);
            
            var attemptToMove = await _gateway.AttemptToMove(originalSquareDto, targetSquareDto);
            
            if (attemptToMove.Success)
            {
                // If the move was successful, we publish an event with the updated board squares
                List<BoardSquare> updatedBoardSquares = LinqMapper.MapToList(attemptToMove.UpdatedBoardSquares);
                _eventBus.Publish(new AttemptPieceMovementEventResult(updatedBoardSquares, attemptToMove.CurrentPlayer.MapToPlayerType(), attemptToMove.OpponentPlayer.MapToPlayerType()));
            }
            else
            {
                // Handle the case where the move was not successful, if needed
                // For example, you could publish a failure event or log an error
                _eventBus.Publish(new IllegalMovementEventResult(attemptToMove.Message));
            }
        }
    }
}
