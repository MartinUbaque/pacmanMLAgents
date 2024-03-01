using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{

    private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private Movement movement;
    private new Collider2D collider;

    public GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        MovementInput();

         // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.localRotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {

        spriteRenderer.enabled = true;
        collider.enabled = true;
        //deathSequence.enabled = false;
        this.gameObject.SetActive(true);
        movement.ResetState();
        
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }

    public void MovementInput(){

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }

       
    }

    public void Die()
    {
        // Disable rendering of the Pacman object
        spriteRenderer.enabled = false;
        collider.enabled = false;
    }

}   