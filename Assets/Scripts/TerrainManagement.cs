using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;
using System;
using System.Collections;

//[ExecuteInEditMode]
public class TerrainManagement : MonoBehaviour
{

    public GameObject TerrainMaster; //Object to store the terrain master to get the child component informantion

    [HideInInspector]
    public Terrain[] TerrainList; // Array to store all the terrains in order in a list fashion

    [HideInInspector]
    public Terrain[,] TerrainGrid; //Array to store all the terrains in  a grid fashion

    public GameObject Player; //Gameobject to get player to record their positioning relative to terrain

    [HideInInspector]
    public string[] Scenes; //Store the scenes containing each tile so that you can reference them later


    public void Restart()
    {
        if (TerrainGrid != null)
        {
            for (int row = 0; row < TerrainGrid.GetLength(0); row++)
            {
                for (int col = 0; col < TerrainGrid.GetLength(1); col++)
                {
                    TerrainGrid[row, col].gameObject.SetActive(true);
                } // end col
            }

        }

        if (Scenes != null)
        {
            print("Deleteing scenes. Please wait as it may take some time to delete");
            FileUtil.DeleteFileOrDirectory("Assets/Terrain Scenes");
        }

        TerrainList = null;
        TerrainGrid = null;
        Scenes = null;

    } //Resets everything

    public void AssignTerrains()
    {
        int count = 0;
        for (int row = 0; row < TerrainGrid.GetLength(0); row++)
        {

            for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            {
                TerrainGrid[row, col] = TerrainList[count]; //put's each terrain in their respective element in TerrainGrid
                #region print statement

                //print("On row " + row);
                //print("On column " + col);
                //print("We put in " + TerrainGrid[row, col].name);
                //print("--------------------------------");

                #endregion
                count++;
            }
        }
    } //Assigns all terrains to a 2D array.

    public void SetNeighbors() 
    {

        for (int row = 0; row < TerrainGrid.GetLength(0); row++)
        {
            for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            {

                //Get the left, top, right and bottom of each terrain
                Terrain Left = (col == 0) ? null : TerrainGrid[row, col - 1];
                Terrain Top = (row == 0) ? null : TerrainGrid[row - 1, col];
                Terrain Right = (col + 1 == TerrainGrid.GetLength(1)) ? null : TerrainGrid[row, col + 1];
                Terrain Bottom = (row + 1 == TerrainGrid.GetLength(0)) ? null : TerrainGrid[row + 1, col];

                TerrainGrid[row, col].SetNeighbors(Left, Top, Right, Bottom); //set the current terrain's neighbor
                TerrainGrid[row, col].gameObject.SetActive(false); //set the terrain invisible

                #region Print statement checks

                    //string txtLeft = (Left == null) ? "Nothing" : Left.name;
                    //string txtTop = (Top == null) ? "Nothing" : Top.name;
                    //string txtRight = (Right == null) ? "Nothing" : Right.name;
                    //string txtBottom = (Bottom == null) ? "Nothing" : Bottom.name;

                    //print("Using terrain " + TerrainGrid[row, col].name);
                    //print("The left of this terrain is " + txtLeft);
                    //print("The Top of this terrain is " + txtTop);
                    //print("The Right of this terrain is " + txtRight);
                    //print("The Bottom of this terrain is " + txtBottom);
                    //print("------------------------------------------------");

                #endregion

            } // end col
        }

    } // Sets all neighbors of each terrain

    public void CreateScenes()
    {

        #region Store scene names
        Scenes = new string[TerrainList.Length]; // Give the length of the scene array to the length of the terrain grid array

        int count1 = 0;
        for (int row = 0; row < TerrainGrid.GetLength(0); row++)
        {
            for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            {
                Scenes[count1] = TerrainGrid[row, col].gameObject.name + "Scene"; // Assign each row and column of the scene array as each terrain grids name
                count1++;
            } // end col
        } // end row

        print("Finished storing the scene names");
        #endregion

        #region Make Scenes
        var initialScene = EditorSceneManager.GetActiveScene(); // hold a reference for the original scene
        foreach (var _scene in Scenes)
        {
            print("Creating: " + _scene);
            var currentScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive); //create the scene additivley
            EditorSceneManager.SetActiveScene(currentScene); //set that scene to active
            EditorSceneManager.SaveScene(currentScene, "Assets/Terrain Scenes/" + _scene +".unity", false); //save current scene

        }
        EditorSceneManager.SetActiveScene(initialScene); //go back to initial scene
        #endregion

        #region Put scenes into build settings
            //var newEditorScenes = new EditorBuildSettingsScene[Scenes.Length];

            //int count = 0;
            //for (int row = 0; row < TerrainGrid.GetLength(0); row++)
            //{
            //    for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            //    {
            //        var sceneToAdd = new EditorBuildSettingsScene("Assets/Terrain Scenes/" + Scenes[count], true);
            //        newEditorScenes[count] = sceneToAdd;
            //        count++;
            //    } // end col
            //} // end row

            //EditorBuildSettings.scenes = newEditorScenes;

            //print("Finished creating scenes!");
            #endregion
    }// Creates the scenes

    public void LoadTerrains()
    {
        for (int row = 0; row < TerrainGrid.GetLength(0); row++)
        {
            for (int col = 0; col < TerrainGrid.GetLength(1); col++)
            {
                float terrainGridZPos = TerrainGrid[row, col].transform.position.z; // get the world position of the current tile Z coordinate
                const int bufferZ = 500; // set a fixed difference for the detector
                int finalZOutput = (int)terrainGridZPos + bufferZ; // add the difference to the world position for z coordinates

                float terrainGridXPos = TerrainGrid[row, col].transform.position.x; //get the world position of the current tile X coordinate
                const int bufferX = 500; //set a fixed difference for the player
                int finalXOutput = (int)terrainGridXPos + bufferX;

            } // end col
        } // end row
    }

}


