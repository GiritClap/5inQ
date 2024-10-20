using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    private Vector2 movementDirection; // 현재 방향
    private float moveSpeed = 2f; // 순찰 속도
    private float changeDirectionTime = 2f; // 방향을 바꾸는 시간 간격
    private float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        ChangeMovementDirection(); // 초기 방향 설정
        timer = changeDirectionTime; // 타이머 초기화
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 주기적으로 방향 변경
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeMovementDirection();
            timer = changeDirectionTime;
        }

        // 적을 현재 방향으로 이동
        enemyTransform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // 플레이어와의 거리 체크
        if (Vector2.Distance(enemyTransform.position, enemy.player.position) <= enemy.distance)
            animator.SetBool("isFollow", true); // 추적 상태로 전환
    }

    private void ChangeMovementDirection()
    {
        // 랜덤한 방향으로 이동
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movementDirection = new Vector2(randomX, randomY).normalized; // 방향 정규화
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태 종료 시 필요한 작업 추가
    }
}
