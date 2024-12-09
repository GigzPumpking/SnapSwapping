using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private LayerMask detectionLayer; // Specify which layers can interact with the plate
    [SerializeField] private Vector3 overlapBoxSize = new Vector3(1, 0.1f, 1); // Size of the detection box
    [SerializeField] private Vector3 boxOffset = Vector3.zero; // Offset of the detection box from the plate's position

    [SerializeField] private Collider[] colliders; // Array to store colliders detected by the overlap box

    [SerializeField] private bool isPressed = false; // Tracks the current state of the pressure plate

    [SerializeField] private Door door; // Reference to the door that the pressure plate controls

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        // Check for objects within the defined area
        colliders = Physics.OverlapBox(transform.position + boxOffset, overlapBoxSize / 2, Quaternion.identity, detectionLayer);

        if (colliders.Length > 0 && !isPressed)
        {
            // If objects are detected and the plate is not pressed, trigger "Press"
            animator.SetTrigger("Press");
            isPressed = true;
            if (door != null)
                door.Open();

        }
        else if (colliders.Length == 0 && isPressed)
        {
            // If no objects are detected and the plate is pressed, trigger "Unpress"
            animator.SetTrigger("Unpress");
            isPressed = false;
            
            if (door != null)
                door.Close();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the overlap box in the scene view for easier debugging
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxOffset, overlapBoxSize);
    }
}
