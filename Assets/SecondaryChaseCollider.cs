using UnityEngine;

public class SecondaryChaseCollider : MonoBehaviour
{
    public BossAI bossAI; // Reference to the BossAI script

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player") && bossAI.chasing)
        {
            Debug.Log("Player exited secondary chase collider.");
            bossAI.chasing = false; // Stop chasing
            bossAI.walking = true;  // Resume patrolling
        }
    }
}
