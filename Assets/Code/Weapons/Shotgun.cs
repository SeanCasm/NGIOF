using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField]int totalPellets;
    private int pelletsShooted=0;
    new void Start() {
         
        base.Start();
        StartCoroutine(base.WaitBulletLoad(totalAmmo*totalPellets));
    }
    /// <summary>
    /// Instantiate the bullets in 5 angles.
    /// </summary>
    public override void Shoot(){
        if (base.currentAmmo > 0)
        {
            base.Shoot();
            for (int i = 50; i >= -50; i -= 25)
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
