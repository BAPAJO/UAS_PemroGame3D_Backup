using UnityEngine;

public class CatchTriggerHandler : MonoBehaviour
{
    public BossAI bossAI; // Reference to the BossAI script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("Player caught in catch trigger!");
            bossAI.OnPlayerCaught();
        }
    }
}
