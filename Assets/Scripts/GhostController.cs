using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    private float one = -660f;
    private float two = -600f;
    private float three = 190f;
    private float four = 120f;

    NavMeshAgent navMeshAgent;
    private Animator anim;
    private float time = 0f;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 6f)
        {
            if(navMeshAgent.remainingDistance < 0.2f)
            {
                SetNewDestination();
                time = 0f;
            }
            
        }
    }

    void SetNewDestination(){
        navMeshAgent.SetDestination(new Vector3(Random.Range(one, two), 0f, Random.Range(three, four)));
    }
}
