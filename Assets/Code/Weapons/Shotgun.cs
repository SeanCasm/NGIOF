using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] int totalPellets;
    private int pelletsShooted = 0;
    new void Start()
    {
        base.Start();
        StartCoroutine(base.WaitBulletLoad(gunProperties.totalAmmo * totalPellets));
    }
    public override void Shoot()
    {
        base.Shoot();
        if(base.currentAmmo>-1){
            for (int i = 30; i >= -30; i -= 15)
            {
                var v = bullets[pelletsShooted];
                v.transform.SetParent(null);
                v.SetActive(true);
                Bullet bullet = v.GetComponent<Bullet>();
                base.SetDirection(bullet);
                Quaternion rotation = Quaternion.Euler(0, 0, i);
                bullet.direction = rotation * bullet.direction;

                bullet.gun = this;
                bullet.damage = damage;
                pelletsShooted++;
            }
        }
    }
}
