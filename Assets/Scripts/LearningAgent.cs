using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using TMPro;

public class LearningAgent : Agent
{

    [SerializeField] Transform pellets;

    [SerializeField] Ghost[] ghosts;
    [SerializeField] Transform nodes;
    [SerializeField] Passage[] passages;
    [SerializeField] GameManager gameManager;
    //public TextMeshProUGUI runView;

    //public TextMeshProUGUI finalScoreView;
    //public int run=0;

    private Movement movement;


    public override void OnEpisodeBegin()
    {
        /* run++;
        runView.text="Runs\n#"+run.ToString();
        finalScoreView.text = "LAST SCORE:\n"+gameManager.score.ToString(); */
        movement = GetComponent<Movement>();
        
        gameManager.NewGame();

    }
    public override void CollectObservations(VectorSensor sensor)
    {sensor.AddObservation(transform.localPosition);

    foreach(Transform pellet in this.pellets){

        if (pellet.gameObject.activeSelf){
            sensor.AddObservation(pellet.localPosition);
            AddReward(-0.01f);
        }

    }
    foreach(Transform node in this.nodes){

        if (node.gameObject.activeSelf){
            sensor.AddObservation(node.localPosition);
        }
    }
    for (int i = 0; i<ghosts.Length; i++){
            sensor.AddObservation(this.ghosts[i].transform.localPosition);
            sensor.AddObservation(this.ghosts[i].frightened.enabled);
            sensor.AddObservation(this.ghosts[i].home.enabled);

        }

        for (int i = 0; i<passages.Length; i++){
            sensor.AddObservation(this.passages[i].transform.localPosition);
        }

        sensor.AddObservation(gameManager.lives);
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        
        int move =actions.DiscreteActions[0];

        

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
