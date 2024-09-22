using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
       
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 적과 플레이어 간의 거리 계산
        float distance = Vector2.Distance(enemyTransform.position, enemy.player.position);

        // 공격 조건 확인
        if (distance <= enemy.attackRange) // 5미터 이내에서 공격
        {
            if (enemy.atkDelay <= 0)
            {
                animator.SetTrigger("Attack"); // 공격 트리거 설정 여기가 공격이니까 
                // 여기다가  대충
                enemy.AttackStart();
            }
            else
            {
                enemy.AttackStop();

            }
        }
        else
        {
            animator.SetBool("isFollow", true); // 5미터 이상일 때 추적 상태로 전환
            enemy.AttackStop();

        }

        // 적의 방향 업데이트
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태 종료 시 필요한 작업 추가
    }
}
