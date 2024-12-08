using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Slime : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float detectionRange = 10f; // �÷��̾� Ž�� �Ÿ�
    public float moveSpeed = 3f; // ���ʹ� �̵� �ӵ�
    public float attackInterval = 3f; // ���� ����
    public GameObject boomSlimePrefab;


    private Animator animator; // �ִϸ�����
    private float attackTimer; // ���� Ÿ�̸�

    void Start()
    {
        animator = GetComponent<Animator>();
        attackTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        FindPlayer();
    }

    void Update()
    {
        HandleMovement(); // �̵� ó��
        HandleAttack();   // ���� ó��
    }

    // �÷��̾���� �Ÿ��� ����ϰ� ���� �̵� ó��
    void HandleMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveAwayFromPlayer(); // �÷��̾ ���� ����
        }
    }

    // �÷��̾� �ݴ� �������� �̵�
    void MoveAwayFromPlayer()
    {
        Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;

        // ��ġ ������Ʈ
        transform.position += directionAwayFromPlayer * moveSpeed * Time.deltaTime;

        // ȸ�� ����: �÷��̾�� �ݴ� �������� ������ ȸ��
        //transform.rotation = Quaternion.LookRotation(-directionAwayFromPlayer);
    }

    // ���� Ÿ�̸Ӹ� �����ϰ� �ִϸ��̼� Ʈ���� ����
    void HandleAttack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            TriggerAttack();  // ���� ����
            attackTimer = 0f; // Ÿ�̸� ����
        }
    }

    // Attack �ִϸ��̼� Ʈ���� ����
    void TriggerAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("IsAttack");
        }
        GameObject boomSlime = Instantiate(boomSlimePrefab, transform.position, transform.rotation);
    }

    // ����׿� Ž�� ���� ǥ��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
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
