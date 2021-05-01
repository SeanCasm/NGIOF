using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Props{
    /// <summary>
    /// Health class exclusive to exploding props on game.
    /// </summary>
    public class Health : HealthBase<int>
    {
        [Tooltip("Hurtbox identificator, resquired in acquired guns or items in game.")]
        [SerializeField]int iD;
        public int ID{get=>iD;}
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private void Awake() {
            animator=GetComponent<Animator>();
            spriteRenderer=GetComponent<SpriteRenderer>();
        }
        public override void AddDamage(int damage)
        {
            health-=damage;
            if(health<=0)Explode();
        }
        private void Explode(){
            animator.SetTrigger("Explode");
        }
        public IEnumerator VisualFeedBack()
        {
            spriteRenderer.color=Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color=Color.white;
        }
    }
}
 
