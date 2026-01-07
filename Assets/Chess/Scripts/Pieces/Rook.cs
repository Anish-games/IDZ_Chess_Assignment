using Chess.Scripts.Core;
using Chess.Scripts.Strategies;

namespace Chess.Scripts.Pieces
{
    public class Rook : ChessPiece
    {
        protected override void Start()
        {
            MoveStrategy = new RookMoveStrategy();
            base.Start();
        }
    }
}
