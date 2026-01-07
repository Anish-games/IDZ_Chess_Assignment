using UnityEngine;

namespace Chess.Scripts.Core
{
    public class MoveHandler : MonoBehaviour
    {
        private int _targetRow;
        private int _targetColumn;
        private ChessPiece _selectedPiece;
        private bool _isCapture;

        public void Initialize(ChessPiece piece, int row, int column, bool isCapture)
        {
            _selectedPiece = piece;
            _targetRow = row;
            _targetColumn = column;
            _isCapture = isCapture;

            if (GetComponent<Collider2D>() == null)
            {
                var boxCollider = gameObject.AddComponent<BoxCollider2D>();
                boxCollider.size = new Vector2(1f, 1f);
            }
        }

        private void OnMouseDown()
        {
            if (_selectedPiece == null) return;

            if (_isCapture)
            {
                var enemyPiece = PieceManager.Instance.GetPieceAt(_targetRow, _targetColumn);
                if (enemyPiece != null)
                {
                    PieceManager.Instance.UnregisterPiece(enemyPiece);
                    Destroy(enemyPiece.gameObject);
                }
            }

            _selectedPiece.MoveTo(_targetRow, _targetColumn);
            ChessBoardPlacementHandler.Instance.ClearHighlights();
            GameManager.Instance?.ClearSelection();
        }
    }
}
