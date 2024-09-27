using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform target = null;



    //distancia para ele eliminar o alvo
    [SerializeField] private float killDistance = 3f;

    //quanto o npc tem que girar para atingir o alvo para eliminalo
    [SerializeField] private float lookThreshold = .8f;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            MoveToTarget();
        }

        if (Vector3.Dot(transform.forward.normalized, target.position.normalized) <= lookThreshold 
            && Vector3.Distance(transform.position, target.position) <= killDistance)
        
            KillTarget();

        return; 
    }
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        agent.isStopped = false;
    }

    private void KillTarget()
    {
        Destroy(target.gameObject);
        target = null;
    }
}
