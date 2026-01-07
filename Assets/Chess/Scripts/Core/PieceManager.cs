using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class PieceManager : MonoBehaviour
    {
        public static PieceManager Instance { get; private set; }

        private readonly Dictionary<(int row, int col), ChessPiece> _piecePositions = 
            new Dictionary<(int row, int col), ChessPiece>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void RegisterPiece(ChessPiece piece)
        {
            var key = (piece.Row, piece.Column);
            _piecePositions[key] = piece;
        }

        public void UpdatePiecePosition(ChessPiece piece, int oldRow, int oldCol)
        {
            _piecePositions.Remove((oldRow, oldCol));
            RegisterPiece(piece);
        }

        public void UnregisterPiece(ChessPiece piece)
        {
            _piecePositions.Remove((piece.Row, piece.Column));
        }

        public ChessPiece GetPieceAt(int row, int col)
        {
            return _piecePositions.TryGetValue((row, col), out var piece) ? piece : null;
        }

        public bool IsSquareOccupied(int row, int col)
        {
            return _piecePositions.ContainsKey((row, col));
        }

        public bool IsEnemyAt(int row, int col, bool isCurrentPieceEnemy)
        {
            var piece = GetPieceAt(row, col);
            return piece != null && piece.IsEnemy != isCurrentPieceEnemy;
        }

        public bool IsFriendlyAt(int row, int col, bool isCurrentPieceEnemy)
        {
            var piece = GetPieceAt(row, col);
            return piece != null && piece.IsEnemy == isCurrentPieceEnemy;
        }

        public static bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }
    }
}
