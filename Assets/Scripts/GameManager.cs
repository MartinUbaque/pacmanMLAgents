using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    private int ghostMultiplier = 1;

    public LearningAgent agent;

    public Pacman  pacman;

    public Transform pellets;

    public int score {get; private set;}

     public int lives {get; private set;}

     private void Start(){

        NewGame();

     }

     private void Update() {

        if (this.lives<=0 && Input.anyKeyDown){
            NewGame();
        }
        
     }

     private void NewGame(){

        SetScore(0);
        SetLives(3);
        NewRound();
     }

    

    private void SetScore(int score){

        this.score = score;

     }

     private void SetLives(int lives){

        this.lives = lives;

     }

     private void NewRound()
    {
        foreach(Transform pellet in this.pellets){

            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

        

    private void ResetState(){

        for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].ResetState();

        }
        this.pacman.ResetState();
        ResetGhostMultiplier();
    }

    private void GameOver(){
        //UI LATER
        for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);

        }
        this.pacman.gameObject.SetActive(false);
        agent.EndEpisode();
    }

    public void GhostEaten(Ghost ghost){
        this.ghostMultiplier+=1;
        SetScore(this.score+ghost.points*this.ghostMultiplier); 
        agent.AddReward(ghost.points*this.ghostMultiplier);
        
    }

    public void PacmanEaten(){

        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives-1);
        
        if (lives>0){
            Invoke(nameof(ResetState),3f);
            agent.AddReward(-100f);
        }
        else{
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet){
        pellet.gameObject.SetActive(false);
        SetScore(this.score+pellet.points); 
        agent.AddReward(pellet.points);

        if(!HasRemainingPellets()){
            pacman.gameObject.SetActive(false);
            agent.AddReward(pellet.points*100);
            
            Invoke(nameof(NewRound),3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet){


        agent.AddReward(pellet.points);
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier),pellet.duration);
        

        for (int i = 0; i<ghosts.Length; i++){
            this.ghosts[i].frightened.Enable(pellet.duration);

        }
    }

    private bool HasRemainingPellets()
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

