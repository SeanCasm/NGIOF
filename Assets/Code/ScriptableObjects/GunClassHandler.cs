using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New GunClassHandler",menuName="ScriptableObjects/Gun/GunClassHandler")]
public class GunClassHandler : ScriptableObject
{
    [SerializeField]GunClass[] gunClasses;
    public GameObject[] GetClass(int index){
        return gunClasses[index].ClassGuns;
    }
}
