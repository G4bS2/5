using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform stalker = null;
    [SerializeField] private float travelCost = 5f;

    [SerializeField] private float distanciaDeTeleporte = 5f;
    [SerializeField] private float cooldown = 0f;
    [SerializeField] private float cooldownSet = 10f;
    private float randomX;
    private float randomZ;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 direction = (stalker.position - transform.position).normalized;

        // Calculando a direção do stalker em relação ao objeto atual
        Gizmos.DrawLine(transform.position, transform.position + direction);
        // Direção normalizada

        // Calculando a magnitude da direção
        float magnitude = direction.magnitude; // Magnitude da direção

        // Imprimindo a magnitude no console
        print(magnitude); // Imprimindo a magnitude
    }

    void Start()
    {
        if (agent == null)
        {
            if (!TryGetComponent(out agent))
            {
                Debug.LogWarning(name + "precisa colocar um navmesh agent");
            }
        }
    }

    
    void Update()
    {
        cooldown = cooldown - 1f * Time.deltaTime;

        if (stalker == null)
            return;

        float distanceToStalker = Vector3.Distance(transform.position, stalker.position);

        if (distanceToStalker <= distanciaDeTeleporte && cooldown <= 0)
        {
            TeleportNPC();
            cooldown = cooldownSet;
        }

        Vector3 directionNormalized = (stalker.position - transform.position).normalized;

        directionNormalized = Quaternion.AngleAxis(Random.Range(100, 100), Vector3.up) * directionNormalized;

        MoveToPos(transform.position - (directionNormalized * travelCost));

    }

    private void MoveToPos(Vector3 position)
    {
        agent.SetDestination(position);
        agent.isStopped = false;
    }

    private void TeleportNPC()
    {
        randomX = Random.Range(-10f, 10f);
        randomZ = Random.Range(-10f, 10f);
        Vector3 teleportPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        transform.position = teleportPosition;
    }

}
