using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(fileName="New GunClass", menuName="ScriptableObjects/Gun/GunClass")]
public class GunClass : ScriptableObject
{
    [SerializeField]AssetReference[] classGuns;
    public AssetReference[] ClassGuns{get=>classGuns;}
}
