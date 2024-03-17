using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;

public class LearningAgent : Agent
{

    [SerializeField] Transform pellets;

    [SerializeField] Ghost[] ghosts;
    [SerializeField] Transform nodes;
    [SerializeField] Passage[] passages;
    [SerializeField] GameManager gameManager;
    public TextMeshProUGUI runView;

    public TextMeshProUGUI finalScoreView;
    public int run=0;

    private Movement movement;


    public override void OnEpisodeBegin()
    {
        run++;
        runView.text="Runs\n#"+run.ToString();
        finalScoreView.text = "TOTAL SCORE:\n"+gameManager.totalScore.ToString(); 
        movement = GetComponent<Movement>();
        
        gameManager.NewGame();

    }
    public override void CollectObservations(VectorSensor sensor)
    {sensor.AddObservation(transform.localPosition);

    foreach(Transform pellet in this.pellets){

        if (pellet.gameObject.activeSelf){
            sensor.AddObservation(pellet.localPosition);
            
            
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

        if (gameManager.HasRemainingPellets()){
            AddReward(-1);
        }
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        
        int move =actions.DiscreteActions[0];

        

        if (move ==0){
            this.movement.SetDirection(Vector2.up);
            if (movement.Occupied(Vector2.up)){
                AddReward(-2);
            }
        }
        if (move ==1){
            this.movement.SetDirection(Vector2.right);
            if (movement.Occupied(Vector2.right)){
                AddReward(-2);
            }
        }
        if (move ==2){
            this.movement.SetDirection(Vector2.down);
            if (movement.Occupied(Vector2.down)){
                AddReward(-2);
            }
        }
        if (move ==3){
            this.movement.SetDirection(Vector2.left);
            if (movement.Occupied(Vector2.left)){
                AddReward(-2);
            }
        }

    }

}
