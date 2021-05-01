using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Props.Spawn
{
    public sealed class Item : Spawner
    {
        const float shieldProb=40f,healthProb=40f,reloadProb=20f;
        private void OnEnable() {
            DeathScreen.deathPause+=ResetData;
            DeathScreen.deathPause +=ResetCoroutine;
        }
        private void OnDisable() {
            DeathScreen.deathPause -= ResetData;
            DeathScreen.deathPause -= ResetCoroutine;
        }
        private new void Start()
        {
            totalPrefabsLoaded = prefabToSpawn.Length;
            for (int i = 0; i < prefabToSpawn.Length; i++)
            {
                prefabToSpawn[i].LoadAssetAsync<GameObject>().Completed += OnComplete;
            }
        }
        private new void OnComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            base.OnComplete(obj);
            if (totalPrefabsLoaded == currentPrefabOnLoad) StartCoroutine(Generator());
        }
        IEnumerator Generator(){
            while(Game.Player.Health.isAlive){
                yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                float n=Random.Range(0f,1f);
                if(n<=40)Instantiate(base.prefabsLoaded[0]);
                else if(n>40 && n<=80)Instantiate(base.prefabsLoaded[1]);
                else if (n > 80) Instantiate(base.prefabsLoaded[2]);
            }
        }
        private void Instantiate(GameObject obj){
            GameSceneObjects.allObjects.Add(Instantiate(obj, base.SpawnerPositionGenerator(), Quaternion.identity, null));
        }
        private void ResetData()
        {
            GameSceneObjects.ClearAll();
        }
        private void ResetCoroutine()
        {
            StartCoroutine(Generator());
        }
    }
}