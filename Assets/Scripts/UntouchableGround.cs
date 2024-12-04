using UnityEngine;

public class UntouchableGround : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // The target location to teleport the player to

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = teleportTarget.position;
        }
    }
}