using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public GameObject objectToThrow; // Prefab of the object to throw
    public float throwForce = 10.0f;
    public Vector3 spawnOffset = new Vector3(0, 0, 1);
    public GameObject currentThrownObject;
    public Camera playerCamera; 
    public Swapper swapping; 

    public AudioSource audioSource;
    public GameObject throwVisualizerPrefab; // Prefab for the visualizer
    private GameObject throwVisualizerInstance;

    private LineRenderer lineRenderer;
    private float chargeTime;
    public float maxChargeTime = 3.0f;
    public float minThrowForce = 5.0f;
    public float maxThrowForce = 50.0f;
    public int trajectoryResolution = 1000;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = trajectoryResolution;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
            ShowThrowVisualizer();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HideThrowVisualizer();
            Throw();
            chargeTime = 0;
        }
    }

    private void ShowThrowVisualizer()
    {
        lineRenderer.enabled = true;
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.TransformDirection(spawnOffset);
        Vector3 direction = playerCamera.transform.forward;
        float currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeTime / maxChargeTime);
        Vector3 velocity = direction * currentThrowForce;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float time = i / (float)trajectoryResolution;
            Vector3 point = CalculateTrajectoryPoint(spawnPosition, velocity, time);
            lineRenderer.SetPosition(i, point);
        }
    }

    private Vector3 CalculateTrajectoryPoint(Vector3 startPosition, Vector3 startVelocity, float time)
    {
        Vector3 gravity = Physics.gravity;
        return startPosition + startVelocity * time + 0.5f * gravity * time * time;
    }

    private void HideThrowVisualizer()
    {
        lineRenderer.enabled = false;
    }

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
        swapping.UpdateThrownObject(currentThrownObject);
        if (rb != null)
        {
            float currentThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargeTime / maxChargeTime);
            rb.AddForce(playerCamera.transform.forward * currentThrowForce, ForceMode.VelocityChange);
        }

        AudioManager.Instance.PlaySound("Throw", audioSource);
    }
}