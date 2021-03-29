using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Gun : MonoBehaviour
{
    #region Encapsulated class
    public class GunZeroAmmoEventArgs : EventArgs
    {
        public int gunIndex;
        public float reloadTime;
        public float ammoBulletSize;
    }
    #endregion
    #region Properties
    [Header("Settings")]
    [SerializeField]protected float damage;
    [SerializeField]float reloadTime;
     
    [Tooltip("ID on the class.")]
    [SerializeField] int iD;
    [Tooltip("The grab type from the gun, one hand for small guns, and two hands for big guns.")]
    [SerializeField]HandsForGrab grabType; 
    [SerializeField]protected AssetReference bulletReference;
    public Properties gunProperties;
    [System.Serializable]
    public struct Properties
    {
        public Sprite icon;
        public Sprite bullet;
        public int totalAmmo;
        public float bulletWidth;
        public float bulletHeight;
    }
    public static event EventHandler<GunZeroAmmoEventArgs> OnAmmoZero;
    public int ID{get=>iD;}
    protected GameObject bullet;
    protected int currentAmmo;
    public int CurrentAmmo{get=>currentAmmo;}
    public HandsForGrab GunGrabType{get=>grabType;}
    public enum HandsForGrab
    {
        one, two
    }

    protected List<GameObject> bullets;
    protected Transform shootPosition;
    #endregion
    protected void Start() {
        currentAmmo=gunProperties.totalAmmo;
        shootPosition=gameObject.GetChild(0).transform;
        bullets = new List<GameObject>();
        bulletReference.LoadAssetAsync<GameObject>().Completed += OnLoadDone;
    }
    protected void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj){
        bullet=obj.Result;
        bullet.SetActive(false);
    }
    #region IEnumerators
    protected IEnumerator WaitBulletLoad()
    {
        while (true)
        {
            if (bullet != null)
            {
                for (int i = 0; i < gunProperties.totalAmmo; i++)
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
    protected IEnumerator Reload()
    {
        float time = 0;
        while (time < reloadTime)
        {
            time += 0.1f;
            yield return new WaitForSeconds(.1f);
        }
        currentAmmo = gunProperties.totalAmmo;
        EventHandlerFunction();
    }
    #endregion
    public virtual void Shoot(){
        currentAmmo--;
        EventHandlerFunction();
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }
    private void EventHandlerFunction(){
        OnAmmoZero?.Invoke(this, new GunZeroAmmoEventArgs
        {
            gunIndex = iD,
            ammoBulletSize = gunProperties.bulletWidth * currentAmmo,
            reloadTime = reloadTime
        });
    }  
    protected virtual void SetDirection(Bullet gunBullet){
        if (transform.root.localScale.x > 0) gunBullet.direction = transform.right;
        else gunBullet.direction = -transform.right;
         
    }
}
