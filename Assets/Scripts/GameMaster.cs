using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static readonly float CELL_OFFSET = 0.5f;

    private GameGrid _Grid;

    [SerializeField]
    private Camera _MainCam = null;

    [SerializeField]
    private GameObject _Prefab_PlantedSeed;

    private List<GameObject> _PlantedSeeds = new List<GameObject>();

    private GamePhase _CurrentPhase { get; set; } = GamePhase.PLACEMENT;

    private bool _MouseWasClicked { get; set; } = false;

    private enum GamePhase
    {
        PLACEMENT,
        SELECTION,
        GENERATION,
        ATTACK
    }

    // Start is called before the first frame update
    void Start()
    {
        _Grid = new GameGrid();
        _Grid.GenerateEmptyGrid();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_CurrentPhase)
        {
            case GamePhase.PLACEMENT:
                    ListenForPlacementClick();
                break;
            case GamePhase.SELECTION:
                break;
            case GamePhase.GENERATION:
                break;
            case GamePhase.ATTACK:
                break;
        }
    }

    private void ListenForPlacementClick()
    {
        if(_MouseWasClicked)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                PlantSeed(GetClickedCell());
                _MouseWasClicked = false;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            _MouseWasClicked = true;
        }
    }

    private Vector2 GetClickedCell()
    {
        Vector2 mousePos = _MainCam.ScreenToWorldPoint(Input.mousePosition);
        float xDecimal = (int)mousePos.x + CELL_OFFSET;
        float yDecimal = (int)mousePos.y + CELL_OFFSET;
        GameCell clickedCell = _Grid.FindClickedCell(new Vector2(xDecimal, yDecimal));
        if (clickedCell != null && !clickedCell.IsOccupied)
        {
            clickedCell.IsOccupied = true;
            return clickedCell.CellCoords;
        }
        return new Vector2(-255, -255);
    }

    private void PlantSeed(Vector2 clickedCell)
    {
        if (clickedCell.x == -255 || clickedCell.y == -255)
            return;
        else
        {
            GameObject plantedSeed = Instantiate(_Prefab_PlantedSeed, new Vector3(clickedCell.x, clickedCell.y, 0), Quaternion.identity, transform);
            _PlantedSeeds.Add(plantedSeed);
            if (_PlantedSeeds.Count >= 5)
            {
                _CurrentPhase = GamePhase.SELECTION;
            }
        }
    }
}
