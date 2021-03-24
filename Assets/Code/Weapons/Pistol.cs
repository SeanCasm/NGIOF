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
        var obj = bullets[currentAmmo - 1];
        Bullet gunBullet = obj.GetComponent<Bullet>();
        base.bulletSize = gunBullet.BulletInterfaceSize;
        base.Shoot();
        obj.SetActive(true);
        obj.transform.SetParent(null);
        base.SetDirection(gunBullet);
        obj.transform.eulerAngles = transform.eulerAngles;
        gunBullet.gun=this;
        gunBullet.damage = damage;
    }
}
