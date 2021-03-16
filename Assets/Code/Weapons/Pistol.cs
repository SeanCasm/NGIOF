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
        var obj = bullets[totalAmmo];
        Bullet gunBullet = obj.GetComponent<Bullet>();
        obj.SetActive(true);
        obj.transform.SetParent(null);
        base.SetDirection(gunBullet);
        gunBullet.damage = damage;
        //gunBullet =null;
        if (totalAmmo <= 0)
        {
            Grab.throwGun.Invoke();
            Destroy(gameObject);
        }
    }
}
