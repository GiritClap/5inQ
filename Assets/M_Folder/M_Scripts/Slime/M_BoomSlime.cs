using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoomSlime : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float explosionRange = 2f; // ���� ����
    public float detectionRange = 15f; // �÷��̾� Ž�� ����
    //public int damage = 50; // �÷��̾�� ���� ������

    private Animator animator; // �ִϸ�����
    private bool isExploding = false; // ���� ������ ����

    void Start()
    {
        animator = GetComponent<Animator>();
        FindPlayer();
    }

    void Update()
    {
        if (isExploding) return; // ���� �߿��� �̵� �ߴ�

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �÷��̾ Ž�� ���� �ȿ� �ִٸ�
        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(); // �÷��̾�� �ٰ�����
        }

        // �÷��̾ ���� ���� �ȿ� ���Դٸ�
        if (distanceToPlayer <= explosionRange)
        {
            StartSelfDestruct(); // ���� ����
        }
    }

    // �÷��̾ ���� �̵�
    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

        // �̵� �������� ȸ��
        //transform.LookAt(player);
    }

    // ���� ���� ����
    void StartSelfDestruct()
    {
        isExploding = true; // ���� ���� ����
        animator.SetTrigger("IsDie"); // ���� �ִϸ��̼� Ʈ���� ����

        // �ִϸ��̼� ���� �� ���� ó�� (1�� ��� ����)
        //Invoke(nameof(DealExplosionDamage), 1f); // ������ ó��
    }

   /* // ���� ���� ���� �÷��̾�� ������ ������
    void DealExplosionDamage()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= explosionRange)
        {
            // �÷��̾� ������ ó�� (��: PlayerHealth ������Ʈ ����)
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }*/

    // ���ʹ� ����
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // ����׿� ���� ���� ǥ��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning($"�±� '{"Player"}'�� ���� �÷��̾ ã�� �� �����ϴ�!");
        }
    }
}
