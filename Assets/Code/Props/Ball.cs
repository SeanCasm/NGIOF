using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace Game.Props{
public class Ball : ScreenObjectMovement
    {
        #region Properties
        [Header("Settings")]
        [SerializeField] int damage;
         
        [Tooltip("Parent points, child points will increment after parent ball get destroyed")]
        [SerializeField] int points;
        [Header("Child settings")]
        [SerializeField] int totalChilds;
        [SerializeField] protected AssetReference childBall;
        protected List<GameObject> ball;
        int oi;
        public static float globalSpeedMultiplier = 1;
        public int Points { get => points; set => points = value; }
        public byte parentLevel { get; set; } = 0;
        #endregion
        #region Unity Methods
        new void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            ball = new List<GameObject>();
            Game.Props.Spawn.Ball.totalBallsRemaining++;
            direction = Random.insideUnitCircle.normalized;
            childBall.LoadAssetAsync<GameObject>().Completed += OnComplete;
        }
        new void FixedUpdate()
        {
            lastVelocity = rigid.velocity;
            rigid.velocity = direction.normalized * speed*globalSpeedMultiplier;
        }
        new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }
        protected void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Player":
                    other.GetComponentInParent<Game.Player.Health>().AddDamage(damage);
                    break;
                case "Bullet":
                    oi++;
                    if (oi == 1) Break();
                    break;
            }
        }
        #endregion
        protected void OnComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            for (int i = 0; i < totalChilds; i++)
            {
                ball.Add(obj.Result);
            }
        }
         
        public void Break()
        {
            Game.Props.Spawn.Ball.ballsDestroyedInGame++;
            if (parentLevel == 0)
            {
                Game.Props.Spawn.Ball.parentBalls--;
                foreach (var item in ball)
                {
                    var ball = Instantiate(item, transform.position, Quaternion.identity, null);
                    var component = ball.GetComponent<Ball>();
                    component.parentLevel++;
                    component.Points += this.points + 1;
                    ball.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2);
                }
            }
            int score = ScoreHandler.Score += points;
            ScoreUIHandler.score.Invoke(score);
            Game.Props.Spawn.Ball.totalBallsRemaining--;
            Destroy(gameObject);
        }
    }
}