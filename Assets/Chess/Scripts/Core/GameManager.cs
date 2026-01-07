using UnityEngine;

namespace Chess.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public ChessPiece SelectedPiece { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void SelectPiece(ChessPiece piece)
        {
            SelectedPiece = piece;
        }

        public void ClearSelection()
        {
            SelectedPiece = null;
        }
    }
}
