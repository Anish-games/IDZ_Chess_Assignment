using Chess.Scripts.Core;
using Chess.Scripts.Strategies;

namespace Chess.Scripts.Pieces
{
    public class Knight : ChessPiece
    {
        protected override void Start()
        {
            MoveStrategy = new KnightMoveStrategy();
            base.Start();
        }
    }
}
