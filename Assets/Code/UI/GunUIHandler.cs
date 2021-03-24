using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUIHandler : MonoBehaviour
{
    [SerializeField]RectTransform[] gunBullets;
    [SerializeField]RectTransform[] loadBars;
    public static Action<int,float,float> gunAmmo;
    private void OnEnable() {
        gunAmmo+=AmmoHandler;
    }
    private void AmmoHandler(int index,float size,float time){
        var sizeDelta=gunBullets[index].sizeDelta;
        float newY=sizeDelta.y+size;
        sizeDelta =new Vector2(sizeDelta.x,newY);
        gunBullets[index].sizeDelta=sizeDelta;
        if(newY==0)StartCoroutine(Reload(index,time));
    }
    /// <summary>
    /// Reloads a gun ammo UI.
    /// </summary>
    /// <param name="index">the index of the gun in the class selected</param>
    /// <param name="time">gun time to reload</param>
    /// <returns></returns>
    IEnumerator Reload(int index,float time){
        var sizeDelta=loadBars[index].sizeDelta;
        float newSize=0;
        //this float represent the loading bar increment per miliseconds.
        //126 is the load bar width on UI.
        //0.1f represents the miliseconds
        float sizeIncrement=126/time*0.1f;
        float currentTime=0;
        while(currentTime<time){
            newSize+=sizeIncrement;
            sizeDelta=new Vector2(newSize,sizeDelta.y);
            loadBars[index].sizeDelta=sizeDelta;
            currentTime+=0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        loadBars[index].sizeDelta=new Vector2(0,loadBars[index].sizeDelta.y);//Sets the width back to zero. 
    }
    private void OnDisable() {
        gunAmmo-=AmmoHandler;
    }
}
