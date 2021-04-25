using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Props.Spawn
{
    public sealed class Item : Spawner
    {
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
                Instantiate(base.prefabsLoaded[Random.Range(0,totalPrefabsLoaded-1)],base.SpawnerPositionGenerator(),Quaternion.identity,null);
                yield return new WaitForSeconds(Random.Range(minTimeSpawn,maxTimeSpawn));
            }
        }
    }
}