using System.Collections.Generic;
using Chess.Scripts.Core;

namespace Chess.Scripts.Strategies
{
    public class BishopMoveStrategy : IMoveStrategy
    {
        public List<Move> GetPossibleMoves(int currentRow, int currentColumn, bool isEnemy)
        {
            var moves = new List<Move>();

            int[][] directions = {
                new[] { 1, 1 },
                new[] { 1, -1 },
                new[] { -1, 1 },
                new[] { -1, -1 }
            };

            foreach (var direction in directions)
            {
                AddMovesInDirection(moves, currentRow, currentColumn, direction[0], direction[1], isEnemy);
            }

            return moves;
        }

        private void AddMovesInDirection(List<Move> moves, int startRow, int startCol, int rowDelta, int colDelta, bool isEnemy)
        {
            int row = startRow + rowDelta;
            int col = startCol + colDelta;

            while (PieceManager.IsWithinBounds(row, col))
            {
                if (PieceManager.Instance.IsFriendlyAt(row, col, isEnemy))
                {
                    break;
                }

                if (PieceManager.Instance.IsEnemyAt(row, col, isEnemy))
                {
                    moves.Add(new Move(row, col, isCapture: true));
                    break;
                }

                moves.Add(new Move(row, col));

                row += rowDelta;
                col += colDelta;
            }
        }
    }
}
