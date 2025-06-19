using System.Collections.Generic;
using BasicCheckeredBE.Networking;
using BasicCheckeredBE.Networking.DTOs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BasicCheckeredBE.Controllers
{
    public class GameBoardController /*: IController*/
    {
        /*
        private GetNewBoardService _getNewBoardService;
        private AttemptToMoveService _attemptToMoveService;
        
        public void Initialize()
        {
            _getNewBoardService = new GetNewBoardService();
            _attemptToMoveService = new AttemptToMoveService();
        }
        */
        
        public async UniTask<BoardDTO> GetNewBoard()
        {
            // Simulate fetching a new board
            await UniTask.Delay(500); // Simulate network delay
            
            SquareDTO[,] board = new SquareDTO[8, 8];
            
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new SquareDTO(new PieceDTO(GlobalFields.PieceType.Pawn, GlobalFields.PlayerType.Player1), x, y);
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player1 Pawn");
                }
            }

            for (int y = 2; y < 6; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new SquareDTO(new PieceDTO(GlobalFields.PieceType.None, GlobalFields.PlayerType.None), x, y);
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to None");
                }
            }
            
            for (int y = 6; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new SquareDTO(new PieceDTO(GlobalFields.PieceType.Pawn, GlobalFields.PlayerType.Player2), x, y);
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player2 Pawn");
                }
            }

            return new BoardDTO(board);
        }

        public async UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare)
        {
            await UniTask.Delay(250); // Simulate processing time
            
            Debug.Log($"GATEWAY: Attempting move: {piece} from [{originalSquare.X}, {originalSquare.Y}, ({originalSquare.Piece.PieceType})] to [{targetSquare.X}, {targetSquare.Y}, ({targetSquare.Piece.PieceType})]");
            
            //Evaluation Block: Is the target square occupied? 
            if (targetSquare.Piece.PieceType != GlobalFields.PieceType.None)
            {
                var invalidMove = new List<SquareDTO>();    
                
                invalidMove.Add(new SquareDTO(piece, originalSquare.X, originalSquare.Y));

                return new AttemptToMoveDTO(false,"Invalid move! The target square is occupied!", invalidMove);
            }
            
            //Evaluation Block: Is the piece moving to a non-adjacent square?
            if (Mathf.Abs(originalSquare.X - targetSquare.X) > 1 || Mathf.Abs(originalSquare.Y - targetSquare.Y) > 1)
            {
                var invalidMove = new List<SquareDTO>();
                
                invalidMove.Add(new SquareDTO(piece, originalSquare.X, originalSquare.Y));
                
                return new AttemptToMoveDTO(false,"Invalid move! The piece can only move to adjacent squares!", invalidMove);
            }
            
            //Sending move block
            
            var boardSquaresToUpdate = new List<SquareDTO>();
            
            boardSquaresToUpdate.Add(new SquareDTO(new PieceDTO(GlobalFields.PieceType.None, GlobalFields.PlayerType.None), originalSquare.X, originalSquare.Y));
            boardSquaresToUpdate.Add(new SquareDTO(piece, targetSquare.X, targetSquare.Y));
            
            Debug.Log($"GATEWAY: Sending move: {piece} from [{boardSquaresToUpdate[0].X}, {boardSquaresToUpdate[0].Y}] to [{boardSquaresToUpdate[1].X}, {boardSquaresToUpdate[1].Y}]");
            var attemptToMove = new AttemptToMoveDTO(true,"Move Successful!", boardSquaresToUpdate);
            return attemptToMove;
        }
    }
    
    /*
    public class AttemptToMoveService
    {

    }

    public class GetNewBoardService
    {

    }

    public interface IController
    {
        void Initialize();
    }
    */
}