using UnityEngine;

public class Stickypad : MonoBehaviour
{
    [SerializeField] private bool ignorePlayer = false;

    [SerializeField] private bool ignoreThrowable = false;

    private AudioSource audioSource;

    // Causing the object to stick to the pad

    [SerializeField] private bool debug = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb;

        if (debug) {
            Debug.Log("Object entered the stickypad");
        }

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

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {
            rb = other.gameObject.transform.parent.GetComponent<Rigidbody>();
        } else {
            rb = other.gameObject.GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            if (debug) {
                Debug.Log("Object is a rigidbody");
            }
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;

            AudioManager.Instance.PlaySound("Stickypad", audioSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb;
        if (debug) {
            Debug.Log("Object exited the stickypad");
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (ignorePlayer)
            {
                return;
            }

            other.gameObject.transform.parent.GetComponent<FPSController>().SetCanMove(true);
        }

        if (other.gameObject.CompareTag("Clone") && ignoreThrowable)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {
            rb = other.gameObject.transform.parent.GetComponent<Rigidbody>();
        } else {
            rb = other.gameObject.GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
}
