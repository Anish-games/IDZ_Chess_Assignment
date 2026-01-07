using System.Collections.Generic;
using Chess.Scripts.Core;

namespace Chess.Scripts.Strategies
{
    public class PawnMoveStrategy : IMoveStrategy
    {
        public List<Move> GetPossibleMoves(int currentRow, int currentColumn, bool isEnemy)
        {
            var moves = new List<Move>();

            int direction = isEnemy ? -1 : 1;
            int startingRow = isEnemy ? 6 : 1;

            int forwardRow = currentRow + direction;
            if (PieceManager.IsWithinBounds(forwardRow, currentColumn) &&
                !PieceManager.Instance.IsSquareOccupied(forwardRow, currentColumn))
            {
                moves.Add(new Move(forwardRow, currentColumn));

                int doubleForwardRow = currentRow + (2 * direction);
                if (currentRow == startingRow &&
                    PieceManager.IsWithinBounds(doubleForwardRow, currentColumn) &&
                    !PieceManager.Instance.IsSquareOccupied(doubleForwardRow, currentColumn))
                {
                    moves.Add(new Move(doubleForwardRow, currentColumn));
                }
            }

            int[] captureColumns = { currentColumn - 1, currentColumn + 1 };
            foreach (int captureCol in captureColumns)
            {
                if (PieceManager.IsWithinBounds(forwardRow, captureCol) &&
                    PieceManager.Instance.IsEnemyAt(forwardRow, captureCol, isEnemy))
                {
                    moves.Add(new Move(forwardRow, captureCol, isCapture: true));
                }
            }

            return moves;
        }
    }
}
