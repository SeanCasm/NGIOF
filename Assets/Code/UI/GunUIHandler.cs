using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Handle of update the gun UI.
/// </summary>
public class GunUIHandler : MonoBehaviour
{
    [SerializeField]GunUI[] gunUI;
    [System.Serializable]
    public class GunUI
    {
        public GameObject gunInterface;
        public Image gunImage, bulletImage;
        public RectTransform gunBullets;
        public RectTransform loadBar;
        public float currentLoadTime{get;set;}
        public Gun.GunCurrentInfo e{get;set;}
        public bool CheckReload(){
            if (currentLoadTime <e.reloadTime && e.currentAmmo == 0) return true;
            return false;
        }
        public void UpdateBullets(){
            gunBullets.sizeDelta.Set(e.ammoBulletSize,gunBullets.sizeDelta.y);
        }
    }
    public static Action<int,bool> swap;
    public static Action<Gun>gunInterface;
    private void OnEnable() {
        swap+=SwappAmmo;
        Gun.OnShoot += AmmoUIUpdateHandler;
        gunInterface+=SetGunUI;
        Gun.instaLoad+=ReloadAllInstantly;
    }
    private void OnDisable() {
        swap -= SwappAmmo;
        Gun.OnShoot -= AmmoUIUpdateHandler;
        gunInterface-=SetGunUI;
        Gun.instaLoad -= ReloadAllInstantly;
    }
    private void ReloadAllInstantly(){
        StopAllCoroutines();
        foreach(var v in gunUI){
            v.loadBar.sizeDelta=new Vector2(0,v.loadBar.sizeDelta.y);
            v.gunBullets.sizeDelta=new Vector2(v.gunBullets.sizeDelta.x,v.e.ammoBulletMaxSize);
            v.e.currentLoadTime=v.e.reloadTime;
        }
    }
    private void AmmoUIUpdateHandler(object sender,Gun.GunCurrentInfo e){
        var sizeDelta=gunUI[e.gunIndex].gunBullets.sizeDelta;
        sizeDelta =new Vector2(sizeDelta.x,e.ammoBulletSize);
        gunUI[e.gunIndex].gunBullets.sizeDelta=sizeDelta;
        if(e.currentAmmo ==0)StartCoroutine(Reload(e));
    }
    /// <summary>
    /// Reloads a gun ammo UI.
    /// </summary>
    IEnumerator Reload(Gun.GunCurrentInfo e){
        var gun = gunUI[e.gunIndex];
        var sizeDelta=gun.loadBar.sizeDelta;
        //this float represent the loading bar increment per miliseconds.
        //126 is the load bar width on UI.
        //0.1f represents the miliseconds
        float sizeIncrement=126/e.reloadTime*0.1f;
        while(e.currentLoadTime<e.reloadTime){
            sizeDelta=new Vector2(sizeDelta.x+=sizeIncrement,sizeDelta.y);
            gun.loadBar.sizeDelta=sizeDelta;
            e.currentLoadTime +=0.1f;
            gun.e = e;
            yield return new WaitForSeconds(0.1f);
        }
        gun.loadBar.sizeDelta=new Vector2(0,gun.loadBar.sizeDelta.y);//Sets the width back to zero. 
        gun.gunBullets.sizeDelta=new Vector2(gun.gunBullets.sizeDelta.x,gun.e.ammoBulletMaxSize);
    }
    /// <summary>
    /// Sets guns UI at the game start.
    /// </summary>
    /// <param name="gunInterface"></param>
    private void SetGunUI(Gun gunInterface) {
        var iD=gunInterface.ID;
        var str=gunInterface.gunProperties;
        var gun=gunUI[gunInterface.ID];
        gun.gunImage.sprite=str.icon;
        gun.bulletImage.sprite=str.bullet;
        gun.gunBullets.sizeDelta=new Vector2(str.bulletHeight,str.totalAmmo * str.bulletWidth);
    }
    private void SwappAmmo(int current,bool active){
        StopAllCoroutines();
        gunUI[current].gunInterface.SetActive(active);
        if(active && gunUI[current].e!=null ){
            if(gunUI[current].CheckReload())StartCoroutine(Reload(gunUI[current].e));
            gunUI[current].UpdateBullets();
        } 
    }
}