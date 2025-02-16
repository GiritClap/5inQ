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
        if (target == null) return; // target�� ������ ���� �� ��

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent�� ã�� �� �����ϴ�!");
            return;
        }

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (navMeshAgent != null && player != null) // navMeshAgent�� player�� ������ ���� ����
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
