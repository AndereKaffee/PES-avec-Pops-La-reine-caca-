using UnityEngine;
using UnityEngine.AI;
public class ChickenAi : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask isGround, isPlayer;
    [SerializeField] Rigidbody rb;

    [SerializeField] float idleTime;
    float m_idleTimer;
    bool m_isIdle;

    Vector3 m_walkPoint;
    bool m_walkPointSet;
    [SerializeField] float walkPointRange;

    [SerializeField] float sightRange, fleeSpeed, walkSpeed;
    bool m_playerInSightRange;

    [HideInInspector] public bool isHeld = false;

    private void Update()
    {
        if (isHeld)
        {
            return;
        }

        if (m_isIdle)
        {
            Debug.Log("idle for : " + m_idleTimer);
            Idling();
            return;
        }

        m_playerInSightRange = Vector3.Distance(transform.position, player.position) <= sightRange;
        if (!m_playerInSightRange)
        {
            Patroling();
            return;
        }
        
        Flee();
    }

    private void CoinFlip()
    {
        if (Random.Range(0, 2) == 0)
        {
            m_isIdle = true;
            m_idleTimer = 0;
        }
    }

    private void Idling()
    {
        if (idleTime <= m_idleTimer)
        {
            m_isIdle = false;
            return;
        }
        
        m_idleTimer += Time.deltaTime;
    }

    private void Patroling()
    {
        m_hasBeenStartled = false;
        rb.useGravity = false;
        agent.speed = walkSpeed;
        if (!m_walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(m_walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            m_walkPointSet = false;
            CoinFlip();
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        m_walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(m_walkPoint, -transform.up, 2f, isGround))
        {
            m_walkPointSet = true;
        }
    }

    private void Startle()
    {
        //animation jump
        m_hasBeenStartled = true;
    }
    
    bool m_hasBeenStartled = false;
    private void Flee()
    {
        if (!m_hasBeenStartled)
        {
            Startle();
            return;
        }

        rb.useGravity = false;
        Vector3 direction = transform.position - player.position;

        agent.SetDestination(transform.position + direction);
        agent.speed = fleeSpeed;

        m_walkPointSet = false;
    }
}
