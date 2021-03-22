using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName="New GunClassHandler",menuName="ScriptableObjects/Gun/GunClassHandler")]
public class GunClassHandler : ScriptableObject
{
    [SerializeField]GunClass[] gunClasses;
    public AssetReference[] GetClass(int index){
        return gunClasses[index].ClassGuns;
    }
}
