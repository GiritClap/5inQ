using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;  // �÷��̾ �Ѿư��� �����ϴ� �Ÿ�
    public Rigidbody2D target;

    bool isLive = true; // �ʱ�ȭ

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator; // �ִϸ����� ������Ʈ

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ �ʱ�ȭ
    }

    private void FixedUpdate()
    {
        if (!isLive || target == null)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float distanceToTarget = Vector2.Distance(target.position, rigid.position);

        if (distanceToTarget < chaseDistance)
        {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;

            animator.SetBool("isWalking", true); // �ȴ� �ִϸ��̼� ����

            if (target.position.x < rigid.position.x)
            {
                spriter.flipX = true; // �÷��̾ ���ʿ� ������ ������
            }
            else
            {
                spriter.flipX = false; // �÷��̾ �����ʿ� ������ ������ �ʱ�
            }

        }
        else
        {
            animator.SetBool("isWalking", false); // �ȴ� �ִϸ��̼� ����
        }
    }

    // ���� Ȱ��ȭ/��Ȱ��ȭ �� �� ����� �޼���
    public void SetActive(bool active)
    {
        isLive = active;
    }
}
