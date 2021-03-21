using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Collectibles{
    public class Health: Collectible<int>
    {
        private Game.Player.Health health;
        private void OnTriggerEnter2D(Collider2D other) {
            if((health=other.GetComponent<Game.Player.Health>())!=null){
                health.AddHealth(amount);
            }
        }
    }
} 
