using System.Collections.Generic;
using Core.Networking.DTOs;
using Core.Service_Locator;
using Core.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Networking
{
    public class Gateway : IGateway
    {
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public async UniTask<BoardDTO> GetNewBoard()
        {
            Debug.Log("Gateway Reached!");

            SquareDTO[,] board = new SquareDTO[8, 8];
            
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new SquareDTO(new PieceDTO(PieceType.Pawn, PlayerType.Player1), x, y);
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player1 Pawn");
                }
            }

            for (int y = 2; y < 6; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new SquareDTO(new PieceDTO(PieceType.None, PlayerType.None), x, y);
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to None");
                }
            }
            
            for (int y = 6; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new SquareDTO(new PieceDTO(PieceType.Pawn, PlayerType.Player2), x, y);
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player2 Pawn");
                }
            }

            return new BoardDTO(board);
        }

        public async UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare)
        {
            var boardSquaresToUpdate = new List<SquareDTO>();
            
            boardSquaresToUpdate.Add(new SquareDTO(new PieceDTO(PieceType.None, PlayerType.None), originalSquare.X, originalSquare.Y));
            boardSquaresToUpdate.Add(new SquareDTO(piece, targetSquare.X, targetSquare.Y));
            
            Debug.Log($"GATEWAY: Sending move: {piece} from [{boardSquaresToUpdate[0].X}, {boardSquaresToUpdate[0].Y}] to [{boardSquaresToUpdate[1].X}, {boardSquaresToUpdate[1].Y}]");
            var attemptToMove = new AttemptToMoveDTO(true, boardSquaresToUpdate);
            return attemptToMove;
        }
    }
}