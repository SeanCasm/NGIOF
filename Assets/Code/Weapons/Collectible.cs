using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collectible<T> : MonoBehaviour
{
    [SerializeField]CollectibleType collectType=CollectibleType.none;
    [SerializeField]protected T amount;
    public CollectibleType CollectType {get=>collectType;set=>collectType=value;}
}
