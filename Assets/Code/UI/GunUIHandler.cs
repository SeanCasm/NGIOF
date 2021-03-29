using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunUIHandler : MonoBehaviour
{
    
    [SerializeField]GameObject[] gunInterface;
    [SerializeField]Image[] gunImage,bulletImage;
    [SerializeField]RectTransform[] gunBullets;
    [SerializeField]RectTransform[] loadBars;
    public static Action<int,bool> ammoSwapper;
    public static Action<Gun> gunInterfaceSetter;
    private void OnEnable() {
        Gun.OnAmmoZero+=AmmoUIUpdateHandler;
        ammoSwapper+=SwappAmmo;
        gunInterfaceSetter+=SetGunUI;
    }
    private void AmmoUIUpdateHandler(object sender,Gun.GunZeroAmmoEventArgs e){
        var sizeDelta=gunBullets[e.gunIndex].sizeDelta;
        sizeDelta =new Vector2(sizeDelta.x,e.ammoBulletSize);
        gunBullets[e.gunIndex].sizeDelta=sizeDelta;
        if(e.ammoBulletSize ==0)StartCoroutine(Reload(e.gunIndex,e.reloadTime));
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
    /// <summary>
    /// Sets guns UI at the game start.
    /// </summary>
    /// <param name="gunInterface"></param>
    private void SetGunUI(Gun gunInterface) {
        var iD=gunInterface.ID;
        var str=gunInterface.gunProperties;
        gunImage[iD].sprite=str.icon;
        bulletImage[iD].sprite=str.bullet;
        gunBullets[iD].sizeDelta=new Vector2(str.bulletHeight,str.totalAmmo * str.bulletWidth);
    }
    private void SwappAmmo(int current,bool active){
        gunInterface[current].SetActive(active);
    }
    private void OnDisable() {
        Gun.OnAmmoZero -= AmmoUIUpdateHandler;
        ammoSwapper -= SwappAmmo;
        gunInterfaceSetter -= SetGunUI;
    }
}
