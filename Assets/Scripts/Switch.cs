using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject objectToDelete; // The object to delete when collided with

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (objectToDelete != null)
            {
                Destroy(objectToDelete);
            }
        }
    }
}