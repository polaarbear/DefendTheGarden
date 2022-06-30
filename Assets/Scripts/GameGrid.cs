using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    private readonly int GRID_WIDTH = 51;
    private readonly int GRID_HEIGHT = 29;
    private List<List<GameCell>> _GameCells = new List<List<GameCell>>();
    public void GenerateEmptyGrid()
    {
        for (int y = 0; y < GRID_HEIGHT; y++)
        {
            List<GameCell> row = new List<GameCell>();
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                row.Add(new GameCell(x, y));
            }
            _GameCells.Add(row);
        }
    }

    public GameCell FindClickedCell(Vector2 cellVector)
    {
        foreach (List<GameCell> row in _GameCells)
        {
            foreach (GameCell cell in row)
            {
                if (cell.CellCoords == cellVector)
                {
                    return cell;
                }
            }
        }
        return null;
    }
}
