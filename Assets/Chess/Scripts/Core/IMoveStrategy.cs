using System.Collections.Generic;

namespace Chess.Scripts.Core
{
    public interface IMoveStrategy
    {
        List<Move> GetPossibleMoves(int currentRow, int currentColumn, bool isEnemy);
    }

    public struct Move
    {
        public int Row { get; }
        public int Column { get; }
        public bool IsCapture { get; }

        public Move(int row, int column, bool isCapture = false)
        {
            Row = row;
            Column = column;
            IsCapture = isCapture;
        }
    }
}
