using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAll : Collectible
{
    new void Awake()
    {
        base.Awake();
    }
    new void Start()
    {
        base.Start();
    }
    new void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Gun.instaLoad.Invoke();
            Destroy(gameObject);
        }
    }
}
