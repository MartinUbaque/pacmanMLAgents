using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class LearningAgent : Agent
{

    [SerializeField] Transform pellets;

    [SerializeField] Ghost[] ghosts;
    [SerializeField] Transform nodes;
    [SerializeField] Passage[] passages;

    private Movement movement;


    public override void OnEpisodeBegin()
    {
        movement = GetComponent<Movement>();
    }
    public override void CollectObservations(VectorSensor sensor)
    {sensor.AddObservation(transform.position);

    foreach(Transform pellet in this.pellets){

        if (pellet.gameObject.activeSelf){
            sensor.AddObservation(pellet.position);
        }

        for (int i = 0; i<ghosts.Length; i++){
            sensor.AddObservation(this.ghosts[i].transform.position);
            sensor.AddObservation(this.ghosts[i].frightened.enabled);

        }

        for (int i = 0; i<passages.Length; i++){
            sensor.AddObservation(this.passages[i].transform.position);
        }

        sensor.AddObservation(FindAnyObjectByType<GameManager>().lives);



    }
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        
        int move =actions.DiscreteActions[0];

        Debug.Log(move);

        if (move ==0){
            this.movement.SetDirection(Vector2.up);
        }
        if (move ==1){
            this.movement.SetDirection(Vector2.right);
        }
        if (move ==2){
            this.movement.SetDirection(Vector2.down);
        }
        if (move ==3){
            this.movement.SetDirection(Vector2.left);
        }

    }

}
