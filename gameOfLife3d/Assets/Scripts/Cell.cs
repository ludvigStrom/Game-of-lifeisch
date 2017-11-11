using System;
using UnityEngine;

public class Cell : MonoBehaviour {

    public float growthRate;
    public bool alive;
    public Color aliveColor;
    public Color deadColor;
    public float size;
    public GameObject cell;
    
    private int gridX;
    private int gridY;
    private int gridZ;

    private Cell(bool _alive, int _size)
    {
        alive = _alive;
        size = _size;
    }


    public GameObject getCellGameObject()
    {
        return cell;
    }

    void Start()
    {
        if (alive == true)
        {
            GetComponent<Renderer>().enabled = true;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            transform.localScale = new Vector3(0f, 0f, 0f);
        }

        int boolInt = alive ? 1 : 0;
        changeColor(boolInt);
        
    }

	void Update () {
        growOrShrink();
    }

    private void growOrShrink()
    {
        if (alive && transform.lossyScale.x < 1)
        {
            GetComponent<Renderer>().enabled = true;
            //grow
            float grow = growthRate * Time.deltaTime;
            transform.localScale += new Vector3(grow, grow, grow);

            if(transform.localScale.x > 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            changeColor(transform.lossyScale.x);

        }
        else if (!alive && transform.lossyScale.x > 0)
        {
            //shrink
            float grow = growthRate * Time.deltaTime;
            transform.localScale -= new Vector3(grow, grow, grow);

            if (transform.localScale.x <= 0)
            {
                GetComponent<Renderer>().enabled = false;
                transform.localScale = new Vector3(0, 0, 0);
            }

            changeColor(transform.lossyScale.x);
        }
    }

    internal void setCellGameObject(GameObject currentCell)
    {
        cell = currentCell;
        Debug.Log("currentCell:" + currentCell);
    }

    private void changeColor(float amount)
    {
        GetComponent<Renderer>().material.color = Color.Lerp(deadColor, aliveColor, amount); 
    }

    public int getGridX()
    {
        return this.gridX;
    }

    public int getGridY()
    {
        return this.gridY;
    }

    public int getGridZ()
    {
        return this.gridZ;
    }


    public void setGridX(int val)
    {
        gridX = val;
    }

    public void setGridY(int val)
    {
        gridY = val;
    }

    public void setGridZ(int val)
    {
        gridZ = val;
    }
}