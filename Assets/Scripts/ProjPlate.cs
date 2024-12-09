using UnityEngine;
using System.Collections;

public class ProjPlate : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform spawnPoint;// Reference to the projectile prefab
    // This function will be called when another collider enters the trigger area

    private PressurePlate pressurePlate;

    private bool isSpawning = false;

    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private float cooldown = 0f;

    private void Awake()
    {
        pressurePlate = transform.GetComponent<PressurePlate>();
    }

    void Update()
    {
        if (pressurePlate == null)
        {
            Debug.LogError("Pressure Plate not found on " + gameObject.name);
            return;
        }

        if (isSpawning) {
            if (cooldown <= 0)
            {
                SpawnProjectile();
                cooldown = spawnRate;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    private void SpawnProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);  

        projectile.SetActive(true);       
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
        }
    }

}
