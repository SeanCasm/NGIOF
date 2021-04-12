using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class BallSpawner : MonoBehaviour
{
    [Tooltip("Balls prefabs to instantiate, IMPORTANT: the index of the array represents the ball tier.")]
    [SerializeField] AssetReference[] ballsToSpawn;
    [SerializeField] Vector2 spawnerLeftSide,spawnerRightSide;
    [Tooltip("Time to spawn balls throught time")]
    [SerializeField]float minTimeSpawn,maxTimeSpawn;
    List<GameObject> ballTiers=new List<GameObject>();
    public static int tierLvl=1;
    private const int lvlOneParentBallsOnScreen=4,lvlTwoParentBallsOnScreen=5,lvlThreeParentBallsOnScreen=6;
    public static int totalBallsRemaining;
    private int totalBallTiers,currentBallLoad;
    public static int parentBalls;
    private void Start() {
        totalBallTiers=ballsToSpawn.Length;
        for (int i = 0; i < ballsToSpawn.Length; i++)
        {
            ballsToSpawn[i].LoadAssetAsync<GameObject>().Completed+=OnComplete;
        }
    }
    private void OnComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        ballTiers.Add(obj.Result);
        currentBallLoad++;
        if(totalBallTiers==currentBallLoad)StartCoroutine(Generator());
    }
    #region IEnumerators
    IEnumerator Generator(){
        while(Game.Player.Health.isAlive){
            switch(tierLvl){
                case 1:
                    if(parentBalls<=lvlOneParentBallsOnScreen && totalBallsRemaining<lvlOneParentBallsOnScreen){
                        Instantiate(ballTiers[0].gameObject, SpawnerPositionGenerator(), Quaternion.identity, null);
                        parentBalls++;
                    }
                break;
                case 2:
                    if (parentBalls <= lvlTwoParentBallsOnScreen && totalBallsRemaining < lvlTwoParentBallsOnScreen)
                    {
                        Instantiate(ballTiers[Random.Range(0, 2)].gameObject, SpawnerPositionGenerator(), Quaternion.identity, null);
                        parentBalls++;
                    }
                break;
                case 3:
                    if (parentBalls <= lvlTwoParentBallsOnScreen && totalBallsRemaining < lvlThreeParentBallsOnScreen)
                    {
                        Instantiate(ballTiers[Random.Range(1, 3)].gameObject, SpawnerPositionGenerator(), Quaternion.identity, null);
                        parentBalls++;
                    }
                break;
            }
            var time=Random.Range(minTimeSpawn, maxTimeSpawn);
            yield return new WaitForSeconds(time);
        }
    }
    #endregion
    private Vector2 SpawnerPositionGenerator(){
        var xPos= Random.Range(spawnerLeftSide.x,spawnerRightSide.x);
        var yPos=Random.Range(spawnerLeftSide.y,spawnerRightSide.y);
        return new Vector2(xPos,yPos);
    }
}