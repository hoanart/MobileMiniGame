using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TouchManager touchManager;
    
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (touchManager.target != null&&GameManager.instance.State==GameState.ONGOING)
        {
            target = touchManager.target;
            if (agent.pathEndPosition!=target.position)
            {
                agent.SetDestination(target.position);
                //target = null;
            }
        }
       
       
    }
}
