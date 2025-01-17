using UnityEngine;

public class DetectionCollider : MonoBehaviour
{
    public BossAI bossAI; // Reference to the BossAI script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("Player detected in detection collider!");
            bossAI.chasing = true; // Switch to chasing mode
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("Player exited detection collider.");
            bossAI.chasing = false; // Stop chasing
        }
    }
}
