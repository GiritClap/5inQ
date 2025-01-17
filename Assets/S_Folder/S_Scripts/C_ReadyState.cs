using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ReadyState : StateMachineBehaviour
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

        // 공격 범위 내 플레이어가 있는지 확인
        if (distance <= enemy.attackRange)
        {
            // 공격 쿨타임이 완료된 경우에만 로켓 생성 및 공격 실행
            if (enemy.atkDelay <= 0)
            {
                // 로켓 생성
                //if (enemy.type == EnemyType.RobotC)
                //{
                    //enemy.LaunchCRocket();
                //}

                // 공격 애니메이션 실행
                animator.SetTrigger("Attack");

                // 쿨타임 초기화
                enemy.atkDelay = enemy.atkCooltime;
            }
        }
        else
        {
            // 5미터 이상일 때 추적 상태로 전환
            animator.SetBool("isFollow", true);
        }

        // 적의 방향 업데이트
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태를 종료할 때 필요한 작업
    }
}
