using Chess.Scripts.Core;
using Chess.Scripts.Strategies;

namespace Chess.Scripts.Pieces
{
    public class King : ChessPiece
    {
        protected override void Start()
        {
            MoveStrategy = new KingMoveStrategy();
            base.Start();
        }
    }
}
