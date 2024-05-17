using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // ���Ͱ� �̵��� �� �ִ� �ִ� �ݰ�
    public float wanderInterval = 5f; // ���Ͱ� ���ο� �������� �̵��ϴ� ����
    public Transform nest;

    private NavMeshAgent agent;
    private float timer;

    /*
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderInterval)
        {
            Vector3 newPos = RandomNavSphere(nest.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }*/

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        //Debug.Log(navHit.position);
        return navHit.position;
    }
}
