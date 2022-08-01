using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    // x, z
    private float one = -660f;
    private float two = -600f;
    private float three = 200f;
    private float four = 140f;
    [SerializeField] private GameObject player;
    private Transform trans;

    NavMeshAgent navMeshAgent;
    private Animator anim;
    private float time = 0f;
    
    private float speedWalk;
    private float speedRun;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetNewDestination();

        navMeshAgent.isStopped = false;
        speedWalk = navMeshAgent.speed;
        speedRun = speedWalk + 500;
        trans = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        trans = player.transform;
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(trans.position.x, trans.position.z)) <= 50f)
        {
            Chasing();
        }
        else
        {
            time += Time.deltaTime;
            if(time > 4f)
            {
                if(navMeshAgent.remainingDistance < 0.2f)
                {
                    Patroling();
                    time = 0f;
                }
                
            }
        }
        
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(trans.position.x, trans.position.z)) <= 10f)
        {
            Stop();
        }
    }
    void Patroling()
    {
        Move(speedWalk);
        SetNewDestination();
    }
    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Chasing()
    {
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(trans.position.x, trans.position.z)) >= 10f)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(new Vector3(trans.position.x, 0f, trans.position.z));
        }
        else
        {
            Stop();
        }
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    void SetNewDestination(){
        navMeshAgent.SetDestination(new Vector3(Random.Range(one, two), 0f, Random.Range(three, four)));
    }
}
