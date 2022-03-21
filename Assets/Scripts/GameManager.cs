using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject cell_prefab;
    public int grid_Width;
    public int grid_Height;
    public Cell[,] grid;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateGrid();
    }
    private void Update()
    {
        CheckNeighbours();
    }
    private void CheckNeighbours()
    {
        for (int i = 0; i < grid_Height; i++)
        {
            for (int j = 0; j < grid_Width; j++)
            {
                int neighbours = 0;
                if(i+1<grid_Height)
                {
                    if (grid[j, i + 1].isalive)
                        neighbours++;
                }
                if(j+1<grid_Width)
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
                if(j+1 < grid_Height && i+1 < grid_Width)
                {
                    if (grid[j + 1, i + 1].isalive)
                        neighbours++;
                }
                if(j-1 >=0 && i+1 <grid_Height)
                {
                    if(grid[j-1,i+1].isalive)
                    {
                        neighbours++;
                    }
                }
                if(j+1 < grid_Width && i-1>=0)
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
    private void CreateGrid()
    {
        grid = new Cell[grid_Height, grid_Width];
        for(int i=0;i<grid_Height;i++)
        {
            for(int j=0;j<grid_Width;j++)
            {
                GameObject cell_gameObject = Instantiate(cell_prefab, new Vector2(i, j), Quaternion.identity);
                Cell cell = cell_gameObject.GetComponent<Cell>();
                RandomlysetAlive(cell);
                grid[i, j] = cell;
            }
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
