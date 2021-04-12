using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] int totalPellets;
    [Tooltip("Angle between pellets")]
    [SerializeField]int angle;
    [SerializeField]int maxAngle,minAngle;
    private int pelletsShooted = 0;
    new void Start()
    {
        base.Start();
        StartCoroutine(base.WaitBulletLoad(gunProperties.totalAmmo * totalPellets));
    }
    public override void Shoot()
    {
        base.Shoot();
        for (int i = maxAngle; i >= minAngle; i -= angle)
        {
            var v = bullets[pelletsShooted];
            v.transform.SetParent(null);
            v.SetActive(true);
            Bullet bullet = v.GetComponent<Bullet>();
            base.SetDirection(bullet);
            Quaternion rotation = Quaternion.Euler(0, 0, i);
            bullet.direction = rotation * bullet.direction;

            bullet.gun = this;
            pelletsShooted++;
        }
        if (pelletsShooted == gunProperties.totalAmmo * totalPellets) pelletsShooted = 0;//resets the pellets count
    }
}
