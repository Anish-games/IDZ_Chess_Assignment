using Chess.Scripts.Core;
using Chess.Scripts.Strategies;

namespace Chess.Scripts.Pieces
{
    public class Bishop : ChessPiece
    {
        protected override void Start()
        {
            MoveStrategy = new BishopMoveStrategy();
            base.Start();
        }
    }
}
