using UnityEngine;
using UnityEngine.AI;
public class ChickenAi : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask isGround, isPlayer;

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

        Debug.Log("caca1");

        m_playerInSightRange = Vector3.Distance(transform.position, player.position) <= sightRange;

        if (!m_playerInSightRange)
        {
            Patroling();
            return;
        }
        Flee();
    }

    private void Patroling()
    {
        if (!m_walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            Debug.Log("DestinationSet");
            agent.SetDestination(m_walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

        Debug.Log("patroling");

        if (distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("walkPointReset");
            m_walkPointSet = false;
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

    private void Flee()
    {
        Debug.Log("fleeing");

        Vector3 direction = transform.position - player.position;

        agent.SetDestination(direction);
    }
}
