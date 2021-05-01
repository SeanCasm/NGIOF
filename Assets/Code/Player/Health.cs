using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Player{
    public class Health : HealthBase<int>
    {
        [SerializeField]SpriteRenderer[] bodyRenderers;
        private int currentHealth;
        private bool invulnerable;
        public static System.Action onDeath;
        public float invTime{get;set;}=2;
        public bool Invulnerable{get=>invulnerable;set{
            invulnerable=value;
            if(invulnerable)Invoke("DisableInv",invTime);
        }}
        public static bool isAlive=true;
        private void OnEnable() {
            DeathScreen.retry+=ResetHealth;
        }
        private void OnDisable() {
            DeathScreen.retry-=ResetHealth;
        }
        private void Awake() {
            currentHealth=health;
        }
        #region Health methods
        public IEnumerator VisualFeedBack(Color color)
        {
            foreach(var e in bodyRenderers){
                e.color=color;
            }
            yield return new WaitForSeconds(0.1f);
            ForeachSprites();
        }
        private void DisableInv(){
            invulnerable=false;
            invTime=2;//back to default
        }
        public void OnDeath(){
            PlayerController pController=GetComponent<PlayerController>();
            isAlive=pController.Movement =false;
            pController.IsDeath=true;
            gameObject.SetActive(false);
            onDeath.Invoke();
            DeathScreen.deathPause.Invoke();         
        }

        public override void AddDamage(int amount)
        {
            if(!invulnerable){
                currentHealth -= amount;
                StartCoroutine(VisualFeedBack(Color.red));
                HealthUIHandler.health.Invoke(currentHealth);
                Invulnerable =true;
                if (currentHealth <= 0) OnDeath();
            }
        }
        public void AddHealth(int amount){
            if(currentHealth<health){
                var dif=health-currentHealth;
                StartCoroutine(VisualFeedBack(Color.green));
                currentHealth+=amount-dif;
                HealthUIHandler.health.Invoke(currentHealth);
            }
        }
        private void ForeachSprites(){
            foreach (var e in bodyRenderers)
            {
                e.color = Color.white;
            }
        }
        #endregion
        #region Resets
        private void ResetHealth(){
            currentHealth=health;
            invulnerable=false;
            HealthUIHandler.health(currentHealth);
            ForeachSprites();
        }
        #endregion
    }
}