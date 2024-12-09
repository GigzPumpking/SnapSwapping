using UnityEngine;

public class UntouchableGround : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // The target location to teleport the player to

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //stop velocity and teleport player to target location
            collision.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            collision.gameObject.transform.position = teleportTarget.position;

            //play sound effect
            AudioManager.Instance.PlaySound("Hurt");
        }
    }
}