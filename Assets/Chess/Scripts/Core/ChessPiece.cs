using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ChessPiece : MonoBehaviour
    {
        [SerializeField] private int row;
        [SerializeField] private int column;
        [SerializeField] private bool isEnemy;

        public int Row => row;
        public int Column => column;
        public bool IsEnemy => isEnemy;

        protected IMoveStrategy MoveStrategy { get; set; }

        protected virtual void Start()
        {
            var tile = ChessBoardPlacementHandler.Instance.GetTile(row, column);
            if (tile != null)
            {
                transform.position = tile.transform.position;
            }

            if (PieceManager.Instance != null)
            {
                PieceManager.Instance.RegisterPiece(this);
            }
        }

        protected virtual void OnDestroy()
        {
            if (PieceManager.Instance != null)
            {
                PieceManager.Instance.UnregisterPiece(this);
            }
        }

        private void OnMouseDown()
        {
            ChessBoardPlacementHandler.Instance.ClearHighlights();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.SelectPiece(this);
            }

            var moves = GetPossibleMoves();
            foreach (var move in moves)
            {
                if (move.IsCapture)
                {
                    ChessBoardPlacementHandler.Instance.HighlightCapture(move.Row, move.Column, this, true);
                }
                else
                {
                    ChessBoardPlacementHandler.Instance.Highlight(move.Row, move.Column, this, false);
                }
            }
        }

        public List<Move> GetPossibleMoves()
        {
            if (MoveStrategy == null)
            {
                Debug.LogWarning($"No move strategy assigned to {gameObject.name}");
                return new List<Move>();
            }

            return MoveStrategy.GetPossibleMoves(row, column, isEnemy);
        }

        public void MoveTo(int newRow, int newColumn)
        {
            SetPosition(newRow, newColumn);

            var tile = ChessBoardPlacementHandler.Instance.GetTile(newRow, newColumn);
            if (tile != null)
            {
                transform.position = tile.transform.position;
            }
        }

        public void SetPosition(int newRow, int newColumn)
        {
            int oldRow = row;
            int oldCol = column;
            row = newRow;
            column = newColumn;

            if (PieceManager.Instance != null)
            {
                PieceManager.Instance.UpdatePiecePosition(this, oldRow, oldCol);
            }
        }
    }
}
