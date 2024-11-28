using UnityEngine;

public class Swapper : MonoBehaviour
{
    public GameObject thrownObject;
    public GameObject player;
    private Vector3 object_coords;
    private Vector3 player_coords;

    private Vector3 player_velocity; 
    private Vector3 object_velocity;

    private bool toSwap = false;

    public AudioSource audioSource;

    public void Swap()
{
    if (thrownObject != null && player != null)
    {
        // Store the current positions
        object_coords = thrownObject.transform.position;
        player_coords = player.transform.position;

        // Get the current velocities
        player_velocity = player.GetComponent<Rigidbody>().linearVelocity;
        object_velocity = thrownObject.GetComponent<Rigidbody>().linearVelocity;

        Debug.Log("Target player position: " + object_coords);

        // Swap positions directly
        player.transform.position = object_coords;
        thrownObject.transform.position = player_coords;

        // Swap velocities
        Vector3 amplifiedVelocity = object_velocity * 1.5f;
        player.GetComponent<Rigidbody>().linearVelocity = amplifiedVelocity;
        thrownObject.GetComponent<Rigidbody>().linearVelocity = player_velocity;

        Debug.Log("Player position: " + player.transform.position);
        Debug.Log("Object position: " + thrownObject.transform.position);

        if (!toSwap) {
            toSwap = true;
            AudioManager.Instance.PlaySound("SwapForward", audioSource);
        }
        else {
            toSwap = false;
            AudioManager.Instance.PlaySound("SwapBack", audioSource);
        }
    }
    else
    {
        Debug.Log("No object to swap with");
    }
}

    public void UpdateThrownObject(GameObject thrownObject){
        this.thrownObject = thrownObject;
        toSwap = false;
    }
}