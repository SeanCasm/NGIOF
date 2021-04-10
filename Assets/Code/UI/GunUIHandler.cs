using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunUIHandler : MonoBehaviour
{
    public static GunUIHandler instance;
    [SerializeField]GunUI[] gunUI;
    [System.Serializable]
    public class GunUI
    {
        public GameObject gunInterface;
        public Image gunImage, bulletImage;
        public RectTransform gunBullets;
        public RectTransform loadBar;
        public float currentLoadTime{get;set;}
        public Gun.GunZeroAmmoEventArgs e;
    }
    private float currentLoadTime;
    private void Start() {
        instance=this;
    }
    private void OnEnable() {
        Gun.OnAmmoZero+=AmmoUIUpdateHandler;
    }
    private void OnDisable() {
        Gun.OnAmmoZero -= AmmoUIUpdateHandler;
    }
    public void AmmoUIUpdateHandler(object sender,Gun.GunZeroAmmoEventArgs e){
        var sizeDelta=gunUI[e.gunIndex].gunBullets.sizeDelta;
        sizeDelta =new Vector2(sizeDelta.x,e.ammoBulletSize);
        gunUI[e.gunIndex].gunBullets.sizeDelta=sizeDelta;
        if(e.ammoBulletSize ==0)StartCoroutine(Reload(e));
    }
    /// <summary>
    /// Reloads a gun ammo UI.
    /// </summary>
    IEnumerator Reload(Gun.GunZeroAmmoEventArgs e){
        var gun = gunUI[e.gunIndex];
        var sizeDelta=gun.loadBar.sizeDelta;
        //this float represent the loading bar increment per miliseconds.
        //126 is the load bar width on UI.
        //0.1f represents the miliseconds
        float sizeIncrement=126/e.reloadTime*0.1f;
        while(e.currentLoadTime<e.reloadTime){
            currentLoadTime=e.currentLoadTime;
            sizeDelta=new Vector2(sizeDelta.x+=sizeIncrement,sizeDelta.y);
            gun.loadBar.sizeDelta=sizeDelta;
            e.currentLoadTime +=0.1f;
            gun.e = e;
            yield return new WaitForSeconds(0.1f);
        }
        gun.loadBar.sizeDelta=new Vector2(0,gun.loadBar.sizeDelta.y);//Sets the width back to zero. 
        gun.gunBullets.sizeDelta=new Vector2(gun.gunBullets.sizeDelta.x,e.ammoBulletMaxSize);
    }
    /// <summary>
    /// Sets guns UI at the game start.
    /// </summary>
    /// <param name="gunInterface"></param>
    public void SetGunUI(Gun gunInterface) {
        var iD=gunInterface.ID;
        var str=gunInterface.gunProperties;
        var gun=gunUI[gunInterface.ID];
        gun.gunImage.sprite=str.icon;
        gun.bulletImage.sprite=str.bullet;
        gun.gunBullets.sizeDelta=new Vector2(str.bulletHeight,str.totalAmmo * str.bulletWidth);
    }
    public void SwappAmmo(int current,bool active){
        gunUI[current].currentLoadTime=currentLoadTime;
        gunUI[current].gunInterface.SetActive(active);
        StopAllCoroutines();
        if(active){
            if (gunUI[current].currentLoadTime < gunUI[current].e.reloadTime) StartCoroutine(Reload(gunUI[current].e));
        }
    }
}