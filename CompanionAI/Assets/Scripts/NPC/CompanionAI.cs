using UnityEngine;
using UnityEngine.AI;

public class CompanionAI : MonoBehaviour
{
    public Transform player;
    public PlayerHealth playerHealth;

    public float followDistance = 2.5f;
    public float healDistance = 3f;

    public int healAmount = 20;
    public float healCooldown = 4f;

    NavMeshAgent agent;
    Animator animator;

    float healTimer;
    bool isHealing;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        healTimer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!isHealing)
        {
            if (distance > followDistance)
                agent.SetDestination(player.position);
            else
                agent.ResetPath();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (!isHealing && healTimer >= healCooldown && distance <= healDistance && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            StartHeal();
        }
    }

    void StartHeal()
    {
        isHealing = true;
        agent.ResetPath();
        animator.SetBool("IsHelping", true); 
        playerHealth.Heal(healAmount);
        healTimer = 0f;
        Invoke(nameof(EndHeal), 1f); 
    }

    void EndHeal()
    {
        isHealing = false;
        animator.SetBool("IsHelping", false); 
    }
}
