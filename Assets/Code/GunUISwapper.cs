using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUISwapper : MonoBehaviour
{
    public static Action<int> gunSwapper;
    private void OnEnable() {
        gunSwapper+=HandleUISwap;
    }
    private void HandleUISwap(int index){

    }
    private void OnDisable() {
        gunSwapper-=HandleUISwap;
    }
}
