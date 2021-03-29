using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase<T> : MonoBehaviour
{
    [SerializeField]protected T health;
    public virtual void AddDamage(T damage){

    }
    public virtual void AddHealth(T amount){
        
    }
}
