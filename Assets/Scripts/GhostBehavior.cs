using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{

    public Ghost ghost {get; private set;}

    //public float duration;

    private void Awake(){
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public virtual void Enable(){

        Enable(Random.Range(5f, 15f));
    }

    public virtual void Enable(float duration){

        this.enabled=true;

        CancelInvoke();
        Invoke(nameof(Disable),duration);

    }

    public virtual void Disable(){

        this.enabled=false;

        CancelInvoke();
    }
   
}
