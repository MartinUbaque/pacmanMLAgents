using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public Movement movement {get; private set;}

    public GhostHome home {get; private set;}
    public GhostChase chase {get; private set;}
    public GhostFrightened frightened {get; private set;}
    public GhostScatter scatter {get; private set;}
    public GhostBehavior initialBehavior; 
    public GameManager gameManager;

    public Transform target;
    public int points = 200;

    private void Awake(){
        this.movement=GetComponent<Movement>();
        this.home=GetComponent<GhostHome>();
        this.chase=GetComponent<GhostChase>();
        this.frightened=GetComponent<GhostFrightened>();
        this.scatter=GetComponent<GhostScatter>();
    }

    private void Start(){
        ResetState();
    }


    public void ResetState(){
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        

        if (home != initialBehavior) {
            if (Random.Range(0f, 1f)<0.5f){
                initialBehavior=this.chase;
            }
            else{
                initialBehavior=this.scatter;
            }
            home.Disable();
        }

        if (this.initialBehavior != null){
            this.initialBehavior.Enable();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer== LayerMask.NameToLayer("Pacman")){
            if (this.frightened.enabled){
                gameManager.GhostEaten(this);
            }
            else {
                gameManager.PacmanEaten ();
            }
        }
        
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.localPosition.z;
        transform.localPosition = position;
    }
}
