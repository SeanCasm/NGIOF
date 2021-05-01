using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Spawner : MonoBehaviour
    {
        [SerializeField] protected AssetReference[] prefabToSpawn;
        [SerializeField] protected Vector2 spawnerLeftSide, spawnerRightSide;
        [SerializeField] protected float minTimeSpawn, maxTimeSpawn;
        protected int totalPrefabsLoaded,currentPrefabOnLoad;
        protected List<GameObject> prefabsLoaded = new List<GameObject>();
        protected void Start()
        {
            totalPrefabsLoaded = prefabToSpawn.Length;
            for (int i = 0; i < prefabToSpawn.Length; i++)
            {
                prefabToSpawn[i].LoadAssetAsync<GameObject>().Completed += OnComplete;
            }
        }
        protected virtual void OnComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            prefabsLoaded.Add(obj.Result);
            currentPrefabOnLoad++;
        }
        protected Vector2 SpawnerPositionGenerator()
        {
            var xPos = Random.Range(spawnerLeftSide.x, spawnerRightSide.x);
            var yPos = Random.Range(spawnerLeftSide.y, spawnerRightSide.y);
            return new Vector2(xPos, yPos);
        }
    }