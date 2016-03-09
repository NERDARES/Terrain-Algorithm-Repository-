using UnityEngine;
using System.Collections;

public class LoadAdditiveTerrain : MonoBehaviour {

    public GameObject _terrainManager;

	// Use this for initialization
	void Start ()
    {
        
	}

    // Update is called once per frame
    void Update()
    {
        
    }


    public void loadTerrain()
    {
        var t1 = _terrainManager.GetComponent<TerrainLoader>();

        for (int row = 0; row < t1.TerrainGrid.GetLength(0); row++)
        {
            for (int col = 0; col < t1.TerrainGrid.GetLength(1); col++)
            {
                if (this.transform.position.y >= t1.TerrainGrid[row, col].transform.position.y)
                {
                    t1.TerrainGrid[row, col].gameObject.SetActive(true);
                    print(t1.TerrainGrid[row, col].name + " Has been activated!");
                }
                else
                {
                    t1.TerrainGrid[row, col].gameObject.SetActive(false);
                    print(t1.TerrainGrid[row, col].name + " Has been DISABLED!");
                }
            }
        }
    }
}
