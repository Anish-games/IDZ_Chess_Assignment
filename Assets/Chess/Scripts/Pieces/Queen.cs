using Chess.Scripts.Core;
using Chess.Scripts.Strategies;

namespace Chess.Scripts.Pieces
{
    public class Queen : ChessPiece
    {
        protected override void Start()
        {
            MoveStrategy = new QueenMoveStrategy();
            base.Start();
        }
    }
}
