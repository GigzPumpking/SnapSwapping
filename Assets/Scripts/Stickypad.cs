using UnityEngine;

public class Stickypad : MonoBehaviour
{
    [SerializeField] private bool ignorePlayer = false;
    [SerializeField] private bool ignoreThrowable = false;
    private AudioSource audioSource;
    [SerializeField] private bool debug = false;
    private bool audioPlayed = false; // Add this line

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

            // get parent object of the player
            other.gameObject.transform.parent.GetComponent<FPSController>().SetCanMove(false);
        }

        if (other.gameObject.CompareTag("Clone") && ignoreThrowable)
        {
            return;
        }

        Rigidbody rb;
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {
            rb = other.gameObject.transform.parent.GetComponent<Rigidbody>();
        }
        else
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            if (debug)
            {
                Debug.Log("Object is a rigidbody");
            }
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;

            if (!audioPlayed) // Add this condition
            {
                AudioManager.Instance.PlaySound("Stickypad", audioSource);
                audioPlayed = true; // Set the flag to true
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb;
        if (debug)
        {
            Debug.Log("Object exited the stickypad");
        }

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {
            rb = other.gameObject.transform.parent.GetComponent<Rigidbody>();
        }
        else
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        audioPlayed = false; // Reset the flag when the object exits
    }
}