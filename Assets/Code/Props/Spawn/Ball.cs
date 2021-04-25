using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace Game.Props.Spawn{
public sealed class Ball : Spawner
    {
        public static int tierLvl = 1;
        private const int lvlOneParentBallsOnScreen = 4;
        public static int totalBallsRemaining;
        public static int parentBalls;
        public static int ballsDestroyedInGame;
        private void OnEnable() {
            Game.Player.Health.onDeath+=ResetData;
        }
        private void OnDisable() {
            Game.Player.Health.onDeath-=ResetData;
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
        #region IEnumerators
        IEnumerator Generator()
        {
            while (Game.Player.Health.isAlive)
            {
                switch (tierLvl)
                {
                    case 1:
                        if (parentBalls <= lvlOneParentBallsOnScreen && totalBallsRemaining < lvlOneParentBallsOnScreen)
                        {
                            Instantiate(prefabsLoaded[0].gameObject, SpawnerPositionGenerator(), Quaternion.identity, null);
                            parentBalls++;
                        }
                        break;
                    case 2:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 1 && totalBallsRemaining < lvlOneParentBallsOnScreen + 1)
                        {
                            Instantiate(prefabsLoaded[Random.Range(0, 2)].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            parentBalls++;
                        }
                        break;
                    case 3:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 2 && totalBallsRemaining < lvlOneParentBallsOnScreen + 2)
                        {
                            Instantiate(prefabsLoaded[Random.Range(1, 3)].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            parentBalls++;
                        }
                        break;
                    case 4:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 3 && totalBallsRemaining < lvlOneParentBallsOnScreen + 3)
                        {
                            Instantiate(prefabsLoaded[Random.Range(2, 4)].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            parentBalls++;
                        }
                        break;
                    case 5:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 4 && totalBallsRemaining < lvlOneParentBallsOnScreen + 4)
                        {
                            Instantiate(prefabsLoaded[3].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            Game.Props.Ball.globalSpeedMultiplier=1.2f;
                            parentBalls++;
                        }
                        break;
                    case 6:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 7 && totalBallsRemaining < lvlOneParentBallsOnScreen + 7)
                        {
                            Instantiate(prefabsLoaded[Random.Range(3,5)].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            parentBalls++;
                        }
                        break;
                    case 7:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 8 && totalBallsRemaining < lvlOneParentBallsOnScreen + 8)
                        {
                            Instantiate(prefabsLoaded[4].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            parentBalls++;
                        }
                        break;
                    case 8:
                        if (parentBalls <= lvlOneParentBallsOnScreen + 10 && totalBallsRemaining < lvlOneParentBallsOnScreen + 10)
                        {
                            Instantiate(prefabsLoaded[4].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            Instantiate(prefabsLoaded[5].gameObject, base.SpawnerPositionGenerator(), Quaternion.identity, null);
                            Game.Props.Ball.globalSpeedMultiplier=1.5f;
                            parentBalls++;
                        }
                        break;
                }
                var time = Random.Range(minTimeSpawn, maxTimeSpawn);
                yield return new WaitForSeconds(time);
            }
        }
        #endregion
        private void ResetData(){
            tierLvl=1;totalBallsRemaining=0;
        }
    }
}
 