using UnityEngine;
using System.Collections;

public class TestCollision : MonoBehaviour {


    void OnCollisionEnter(Collision col)
    {
        print("collided with " + col.gameObject.name);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
