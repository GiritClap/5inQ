using UnityEngine;

public class BossFlashAttack : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public Transform boss;    // 보스의 Transform
    public float flashDamageRange = 90f; // 플레이어가 몇 도 이상으로 등지고 있어야 피해를 받지 않음

    // 섬광 패턴 발동
    public void TriggerFlashAttack()
    {
        Debug.Log("섬광 패턴이 발동되었습니다!");

        // 플레이어가 보스에게 등을 돌리고 있는지 확인
        if (IsPlayerLookingAway())
        {
            Debug.Log("플레이어가 섬광을 피했습니다.");
        }
        else
        {
            Debug.Log("플레이어가 섬광에 맞았습니다!");
            // 플레이어에게 데미지 주기
        }
    }

    // 플레이어가 보스에게 등을 돌리고 있는지 확인하는 함수
    private bool IsPlayerLookingAway()
    {
        // 플레이어가 보고 있는 방향
        Vector3 playerForward = player.forward;
        // 보스의 위치에서 플레이어의 위치까지의 방향
        Vector3 directionToBoss = (boss.position - player.position).normalized;

        // 플레이어가 보스와 반대 방향을 보고 있는지 확인
        float angle = Vector3.Angle(playerForward, directionToBoss);

        Debug.Log("플레이어와 보스 간의 각도: " + angle);

        // 플레이어가 flashDamageRange 이상으로 보스에게 등을 돌리고 있으면 피할 수 있음
        return angle > flashDamageRange;
    }
}
