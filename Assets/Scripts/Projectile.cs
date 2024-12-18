using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;  // Speed of the projectile
    public float lifeTime = 5f; // Time before the projectile disappears
    public Transform player;  // Reference to the player's transform

    [SerializeField] private Vector3 teleportLocation = new Vector3(7.43f, 2.19f, -88.63f);
    void OnEnable()
    {
        if (player == null)
        {
            // If no player is assigned in the inspector, find the player by tag
            player = GameManager.Instance.GetPlayer().transform;
        }

        // Destroy the projectile after a set lifetime to avoid it floating around forever
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Optionally, rotate the projectile towards the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    // This function can be called when the projectile collides with something (like the player)
    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile hits the player, apply damage or any other effect
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle damage or effects here
            Debug.Log("Projectile hit the player!");
            player.position = teleportLocation;
            // Destroy the projectile after hitting the player
            Destroy(gameObject);

            AudioManager.Instance.PlaySound("Hurt");
        }
        else
        {
            // Destroy the projectile on any other collision (optional)
            Destroy(gameObject);
        }
    }
}
