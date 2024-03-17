using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    private int ghostMultiplier = 1;
 [SerializeField]
    private LearningAgent agent;

    [SerializeField]
    private Pacman  pacman;
 [SerializeField]
    private Transform pellets;

    public bool withGhosts;

    public int score {get; private set;}

    public int totalScore {get; private set;}

     public int lives {get; private  set;}

     public TextMeshProUGUI scoreView;
    
    public TextMeshProUGUI livesView; 

     private void Start(){
        totalScore=0;

        NewGame();

     }


     public void NewGame(){

        
        SetLives(3);
        SetScore(0);
        
        NewRound();
     }

    

    private void SetScore(int score){

        this.score = score;
        scoreView.text = score.ToString();


     }

     private void SetLives(int lives){

        this.lives = lives;
        livesView.text=lives.ToString();

     }

     private void NewRound()
    {
        foreach(Transform pellet in this.pellets){

            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

        

    private void ResetState(){

        if (!withGhosts){
            for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);

        }
        }
        else{
        for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].ResetState();

        }
        }
    
        pacman.ResetState();
        ResetGhostMultiplier();
    }

    private void GameOver(){
        
        //UI LATER
        for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);

        }
        this.pacman.gameObject.SetActive(false);
        
    }

    public void GhostEaten(Ghost ghost){
        this.ghostMultiplier+=1;
        SetScore(this.score+ghost.points*this.ghostMultiplier); 
        agent.AddReward(10);
        
    }

    public void PacmanEaten(){

        pacman.Die();
        SetLives(this.lives-1);

        
        
        if (lives>0){
            agent.AddReward(-10);
            ResetState();
            
        }
        else{

            totalScore=totalScore+score;
            
            agent.AddReward(-20);

            agent.EndEpisode();
            Debug.Log("Episode end");
        }
    }

    public void PelletEaten(Pellet pellet){
        pellet.gameObject.SetActive(false);
        SetScore(this.score+pellet.points); 
        agent.AddReward(1);

        if(!HasRemainingPellets()){
            NewRound();
        }
    }

    public void PowerPelletEaten(PowerPellet pellet){


        
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier),pellet.duration);
        

        for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].frightened.Enable(pellet.duration);

        }

    }

    public bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }


    private void ResetGhostMultiplier(){
        this.ghostMultiplier=1;
    }

    
    
}

