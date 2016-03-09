using UnityEngine;
using System.Collections;

public class TerrainLoader : MonoBehaviour
{
    [HideInInspector]
    public Terrain[] TerrainList;

    [HideInInspector]
    public Terrain[,] TerrainGrid;

    [HideInInspector]
    public bool isFinished = false;

    public GameObject detector;




    void Start()
    {
        TerrainList = GetComponentsInChildren<Terrain>();
        if (Mathf.Sqrt(TerrainList.Length) % 4 == 0 || TerrainList.Length == 4)
        {
            TerrainGrid = new Terrain[(int)Mathf.Sqrt(TerrainList.Length), (int)Mathf.Sqrt(TerrainList.Length)];
            print("Will proceed -> amount of terrain tiles are divisiable by 4 and are square roots");
            AssignTerrains();
        }
        else
        {
            print("Cannot proceed. The amount of terrain's entered are not a multiple of 4 and is a squared number");

        }

    }

    void AssignTerrains()
    {
        int count = 0;
        for (int row = 0; row < TerrainGrid.GetLength(0); row++)
        {

            for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            {

                TerrainGrid[row, col] = TerrainList[count];
                #region print statement
                /*
                print("On row " + row);
                print("On column " + col);
                print("We put in " + TerrainGrid[row, col].name);
                print("--------------------------------");
                */
                #endregion
                count++;
            }
        }

        SetNeighbors();

    }

    void SetNeighbors()
    {

        for (int row = 0; row < TerrainGrid.GetLength(0); row++)
        {
            for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            {

                Terrain Left = (col == 0) ? null : TerrainGrid[row, col - 1];
                Terrain Top = (row == 0) ? null : TerrainGrid[row - 1, col];
                Terrain Right = (col + 1 == TerrainGrid.GetLength(1)) ? null : TerrainGrid[row, col + 1];
                Terrain Bottom = (row + 1 == TerrainGrid.GetLength(0)) ? null : TerrainGrid[row + 1, col];

                TerrainGrid[row, col].SetNeighbors(Left, Top, Right, Bottom);
                TerrainGrid[row, col].gameObject.SetActive(false);

                #region Print statement checks
                /*
                string txtLeft = (Left == null) ? "Nothing" : Left.name;
                string txtTop = (Top == null) ? "Nothing" : Top.name;
                string txtRight = (Right == null) ? "Nothing" : Right.name;
                string txtBottom = (Bottom == null) ? "Nothing" : Bottom.name;

                print("Using terrain " + TerrainGrid[row, col].name);
                print("The left of this terrain is " + txtLeft);
                print("The Top of this terrain is " + txtTop);
                print("The Right of this terrain is " + txtRight);
                print("The Bottom of this terrain is " + txtBottom);
                print("------------------------------------------------");
                */
                #endregion

            } // end col
        }
        isFinished = true;

    }

    void Update()
    {
        if(isFinished)
        {

            // LoadAdditiveTerrain l1 = GetComponent<LoadAdditiveTerrain>();

            // l1.loadTerrain();

           // detector.GetComponent<LoadAdditiveTerrain>().loadTerrain();
        }

    }


}
