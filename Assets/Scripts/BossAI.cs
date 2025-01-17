using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, idleTime, chaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public string deathScene;

    private bool playerInSecondaryRadius = false;
    private bool hasLeftSecondaryRadius = false;

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];

        // Set initial values for idleTime and chaseTime (you can set them in the Inspector).
        idleTime = 5f;  // Set default idle time
        chaseTime = 10f;  // Set default chase time
    }

    void Update()
    {
        if (chasing)
        {
            ChasePlayer();
        }
        else if (walking)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        dest = currentDest.position;
        ai.destination = dest;
        ai.speed = walkSpeed;
        aiAnim.ResetTrigger("sprint");
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

        if (ai.remainingDistance <= ai.stoppingDistance)
        {
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("walk");
            aiAnim.SetTrigger("idle");
            ai.speed = 0;
            StopCoroutine("stayIdle");
            StartCoroutine("stayIdle");
            walking = false;
        }
    }

    void ChasePlayer()
    {
        dest = player.position;
        ai.destination = dest;
        ai.speed = chaseSpeed;
        aiAnim.ResetTrigger("walk");
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("sprint");
    }

    public void OnPlayerCaught()
    {
        Debug.Log("Player caught! Initiating jumpscare.");
        player.gameObject.SetActive(false);
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("jumpscare");
        StartCoroutine(deathRoutine());
        chasing = false;
    }

    IEnumerator stayIdle()
    {
        // Use set idleTime instead of random value.
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator chaseRoutine()
    {
        // Use set chaseTime instead of random value.
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the primary detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);  // This can be customized

        // Visualize the secondary detection radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 15f);  // This can be customized
    }
}
