using UnityEngine;

public class ProjPlate : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform spawnPoint;// Reference to the projectile prefab
    // This function will be called when another collider enters the trigger area
    private void OnTriggerEnter(Collider other)
    {

        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);         
        Debug.Log("Player has entered the trigger!");
    }

    // This function will be called when another collider exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player has exited the trigger!");


    }
}
