using UnityEngine;
using UnityEngine.AI;

public class CompanionAI : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float stopDistance = 2f;
    private PlayerHealth playerHealth;
    public float helpDuration = 2f;
    private float helpTimer;



    public CompanionState currentState = CompanionState.Idle;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.OnPlayerDamaged += OnPlayerDamaged;

    }
    void OnPlayerDamaged()
    {
        currentState = CompanionState.Help;
        helpTimer = helpDuration;
    }



    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case CompanionState.Help:
                agent.SetDestination(player.position);
                helpTimer -= Time.deltaTime;

                if (helpTimer <= 0)
                    currentState = CompanionState.Follow;
                break;


            case CompanionState.Idle:
                if (distance < followDistance)
                    currentState = CompanionState.Follow;
                break;

            case CompanionState.Follow:
                if (!agent.isOnNavMesh) return;

                if (distance > stopDistance)
                    agent.SetDestination(player.position);
                else
                    agent.ResetPath();
                break;
        }
    }
}
