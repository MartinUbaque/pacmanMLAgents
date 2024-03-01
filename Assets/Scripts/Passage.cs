using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = connection.localPosition;
        position.z = other.transform.localPosition.z;
        other.transform.localPosition = position;
    }

} 
