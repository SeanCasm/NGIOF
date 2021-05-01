using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Collectibles{
    public class Health: Collectible
    {
        [SerializeField] int healthRestore;
        new void Awake()
        {
            base.Awake();
        }
        new void Start() {
            base.Start();
        }
        new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            if (other.gameObject.CompareTag("Player"))
            {
                var pHealth = other.gameObject.GetComponent<Game.Player.Health>();
                pHealth.AddHealth(healthRestore);
            }
        }
    }
} 