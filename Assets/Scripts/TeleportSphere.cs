using UnityEngine;
using System.Collections;

public class TeleportSphere : MonoBehaviour
{
    [SerializeField] private Transform destination; // The destination to teleport to

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Clone"))
        {
            collision.transform.position = destination.position;

            // After 3 seconds, play fanfare sound effect
            StartCoroutine(PlayFanfare());
        }
    }

    private IEnumerator PlayFanfare()
    {
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.PlaySound("Fanfare");
    }

}
