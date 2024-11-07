using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public GameObject objectToThrow; // Prefab of the object to throw
    public float throwForce = 10.0f;
    public Vector3 spawnOffset = new Vector3(0, 0, 1);
    public GameObject currentThrownObject;
    public Camera playerCamera; 

    public void Throw()
    {
        // Destroy the existing object if it exists
        if (currentThrownObject != null)
        {
            Destroy(currentThrownObject);
        }

        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.TransformDirection(spawnOffset);
        currentThrownObject = Instantiate(objectToThrow, spawnPosition, playerCamera.transform.rotation);
        Rigidbody rb = currentThrownObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}