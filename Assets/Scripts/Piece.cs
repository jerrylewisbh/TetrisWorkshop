using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int moveSpeed = -1;
    public float fallTime = 1;
    public float timeCount = 0;

    public Vector3 spawnOffset;

    private static int width = 10;
    private static int height = 20;


    private bool accelerate = false;

    private Bounds bounds;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        transform.position += spawnOffset;
    }

    // Update is called once per frame
    void Update()
    {

        accelerate = Input.GetKey(KeyCode.DownArrow);

        timeCount += Time.deltaTime;


        if (timeCount > (accelerate ? fallTime / 30 : fallTime))
        {
            transform.Translate(0, -1, 0, Space.World);


            if (!ValidPos())
            {
                transform.Translate(0, 1, 0, Space.World);


                AddToGrid();
                ProcessLines();

                this.enabled = false;
                FindObjectOfType<SpawnManager>().SpawnRandomPiece();


            }


            timeCount = 0;
        }


        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1), 90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(1, 0, 0), Space.World);


            if (!ValidPos())
            {
                transform.Translate(new Vector3(-1, 0, 0), Space.World);
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            transform.Translate(new Vector3(-1, 0, 0), Space.World);

            if (!ValidPos())
            {
                transform.Translate(new Vector3(1, 0, 0), Space.World);

            }


        }


    }


    private void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.FloorToInt(child.position.x);
            int roundedY = Mathf.FloorToInt(child.position.y);

            grid[roundedX, roundedY] = child;

        }
    }

    private bool ValidPos()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.FloorToInt(child.position.x);
            int roundedY = Mathf.FloorToInt(child.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }

        }
        return true;

    }

    private void ProcessLines()
    {
        for (int row = height - 1; row >= 0; row--)
        {
            if (IsLineFull(row))
            {
                DeleteLine(row);
                MoveDown(row);
            }
        }
    }

    private void MoveDown(int row)
    {
        for (int y = row; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].transform.Translate(Vector3.down, Space.World);

                }
            }
        }
    }

    private void DeleteLine(int row)
    {
        for (int column = 0; column < width; column++)
        {
            Destroy(grid[column, row].gameObject);
            grid[column, row] = null;
        }
    }

    private bool IsLineFull(int row)
    {

        for (int column = 0; column < width; column++)
        {

            if (grid[column, row] == null)
            {
                return false;
            }
        }

        return true;
    }


}


