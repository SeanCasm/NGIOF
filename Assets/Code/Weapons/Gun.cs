using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Gun : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]protected int totalAmmo;
    [SerializeField]protected float damage;
    [SerializeField]float reloadTime;
     
    [Tooltip("ID on the class.")]
    [SerializeField] int iD;
    [Tooltip("The grab type from the gun, one hand for small guns, and two hands for big guns.")]
    [SerializeField]HandsForGrab grabType; 
    [SerializeField]protected AssetReference bulletReference;
    protected GameObject bullet;
    protected int currentAmmo;
    public bool selected{get;set;}
    protected float bulletSize; 
    public HandsForGrab GunGrabType{get=>grabType;}
    public enum HandsForGrab
    {
        one, two
    }

    protected List<GameObject> bullets;
    protected Transform shootPosition;
    protected void Start() {
        currentAmmo=totalAmmo;
        shootPosition=gameObject.GetChild(0).transform;
        bullets = new List<GameObject>();
        bulletReference.LoadAssetAsync<GameObject>().Completed += OnLoadDone;
    }
    protected void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj){
        bullet=obj.Result;
        bullet.SetActive(false);
    }
    protected IEnumerator WaitBulletLoad()
    {
        while (true)
        {
            if (bullet != null)
            {
                for (int i = 0; i < totalAmmo; i++)
                {
                    bullets.Add(Instantiate(bullet, shootPosition.position, Quaternion.identity));
                    bullets[i].transform.SetParent(shootPosition);
                }
                break;
            }
            yield return null;
        }
    }
    protected IEnumerator WaitBulletLoad(int bulletAmount)
    {
        while (true)
        {
            if (bullet != null)
            {
                for (int i = 0; i < bulletAmount; i++)
                {
                    bullets.Add(Instantiate(bullet, shootPosition.position, Quaternion.identity));
                    bullets[i].transform.SetParent(shootPosition);
                }
                break;
            }
            yield return null;
        }
    }
    public virtual void Shoot(){
        currentAmmo--;
        GunUIHandler.gunAmmo.Invoke(iD, -bulletSize,reloadTime);
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }
    protected IEnumerator Reload()
    {
        float time=0;
        while (time<reloadTime)
        {
            time+=0.1f;
            yield return new WaitForSeconds(.1f);
        }
        currentAmmo=totalAmmo;
        GunUIHandler.gunAmmo.Invoke(iD, currentAmmo*bulletSize,reloadTime);
    }
    protected virtual void SetDirection(Bullet gunBullet){
        if (transform.root.localScale.x > 0) gunBullet.direction = transform.right;
        else gunBullet.direction = -transform.right;
         
    }
}
