using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(TerrainManagement))]
public class TerrainManagementEditor : Editor
{


    public static int iteration = 0;

    public override void OnInspectorGUI()
    {
        TerrainManagement myTarget = (TerrainManagement)target; //make an object reference to the terrain script

        #region Field Creators
        myTarget.TerrainMaster = (GameObject)EditorGUILayout.ObjectField("Terrain master", myTarget.TerrainMaster, typeof(GameObject), true);
        #endregion

        #region Restart button
        if (GUILayout.Button("Reset everything"))
        {
            if (iteration > 0)
            {
                myTarget.Restart();
                Debug.Log("Terrain's have been reset");
                iteration = 0;
            }
            else if(iteration == 0)
            {
                Debug.Log("Terrain's have already been reset!");
            }
        }
        #endregion

        #region Store All Terrains to a 2D Array Button
        if (GUILayout.Button("Store all terrains into a 2D array"))
        {
            myTarget.TerrainList = myTarget.TerrainMaster.GetComponentsInChildren<Terrain>(); //store all terrain's in the TerrainMaster object into an array called TerrainList.

            if (Mathf.Sqrt(myTarget.TerrainList.Length) % 4 == 0 || myTarget.TerrainList.Length == 4) //check if the amount of terrain's in the TerrainList is valid
            {
                myTarget.TerrainGrid = new Terrain[(int)Mathf.Sqrt(myTarget.TerrainList.Length), (int)Mathf.Sqrt(myTarget.TerrainList.Length)];
                Debug.Log("Will proceed -> amount of terrain tiles are divisiable by 4 and are square roots");
                myTarget.AssignTerrains();
                iteration = 1;
            }
            else
            {
                Debug.Log("Cannot proceed. The amount of terrain's entered are not a multiple of 4 and is a squared number");

            } // end check for terrainlist value
        } // end Store terrains button
        #endregion
         
        #region Set the Neighbors of Each Terrain button
        if (GUILayout.Button("Set neighbors of each terrain"))
        {
            if(iteration == 1)
            {
                myTarget.SetNeighbors();
                iteration = 2;
            }
            else
            {
                Debug.Log("Terrain's have not been stored yet!");
            }
        }
        #endregion

        #region Create Scenes button
        if (GUILayout.Button("Create Scenes"))
        {
            if (iteration == 2)
            {
                myTarget.CreateScenes();
                iteration = 3;
                Debug.Log("Iteration is now " + iteration);
            }
            else
            {
                Debug.Log("Terrain's have not been assigned neighbors yet!");
            }
        }
        #endregion
    }


}
