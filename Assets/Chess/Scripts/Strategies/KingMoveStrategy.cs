using System.Collections.Generic;
using Chess.Scripts.Core;

namespace Chess.Scripts.Strategies
{
    public class KingMoveStrategy : IMoveStrategy
    {
        private static readonly int[][] KingOffsets = {
            new[] { 1, 0 },
            new[] { -1, 0 },
            new[] { 0, 1 },
            new[] { 0, -1 },
            new[] { 1, 1 },
            new[] { 1, -1 },
            new[] { -1, 1 },
            new[] { -1, -1 }
        };

        public List<Move> GetPossibleMoves(int currentRow, int currentColumn, bool isEnemy)
        {
            var moves = new List<Move>();

            foreach (var offset in KingOffsets)
            {
                int newRow = currentRow + offset[0];
                int newCol = currentColumn + offset[1];

                if (!PieceManager.IsWithinBounds(newRow, newCol))
                {
                    continue;
                }

                if (PieceManager.Instance.IsFriendlyAt(newRow, newCol, isEnemy))
                {
                    continue;
                }

                bool isCapture = PieceManager.Instance.IsEnemyAt(newRow, newCol, isEnemy);
                moves.Add(new Move(newRow, newCol, isCapture));
            }

            return moves;
        }
    }
}
