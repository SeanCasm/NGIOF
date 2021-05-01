using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneObjects:MonoBehaviour
{
    public static List<GameObject> allObjects=new List<GameObject>();
    public static void ClearAll(){
        foreach(GameObject e in allObjects){
            Destroy(e);
        }
    }
}
