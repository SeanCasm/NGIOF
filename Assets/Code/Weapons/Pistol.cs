using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    new void Start()
    {
        base.Start();
        StartCoroutine(base.WaitBulletLoad());
    }
    public override void Shoot()
    {
        base.Shoot();
        if(base.currentAmmo>-1){
            var obj = bullets[currentAmmo];
            Bullet gunBullet = obj.GetComponent<Bullet>();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            base.SetDirection(gunBullet);
            obj.transform.eulerAngles = transform.eulerAngles;
            gunBullet.gun = this;
            gunBullet.damage = damage;
        }
    }
}
