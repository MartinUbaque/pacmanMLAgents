using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat(GameManager gameManager)
    {
        gameManager.PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            // Retrieve the Pacman script from the collided GameObject
            Pacman pacman = other.gameObject.GetComponent<Pacman>();
            
            if (pacman != null && pacman.gameManager != null)
            {
                // Call the Eat method with the GameManager reference from Pacman
                Eat(pacman.gameManager);
            }
            else
            {
                Debug.LogError("Pacman or GameManager reference not found.");
            }
        }
    }
}

