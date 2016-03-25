using UnityEngine;
using System;
using System.Threading;
using System.Collections;

public class TestScript : MonoBehaviour
{
    public void Sleep()
    {
        print("Sleeping");
        Thread.Sleep(5000);
        
    }
}
