using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Speed of the bullet, multiplied by Time.deltaTime so needs a high value.")]
    [SerializeField]float speed;
    private Rigidbody2D rigid;
    public float damage { get; set; }
        
    public Vector3 direction{get;set;}
    protected void Awake() {
        rigid=GetComponent<Rigidbody2D>();
    }
    protected void FixedUpdate() {
        rigid.velocity=direction.normalized*speed*Time.deltaTime;
    }
    protected void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag){
            case "Enemy":
                var component=other.GetComponentInParent<Ball>();
                component.Break();
            break;
            case "Ground":
                Destroy(gameObject);
            break;
        }
    }
}
