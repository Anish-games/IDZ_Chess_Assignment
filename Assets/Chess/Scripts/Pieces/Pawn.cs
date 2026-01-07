using Chess.Scripts.Core;
using Chess.Scripts.Strategies;

namespace Chess.Scripts.Pieces
{
    public class Pawn : ChessPiece
    {
        protected override void Start()
        {
            MoveStrategy = new PawnMoveStrategy();
            base.Start();
        }
    }
}
