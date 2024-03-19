using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    // Patrol
    Vector3 destPoint;
    bool walkpointSet;
    [SerializeField] float range;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;

    // Chase
    public Transform player;
    [SerializeField] float sightRange;
    public float timeToPatrolWaypoints = 10f;
    bool playerInSight;
    float playerLostSightTime = 5f;
    public float timeWithoutSightPlayer = 1000;

    // Attack
    public float attackDistance = 2f;
    public GameObject playerGameObject;
    public bool killAnim = false;

    // Audio
    public GameObject chaseMusic;
    public GameObject normalMusic;

    // Waypoints
    public Transform[] waypoints;
    public int currentWaypointIndex;
    
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("El objetivo no ha sido asignado en el inspector.");
        }

        Patrol();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        if (!killAnim)
        {
            if (!playerInSight)
            {
                timeWithoutSightPlayer += Time.deltaTime;
                print(timeWithoutSightPlayer);
                if (timeWithoutSightPlayer >= playerLostSightTime)
                {
                    // Si el tiempo sin ver al jugador es menor que el tiempo necesario para patrullar waypoints, entonces busca.
                    if (timeWithoutSightPlayer <= timeToPatrolWaypoints)
                    {
                        Patrol();
                    }
                    else // Si no, patrulla los waypoints.
                    {
                        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
                        {
                            SetDestinationToNextWaypoint();
                        }
                    }
                }
            }

            if (playerInSight)
            {
                timeWithoutSightPlayer = 0f;
            }

            if (timeWithoutSightPlayer < playerLostSightTime)
            {
                Chase();
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackDistance)
        {
            killAnim = true;
            // Realiza la acción que desees cuando el jugador esté a la distancia especificada.
            // Puedes agregar aquí el código para esa acción.
            playerGameObject.GetComponent<SC_FPSController>().enabled = false;
            playerGameObject.GetComponent<PickNoteSystem>().enabled = false;
            playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
            playerGameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void Chase()
    {
        gameObject.transform.GetChild(7).gameObject.SetActive(true);
        gameObject.transform.GetChild(8).gameObject.SetActive(false);
        normalMusic.SetActive(false);
        chaseMusic.SetActive(true);
        print(player.transform.position);
        navMeshAgent.SetDestination(player.transform.position);
    }

    void Patrol()
    {
        gameObject.transform.GetChild(7).gameObject.SetActive(false);
        gameObject.transform.GetChild(8).gameObject.SetActive(true);
        normalMusic.SetActive(true);
        chaseMusic.SetActive(false);
        if (!walkpointSet) SearchForDest();
        if (walkpointSet) navMeshAgent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkpointSet = false; 
    }
    void SetDestinationToNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No se han asignado waypoints al NPC.");
            return;
        }

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void SearchForDest()
    {
        float z = UnityEngine.Random.Range(-range, range);
        float x = UnityEngine.Random.Range(-range, range);

        Vector3 randomPosition = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            destPoint = hit.position;
            walkpointSet = true;
        }
        else
        {
            // Si no se encuentra una posición válida en el NavMesh, puedes tomar alguna acción alternativa, como volver a buscar.
            Debug.Log("No se pudo encontrar una posición válida en el NavMesh.");
            // También puedes agregar un temporizador o contador para evitar bucles infinitos.
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Cambia el color del raycast
        Vector3 raycastStart = transform.position;
        Vector3 raycastDirection = Vector3.down;
        Gizmos.DrawRay(raycastStart, raycastDirection * 3);
    }
}