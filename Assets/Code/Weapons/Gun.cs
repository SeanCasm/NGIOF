using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Gun : MonoBehaviour
{
    #region EventHandler
    public static event EventHandler<GunCurrentInfo> OnShoot;

    public class GunCurrentInfo : EventArgs
    {
        public int gunIndex,currentAmmo;
        public float reloadTime,currentLoadTime,ammoBulletSize;
        public float ammoBulletMaxSize;
    }
    #endregion
    #region Properties
    [Header("Settings")]
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
    public int ID{get=>iD;}
    protected GameObject bullet;
    protected int currentAmmo;
    public int CurrentAmmo{get=>currentAmmo;}
    public float loadProgress{get;set;}
    public static Action instaLoad;
    public HandsForGrab GunGrabType{get=>grabType;}
    public enum HandsForGrab
    {
        one, two
    }

    protected List<GameObject> bullets;
    public Transform shootPoint{get;set;}
    #endregion
    private void Awake() {
        loadProgress = reloadTime;
        currentAmmo = gunProperties.totalAmmo;
    }
    private void OnEnable() {
        if(currentAmmo<=0){
            StartCoroutine(Reload());
        }
        instaLoad+=ReloadAllInstantly;
    }
    private void OnDisable() {
        instaLoad -= ReloadAllInstantly;
    }
    protected void Start() {
        shootPoint=gameObject.GetChild(0).transform;
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
                    bullets.Add(Instantiate(bullet, shootPoint.position, Quaternion.identity));
                    bullets[i].transform.SetParent(shootPoint);
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
                    bullets.Add(Instantiate(bullet, shootPoint.position, Quaternion.identity));
                    bullets[i].transform.SetParent(shootPoint);
                }
                break;
            }
            yield return null;
        }
    }
    protected IEnumerator Reload()
    {
        EventHandlerFunction();
        while (loadProgress < reloadTime)
        {
            loadProgress += 0.1f;
            yield return new WaitForSeconds(.1f);
        }
        currentAmmo = gunProperties.totalAmmo;
    }
    #endregion
    public virtual void Shoot(){
        currentAmmo--;
        EventHandlerFunction();
        if (currentAmmo <= 0)
        {
            loadProgress = 0;
            StartCoroutine(Reload());
            return;
        }
    }
    private void ReloadAllInstantly(){
        StopAllCoroutines();
        currentAmmo=gunProperties.totalAmmo;
        EventHandlerFunction();
    }
    private void EventHandlerFunction(){
        OnShoot?.Invoke(this, new GunCurrentInfo
        {
            gunIndex = iD,
            ammoBulletSize = gunProperties.bulletWidth*currentAmmo,
            reloadTime = reloadTime,
            currentLoadTime=loadProgress,
            ammoBulletMaxSize=gunProperties.bulletWidth*gunProperties.totalAmmo,
            currentAmmo=currentAmmo,
        });
    }  
    protected virtual void SetDirection(Bullet gunBullet){
        if (transform.root.localScale.x > 0) gunBullet.direction = transform.right;
        else gunBullet.direction = -transform.right;
    }
}
