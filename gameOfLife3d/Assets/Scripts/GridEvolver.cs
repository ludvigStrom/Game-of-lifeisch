using UnityEngine;
using System.Collections;

public class GridEvolver : MonoBehaviour
{
    public float time = 1;
    Grid grid;
    private bool[,,] newStates;
    private bool stateIsChanged = false;
    public bool randomspawn;
    public float spawnProbebility;


    void Awake()
    {
        //Debug.Log("get grid component");
        grid = GetComponent<Grid>();
        //Debug.Log("init new states");
        //lägger till -1 för att kolla om GetLength ger fel längd!!!!!!! test test test
        newStates = new bool[grid.cells.GetLength(0), grid.cells.GetLength(1), grid.cells.GetLength(2)];
    }

    void Start()
    {
        //Debug.Log("Start co routine");
        StartCoroutine("UpdateCells");
    }

    void Update()
    {
        if (stateIsChanged)
        {
            //Debug.Log("Updates Array");
            Cell currentCell;

            //uppdatera arrayen
            for (int x = 0; x < grid.cells.GetLength(0); x++)
            {
                for (int y = 0; y < grid.cells.GetLength(1); y++)
                {
                    for (int z = 0; z < grid.cells.GetLength(2); z++)
                    {
                        currentCell = grid.cells[x, y, z];
                        if(newStates[x,y,z] == true)
                        {
                            currentCell.GetComponent<Cell>().alive = true;
                        }else
                            currentCell.GetComponent<Cell>().alive = false;
                    }
                }
            }
            stateIsChanged = false;
        }
    }

    IEnumerator UpdateCells()
    {
        while (true)
        {
            //Debug.Log("Inside evolve loop");
            if (!stateIsChanged)
            {
                Cell currentCell = grid.cells[0, 0, 0]; ;

                //loopar all celler och kollar om de skall leva eller dö
                //for (int x = 1; x <= grid.cells.GetLength(0); x++)
                for (int x = 1; x <= grid.cells.GetLength(0); x++)
                {
                    for (int y = 1; y <= grid.cells.GetLength(1); y++)
                    {
                        for (int z = 1; z <= grid.cells.GetLength(2); z++)
                        {
                            //Debug.Log("Testing:" + x + y + z);
                            //KOLLA SÅ ATT DEN ÄR IN RANGE!!!! FEL FEL FEL
                            if (x >= 0 && x <= grid.gridSizeX - 1 && y >= 0 && y <= grid.gridSizeY - 1 && z >= 0 && z <= grid.gridSizeZ - 1)
                            {
                                //Debug.Log("Should be in bound:" + x + y + z);
                                currentCell = grid.cells[x, y, z];
                                bool stateOfCurrentCell = currentCell.GetComponent<Cell>().alive;
                                int currentCellsNeighbourCount = grid.getNumOfNeighbours(currentCell);

                                if (stateOfCurrentCell == false && currentCellsNeighbourCount == 3 || stateOfCurrentCell == true && currentCellsNeighbourCount >= 2 && currentCellsNeighbourCount <= 6)
                                {
                                    //Debug.Log("Alive!");
                                    newStates[x, y, z] = true;
                                }
                                else
                                {
                                    //Debug.Log("Dead!");
                                    newStates[x, y, z] = false;
                                }

                                //spontainous spawn?
                                if (randomspawn)
                                {
                                    float randy = Random.Range(0, 100.0F);
                                    if (randy >= spawnProbebility)
                                    {
                                        newStates[x, y, z] = true;
                                    }
                                }
                            }
                        }
                    }
                }
                stateIsChanged = true;
                yield return new WaitForSeconds(time);
            }        
        }
    }
}