using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ClassHandler : MonoBehaviour
{
    public static int classIndex;
    public void SetClass(int index){
         switch(index){
            case 1:
                classIndex=index;
            break;
         }
    }
}
