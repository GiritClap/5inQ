using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform player;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        Setup(player);
    }

    public void Setup(Transform target)
    {
        if (target == null) return; // target이 없으면 실행 안 함

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent를 찾을 수 없습니다!");
            return;
        }

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (navMeshAgent != null && player != null) // navMeshAgent와 player가 존재할 때만 실행
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
