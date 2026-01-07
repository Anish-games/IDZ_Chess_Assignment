using System.Collections.Generic;
using Chess.Scripts.Core;

namespace Chess.Scripts.Strategies
{
    public class QueenMoveStrategy : IMoveStrategy
    {
        private readonly RookMoveStrategy _rookStrategy = new RookMoveStrategy();
        private readonly BishopMoveStrategy _bishopStrategy = new BishopMoveStrategy();

        public List<Move> GetPossibleMoves(int currentRow, int currentColumn, bool isEnemy)
        {
            var moves = new List<Move>();
            moves.AddRange(_rookStrategy.GetPossibleMoves(currentRow, currentColumn, isEnemy));
            moves.AddRange(_bishopStrategy.GetPossibleMoves(currentRow, currentColumn, isEnemy));
            return moves;
        }
    }
}
