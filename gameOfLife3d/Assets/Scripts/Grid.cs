using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public bool initGridWithRandomCells = true;

    public bool stickToGround;
    public Vector3 gridWorldSize;
    public float cellRadius; 
    public GameObject cellPrefab;
    public GameObject cubePrefab;
    public Cell[,,] cells;


    float cellDiameter;
    public int gridSizeX, gridSizeY, gridSizeZ;

    void Awake()
    {
        
        if (stickToGround)
        {
            transform.position = new Vector3(transform.localPosition.x, gridWorldSize.y / 2, transform.localPosition.z);
        }

        cellDiameter = cellRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / cellDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / cellDiameter);
        gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / cellDiameter);
        CreateGrid();
    }   

    void CreateGrid()
    {
        cells = new Cell[gridSizeX, gridSizeY, gridSizeZ];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * cellDiameter + cellRadius) + Vector3.up * (y * cellDiameter + cellRadius) + (Vector3.forward * (z * cellDiameter + cellRadius - gridWorldSize.z / 2));

                    //create a cell gameobject
                    GameObject cellPrefabilutti = Instantiate(cellPrefab, worldPoint, Quaternion.identity) as GameObject;
                    GameObject cubePrefabilutti = Instantiate(cubePrefab, worldPoint, Quaternion.identity) as GameObject;


                    //get access to cell skruipt on prefabGameobject
                    Cell cell = (Cell)cellPrefabilutti.GetComponent(typeof (Cell));

                    //set xyz for cell
                    cell.setGridX(x);
                    cell.setGridY(y);
                    cell.setGridZ(z);

                    //Debug.Log("xyz: " + x + y + z + "getGridXYZ:" + cell.getGridX() + ", " + cell.getGridY() + ", " + cell.getGridZ());

                    //assign cell to cells
                    cells[x, y, z] = cell;

                    //alive?!
                    if (initGridWithRandomCells)
                    {
                        float randy = Random.Range(0, 10.0F);
                        if (randy >= 9)
                        {
                            cell.alive = true;
                        }
                    }else
                        cell.alive = false;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, gridWorldSize);
    }

    public List<Cell> listOfNeighbours(Cell cell)
    {

        int initX = cell.getGridX() - 1;
        int initY = cell.getGridY() - 1;
        int initZ = cell.getGridZ() - 1;

        List<Cell> list = new List<Cell>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == 0 && y == 0 && z==0)
                        continue;

                    int checkX = initX + x;
                    int checkY = initY + y;
                    int checkZ = initZ + z;

                    //check if inside the grid
                    if (checkX >= 0 && checkX <= gridSizeX && checkY >= 0 && checkY <= gridSizeY && checkZ >= 0 && checkZ <= gridSizeZ)
                    {
                        list.Add(cells[checkX, checkY, checkZ]); 
                    }
                }

            }
        }
        return list;
    }

    public int getNumOfNeighbours(Cell cell)
    {
        int rtn = 0;

        List<Cell> list = listOfNeighbours(cell);

            foreach (Cell CurrentCell in list)
            {
                if(CurrentCell.alive == true)
                {
                    rtn++;
                }
            }
        return rtn;
    }

    public int getNumOfNeighboursOld(Cell cell)
    {
        int rtn = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == 0 && y == 0 && z == 0)
                    {
                        continue;
                    }

                    int checkX = cell.getGridX() + x;
                    int checkY = cell.getGridY() + y;
                    int checkZ = cell.getGridZ() + z;

                    //Debug.Log("Cell " + initX + "," + initY +","+ initZ +" have " + rtn + " neighbours.");

                    //check if inside the grid
                    if (checkX >= 0 && checkX <= gridSizeX && checkY >= 0 && checkY <= gridSizeY && checkZ >= 0 && checkZ <= gridSizeZ)
                    {
                        if(cells[checkX, checkY, checkZ].alive == true)
                        {
                            rtn++;
                        }
                    }
                }

            }
        }
        //Debug.Log("Cell " + initX + "," + initY +","+ initZ +" have " + rtn + " neighbours.");
        return rtn; 
    }
}
