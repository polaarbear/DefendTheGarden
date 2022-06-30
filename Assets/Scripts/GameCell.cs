using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell
{
    public Vector2 CellCoords { get; private set; }
    public bool IsOccupied = false;

    public GameCell(int xIndex, int yIndex)
    {
        CellCoords = new Vector2(xIndex + GameMaster.CELL_OFFSET, yIndex + GameMaster.CELL_OFFSET);
    }
}
