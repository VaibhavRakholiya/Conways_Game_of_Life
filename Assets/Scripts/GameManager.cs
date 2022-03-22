using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int PatternNo=-1;
    public GameObject cell_prefab;
    public int grid_Size=5;
    public Cell[,] grid;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateGrid(grid_Size);
        StartCoroutine(LifeCycle());
    }
    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            DrawPattern();
        }
    }
    private void DrawPattern()
    {
        if (PatternNo > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) )
            {
                int i = hit.collider.GetComponent<Cell>().index_i;
                int j = hit.collider.GetComponent<Cell>().index_j;
                if (PatternNo == 1)
                {
                    // Draw Oscilator on Grid.
                    if(j-1 > 0)
                    grid[i, j - 1].ToggleCell();
                    grid[i, j].ToggleCell();
                    if(j+1 < grid_Size)
                    grid[i, j + 1].ToggleCell();
                }
                else if(PatternNo == 2)
                {
                    // Draw Block on Grid.
                    if(i+1 < grid_Size)
                    grid[i+1, j].ToggleCell();
                    if(j+1 <grid_Size)
                    grid[i,j+1].ToggleCell();
                    grid[i, j].ToggleCell();
                }
                else if (PatternNo == 3)
                {
                    // Draw Glider on Grid.
                    TurnOnCell(i, j);
                    TurnOnCell(i+1, j-1);
                    TurnOnCell(i+1, j-2);
                    TurnOnCell(i, j-2);
                    TurnOnCell(i-1, j-2);

                }

            }
        }
    }
    private void TurnOnCell(int i,int j)
    {
        if(i>0 && i < grid_Size && j>0 && j < grid_Size)
        {
            grid[i, j].ToggleCell();
        }
    }
    private IEnumerator LifeCycle()
    {
        CheckNeighbours();
        LifeControl();
        yield return new WaitForSeconds(0.50f);
        StartCoroutine(LifeCycle());
    }
    private void CheckNeighbours()
    {
        for (int i = 0; i < grid_Size; i++)
        {
            for (int j = 0; j < grid_Size; j++)
            {
                int neighbours = 0;
                if(i+1<grid_Size)
                {
                    if (grid[j, i + 1].isalive)
                        neighbours++;
                }
                if(j+1<grid_Size)
                {
                    if (grid[j + 1, i].isalive)
                        neighbours++;
                }
                if(i-1>=0)
                {
                    if (grid[j, i - 1].isalive)
                        neighbours++;
                }
                if(j-1>0)
                {
                    if (grid[j - 1, i].isalive)
                        neighbours++;
                }
                if(j+1 < grid_Size && i+1 < grid_Size)
                {
                    if (grid[j + 1, i + 1].isalive)
                        neighbours++;
                }
                if(j-1 >=0 && i+1 <grid_Size)
                {
                    if(grid[j-1,i+1].isalive)
                    {
                        neighbours++;
                    }
                }
                if(j+1 < grid_Size && i-1>=0)
                {
                    if(grid[j+1,i-1].isalive)
                    {
                        neighbours++;
                    }
                }
                if (j - 1 >= 0 && i - 1 >= 0)
                {
                    if (grid[j - 1, i - 1].isalive)
                    {
                        neighbours++;
                    }
                }
                grid[j, i].Neighbours = neighbours;
            }
        }
    }
    private void LifeControl()
    {
        for (int i=0;i<grid_Size;i++)
        {
            for(int j=0;j<grid_Size;j++)
            {
                    if(grid[j,i].isalive)
                    {
                            if(grid[j,i].Neighbours != 2 && grid[j,i].Neighbours !=3)
                            {
                                 grid[j, i].ToggleCell();
                            }
                    }
                    else
                    {
                        if(grid[j,i].Neighbours == 3)
                        {
                                grid[j, i].ToggleCell();
                        }
                    }
            }
        }
    }
    public void CreateGrid(int _grid_Size)
    {
        grid_Size = _grid_Size;
        UIManager.instance.CurrentGrid_Size_Text.text = "Grid Size : " + grid_Size;
        Camera.main.transform.position = new Vector3(grid_Size, grid_Size/2, -10f);
        Camera.main.orthographicSize = (grid_Size + grid_Size) / 2.5f;
        grid = new Cell[grid_Size, grid_Size];
        for(int i=0;i<grid_Size;i++)
        {
            for(int j=0;j<grid_Size;j++)
            {
                GameObject cell_gameObject = Instantiate(cell_prefab, new Vector2(i, j), Quaternion.identity);
                Cell cell = cell_gameObject.GetComponent<Cell>();
                cell.index_i = i;
                cell.index_j = j;
                grid[i, j] = cell;
                RandomlysetAlive(cell);
            }
        }
    }
    public void ClearGrid()
    {
        foreach (Cell item in grid)
        {
            item.setFalse();
        }
    }
    public void DestroyGrid()
    {

        foreach (Cell item in grid)
        {
            Destroy(item.gameObject);
        }
    }
    private void RandomlysetAlive(Cell cell)
    {
        if (Random.Range(0,100) > 80)
        {
            cell.ToggleCell();
        }
    }
}
