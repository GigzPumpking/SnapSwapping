using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] private bool ignorePlayer = false;

    private AudioSource audioSource;

    [SerializeField] private float jumpForce = 10.0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Send the object upwards, if the object is the player, check if ignorePlayer is false in order to send the player upwards
        if (other.gameObject.CompareTag("Player"))
        {
            if (ignorePlayer)
            {
                return;
            }
        }

        Rigidbody rb;

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {
            rb = other.gameObject.transform.parent.GetComponent<Rigidbody>();
        } else {
            rb = other.gameObject.GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioManager.Instance.PlaySound("Jumppad", audioSource);
        }
    }
}
