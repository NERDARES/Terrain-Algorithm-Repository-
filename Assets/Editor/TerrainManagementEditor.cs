using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(TerrainManagement))]
public class TerrainManagementEditor : Editor
{

    //treat all of the following booleans as buttons. true means they have been pressed. false means they have not been pressed.

    public bool resetPressed = false;
    public bool storeTerrainPressed = false;
    public bool setNeighborsPressed = false;

    public override void OnInspectorGUI()
    {
        TerrainManagement myTarget = (TerrainManagement)target; //make an object reference to the terrain script

        #region Field Creators

        myTarget.TerrainMaster = (GameObject)EditorGUILayout.ObjectField("Terrain master", myTarget.TerrainMaster, typeof(GameObject), true);

        #endregion

        #region Restart button
        if (GUILayout.Button("Reset everything"))
        {
                myTarget.Restart();          // Removes Terrain from each scene and puts them back to terrain master.
                                             // Deletes Scenes
                Debug.Log("Terrain's have been reset");
                resetPressed = true;         // reset button has been pressed
                storeTerrainPressed = false; // store terrain button has not been pressed
                setNeighborsPressed = false; // set neighbors button has not been pressed
        }
        #endregion

        #region Store All Terrains to a 2D Array Button
        if (GUILayout.Button("Store all terrains into a 2D array"))
        {
            myTarget.TerrainList = myTarget.TerrainMaster.GetComponentsInChildren<Terrain>(); //store all terrain's in the TerrainMaster object into an array called TerrainList.
            
            if ((Mathf.Sqrt(myTarget.TerrainList.Length) % 4 == 0 || myTarget.TerrainList.Length == 4) && resetPressed) //check if the amount of terrain's in the TerrainList is valid
            {
                myTarget.TerrainGrid = new Terrain[(int)Mathf.Sqrt(myTarget.TerrainList.Length), (int)Mathf.Sqrt(myTarget.TerrainList.Length)];
                Debug.Log("Will proceed -> amount of terrain tiles are divisiable by 4 and are square roots.");
                myTarget.AssignTerrains();
                resetPressed = false;
                storeTerrainPressed = true;
                setNeighborsPressed = false;
            }
            else
            {
                Debug.Log("The amount of terrain's entered are not a multiple of 4 \n or not a square number \n or RESET BUTTON has not been pressed \n Or you have already pressed this button!");
                Debug.Log("Cannot proceed. Check console for more info.");

            } // end check for terrainlist value
        } // end Store terrains button
        #endregion
         
        #region Set the Neighbors of Each Terrain button
        if (GUILayout.Button("Set neighbors of each terrain"))
        {
            if(storeTerrainPressed)
            {
                myTarget.SetNeighbors();
                resetPressed = false;
                storeTerrainPressed = false;
                setNeighborsPressed = true;
                Debug.Log("Terrain's have been stored!");
            }
            else
            {
                Debug.Log("Terrain's have not been stored yet! Or you have already pressed this button!");
            }
        }
        #endregion

        #region Create Scenes button
        if (GUILayout.Button("Create Scenes"))
        {
            if (setNeighborsPressed)
            {
                myTarget.CreateScenes();
                resetPressed = false;
                storeTerrainPressed = false;
                setNeighborsPressed = false;
                Debug.Log("WARNING: DO NOT REMOVE OR UNLOAD SCENES IF YOU WANT TO RESET EVERYTHING ELSE.");
                Debug.Log("Scenes have been created! Check Console for IMPORTANT MESSAGES");
            }
            else
            {
                Debug.Log("Terrain's have not been assigned neighbors yet! Or you have already pressed this button!");
            }
        }
        #endregion

    }


}
