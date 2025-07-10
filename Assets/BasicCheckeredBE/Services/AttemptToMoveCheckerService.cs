using System;
using System.Collections.Generic;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace BasicCheckeredBE.Services
{
    public class AttemptToMoveCheckerService
    {
        public AttemptToMove AttemptToMoveChecker(BoardSquare[,] currentBoard, BoardSquare originalSquare, BoardSquare targetSquare)
        {
            //Evaluation Block: Is the target square occupied? 
            if (targetSquare.Piece.PieceType != GlobalFields.PieceType.None)
            {
                var invalidMove = new List<BoardSquare>();    
                
                invalidMove.Add(new BoardSquare(originalSquare.Piece, new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));

                return new AttemptToMove(false,"Invalid move! The target square is occupied!", invalidMove/*, originalSquare.Piece.Owner, targetSquare.Piece.Owner*/);
            }
            
            //Evaluation Block: Is the piece jumping over an enemy piece?
            if (CanCapture(currentBoard, originalSquare, targetSquare))
            {
                var captureMove = new List<BoardSquare>();
                
                int dx = (int)(targetSquare.Coordinates.X - originalSquare.Coordinates.X);
                int dy = (int)(targetSquare.Coordinates.Y - originalSquare.Coordinates.Y);

                int midX = (int)originalSquare.Coordinates.X + dx / 2;
                int midY = (int)originalSquare.Coordinates.Y + dy / 2;
                
                captureMove.Add(new BoardSquare(new Piece(GlobalFields.PieceType.None, new Player(GlobalFields.PlayerType.None, Guid.Empty)), new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));
                captureMove.Add(new BoardSquare(new Piece(GlobalFields.PieceType.None, new Player(GlobalFields.PlayerType.None, Guid.Empty)), new Vector2(midX, midY)));
                captureMove.Add(new BoardSquare(originalSquare.Piece, new Vector2(targetSquare.Coordinates.X, targetSquare.Coordinates.Y)));

                return new AttemptToMove(true, "Enemy piece captured!", captureMove/*, targetSquare.Piece.Owner, originalSquare.Piece.Owner*/);
            }
            
            
            //Evaluation Block: Is the piece moving to a non-adjacent square?
            if (Mathf.Abs(originalSquare.Coordinates.X - targetSquare.Coordinates.X) > 1 || Mathf.Abs(originalSquare.Coordinates.Y - targetSquare.Coordinates.Y) > 1)
            {
                var invalidMove = new List<BoardSquare>();
                
                invalidMove.Add(new BoardSquare(originalSquare.Piece, new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));
                
                return new AttemptToMove(false,"Invalid move! The piece can only move to adjacent squares!", invalidMove/*, originalSquare.Piece.Owner, targetSquare.Piece.Owner*/);
            }
            
            //Sending move block
            
            var boardSquaresToUpdate = new List<BoardSquare>();
            
            boardSquaresToUpdate.Add(new BoardSquare(new Piece(GlobalFields.PieceType.None, new Player(GlobalFields.PlayerType.None, Guid.Empty)), new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));
            boardSquaresToUpdate.Add(new BoardSquare(originalSquare.Piece, new Vector2(targetSquare.Coordinates.X, targetSquare.Coordinates.Y)));
            
            Debug.Log($"GATEWAY: Sending move: {originalSquare.Piece.PieceType} from [{boardSquaresToUpdate[0].Coordinates.X}, {boardSquaresToUpdate[0].Coordinates.Y}] to [{boardSquaresToUpdate[1].Coordinates.X}, {boardSquaresToUpdate[1].Coordinates.Y}]");
            
            return new AttemptToMove(true,"Move Successful!", boardSquaresToUpdate/*, targetSquare.Piece.Owner, originalSquare.Piece.Owner*/);
        }
        
        public bool CanCapture(BoardSquare[,] board, BoardSquare originalSquare, BoardSquare targetSquare)
        {
            int dx = (int)(targetSquare.Coordinates.X - originalSquare.Coordinates.X);
            int dy = (int)(targetSquare.Coordinates.Y - originalSquare.Coordinates.Y);

            // Must move exactly 2 spaces in a straight line or diagonal
            bool isValidJump =
                (Math.Abs(dx) == 2 && dy == 0) ||    // horizontal jump
                (dx == 0 && Math.Abs(dy) == 2) ||    // vertical jump
                (Math.Abs(dx) == 2 && Math.Abs(dy) == 2); // diagonal jump

            if (!isValidJump) return false;

            int midX = (int)originalSquare.Coordinates.X + dx / 2;
            int midY = (int)originalSquare.Coordinates.Y + dy / 2;

            var origin = board[(int)originalSquare.Coordinates.X, (int)originalSquare.Coordinates.Y];
            var middle = board[midX, midY];
            var target = board[(int)targetSquare.Coordinates.X, (int)targetSquare.Coordinates.Y];

            // There must be an enemy piece in the middle and an empty target square
            return middle.Piece.PieceType != GlobalFields.PieceType.None && middle.Piece.Owner.PlayerId != origin.Piece.Owner.PlayerId && target.Piece.PieceType == GlobalFields.PieceType.None;
        }
    }
}