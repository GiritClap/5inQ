using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    private Vector2 movementDirection; // ���� ����
    private float moveSpeed = 2f; // ���� �ӵ�
    private float changeDirectionTime = 2f; // ������ �ٲٴ� �ð� ����
    private float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        ChangeMovementDirection(); // �ʱ� ���� ����
        timer = changeDirectionTime; // Ÿ�̸� �ʱ�ȭ
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ֱ������� ���� ����
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeMovementDirection();
            timer = changeDirectionTime;
        }

        // ���� ���� �������� �̵�
        enemyTransform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // �÷��̾���� �Ÿ� üũ
        if (Vector2.Distance(enemyTransform.position, enemy.player.position) <= enemy.distance)
            animator.SetBool("isFollow", true); // ���� ���·� ��ȯ
    }

    private void ChangeMovementDirection()
    {
        // ������ �������� �̵�
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movementDirection = new Vector2(randomX, randomY).normalized; // ���� ����ȭ
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� ���� �� �ʿ��� �۾� �߰�
    }
}
