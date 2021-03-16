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
    [Tooltip("The grab type from the gun, one hand for small guns, and two hands for big guns.")]
    [SerializeField]HandsForGrab grabType; 
    [SerializeField]protected AssetReference bulletReference;
    protected GameObject bullet;
    public HandsForGrab GunGrabType{get=>grabType;}
    public enum HandsForGrab
    {
        one, two
    }

    protected List<GameObject> bullets;
    protected Transform shootPosition;
    protected void Start() {
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
        totalAmmo--;
    }
    protected virtual void SetDirection(Bullet gunBullet){
        if (transform.root.localScale.x > 0) gunBullet.direction = transform.right;
        else gunBullet.direction = -transform.right;
    }
}
