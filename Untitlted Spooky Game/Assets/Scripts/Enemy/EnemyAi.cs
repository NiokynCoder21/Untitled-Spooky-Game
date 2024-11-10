using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAi : MonoBehaviour
{
  
    public Transform player;
    public float detectionRange; // Range to detect the player
    public List<Transform> patrolWaypoints; // List of patrol waypoints
    private NavMeshAgent agent; //the nav mesh agent
    private int currentWaypointIndex = 0; 
    public float interestDuration; //float for intrest duration 
    private float timeSinceLastSighting = 0f; //float for time since last sighting
    public float rotationSpeed; //how fast the enemy rotates 

    private Vector3 lastknownPlayerPosition;

    public Transform eyePosition; // Assign this to the point where you want the enemy to "see" from
    public float fieldOfViewAngle = 60f;
    public bool reachedWaypoint = false;

    public enum EnemyState
    {
        Patrol,
        Chase,
        Investigate,
        Stand,
    }

    public EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //gets the navmesh component
        currentState = EnemyState.Patrol; //sets the state at the beginning of the game to EnemyState.Patrol
        SetNextWaypoint(); //calls the newway point function
        AudioSource audio = GetComponent<AudioSource>(); //get audio component 
        GameObject.FindGameObjectWithTag("Player"); //get gameobject with tag player
        agent.updateRotation = false;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.Chase:
                ChaseUpdate();
                break;
           case EnemyState.Investigate:
                InvestigateUpdate();
                break;
            case EnemyState.Stand:
                StandUpdate();
                break;
        }

       
    }

    void PatrolUpdate()
    {
        if (CanSeePlayer()) //checks if the enemy can see player 
        {
            currentState = EnemyState.Chase; //if can see player chase where player went 
        }

        else
        {
            if (agent.remainingDistance < 0.5f) //if the player is close to the waypoint set the next waypoint 
            {
                SetNextWaypoint();
            }  
           
        }
    }

    void StandUpdate()
    {
        if (CanSeePlayer()) //if can see the player is yes
        {
            currentState = EnemyState.Chase; //if can see player chase the player
        }

        else
        {
            currentState = EnemyState.Patrol;
        }
    }


    void InvestigateUpdate()
    {
        if (CanSeePlayer()) ////if can see the player is yes
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            lastknownPlayerPosition = player.position; //last known position is the players current position
            timeSinceLastSighting = 0f;
            currentState = EnemyState.Chase; //if the player seen then the enemy enters chase state
            agent.destination = player.position; //the enemey moves to the players position
        }

        else
        {
            timeSinceLastSighting += Time.deltaTime; //calculate time since last seen independant of frame rate
            currentState = EnemyState.Stand;
        }

        if (timeSinceLastSighting < interestDuration) //if time since last seen is less than interest duration 
        {
            if (currentState == EnemyState.Chase) //and if current state is enemy chase 
            {
                MoveTowardsLastKnownPosition();
                currentState = EnemyState.Patrol; //change current state to stand state , so the player stays the the position they moved to
            }

        }
    }


    void MoveTowardsLastKnownPosition()
    {
        // Set the agent's destination to the last known player position
        agent.destination = lastknownPlayerPosition;

        // Check if the agent is moving to avoid unnecessary rotation
        if (agent.velocity.sqrMagnitude > 0.01f)
        {
            // Calculate the direction to the last known position
            Vector3 directionToMove = agent.velocity.normalized;

            // Calculate the target rotation to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(directionToMove);

            // Rotate smoothly, allowing a full 180-degree turn if needed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void ChaseUpdate()
    {  
        if (CanSeePlayer()) //if can see the player is yes
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            currentState = EnemyState.Chase; //change current state to chase
            agent.destination = player.position; //set enemy desitination to player position
        }

        else
        {
            currentState = EnemyState.Investigate; //else if cannot see player, set current state to investigate  
        }
    }



    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - eyePosition.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        int layerMask = 1 << LayerMask.NameToLayer("Walls"); // Only wall layer
        layerMask = ~layerMask; // Exclude wall layer for raycast

        // Check if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Check if player is within field of view angle
            float angleToPlayer = Vector3.Angle(eyePosition.forward, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle / 2) // Divide by 2 to get a symmetrical cone
            {
                // Perform raycast from eye position in the direction of the player
                RaycastHit hit;
                if (Physics.Raycast(eyePosition.position, directionToPlayer.normalized, out hit, detectionRange, layerMask) && hit.collider.CompareTag("Player"))
                {
                    return true; // Enemy can see the player
                }
            }
        }

        return false; // Enemy cannot see the player
    }

    void SetNextWaypoint()
    {
        if (patrolWaypoints.Count == 0)
        {
            Debug.LogError("No patrol waypoints assigned!");
            return;
        }

        // If the enemy has just arrived at a waypoint
        if (!reachedWaypoint)
        {
            // Set a flag indicating the enemy has now reached a waypoint
            reachedWaypoint = true;

            // Perform a 180-degree turn
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);
        }
        else
        {
            // Move to the next waypoint
            agent.destination = patrolWaypoints[currentWaypointIndex].position;

            // Reset the reachedWaypoint flag
            reachedWaypoint = false;

            // Increment index for the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Count;
        }

    }

}
