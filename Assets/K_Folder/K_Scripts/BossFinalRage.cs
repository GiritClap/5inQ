using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinalRage : MonoBehaviour
{
    public Transform[] weakPoints;  // 보스의 상하좌우 약점 지점 (순서: 상, 하, 좌, 우)
    public GameObject bossChargeEffect;  // 보스가 힘을 모으는 시각적 효과
    public float timeToCharge = 10f;     // 보스가 힘을 모으는 시간
    public float timeBetweenHits = 2f;   // 각 공격 사이에 플레이어가 쓸 수 있는 시간

    private bool[] hitWeakPoints;        // 약점이 공격받았는지 여부
    private bool bossCharging = false;   // 보스가 힘을 모으고 있는지 여부
    private float chargeTimer = 0f;      // 힘 모으기 타이머
    private int currentWeakPointIndex = 0;  // 다음으로 공격해야 하는 약점의 인덱스

    void Start()
    {
        hitWeakPoints = new bool[weakPoints.Length];
    }

    void Update()
    {
        if (bossCharging)
        {
            chargeTimer += Time.deltaTime;

            // 보스가 힘을 모으는 시간 내에 플레이어가 약점을 모두 공격하지 못하면 공격 발동
            if (chargeTimer >= timeToCharge)
            {
                PerformFinalAttack();
                bossCharging = false;
            }
        }
    }

    // 보스가 최후의 힘을 모으기 시작함
    public void StartFinalCharge()
    {
        bossCharging = true;
        chargeTimer = 0f;

        // 모든 약점 초기화
        for (int i = 0; i < hitWeakPoints.Length; i++)
        {
            hitWeakPoints[i] = false;
        }

        currentWeakPointIndex = 0;

        // 힘을 모으는 시각적 효과 활성화
        bossChargeEffect.SetActive(true);
    }

    // 플레이어가 약점 공격 시 호출
    public void HitWeakPoint(int weakPointIndex)
    {
        // 현재 공격해야 하는 약점이 맞는지 확인
        if (weakPointIndex == currentWeakPointIndex && !hitWeakPoints[weakPointIndex])
        {
            hitWeakPoints[weakPointIndex] = true;
            currentWeakPointIndex++;

            Debug.Log("약점 공격 성공: " + weakPointIndex);

            // 네 방향 모두 공격했으면 보스를 저지
            if (currentWeakPointIndex >= weakPoints.Length)
            {
                StopBossCharge();
            }
        }
        else
        {
            Debug.Log("잘못된 약점 공격");
        }
    }

    // 보스의 힘 모으기 저지
    private void StopBossCharge()
    {
        bossCharging = false;
        bossChargeEffect.SetActive(false);
        Debug.Log("보스의 힘 모으기를 저지했습니다!");
        // 추가적인 성공 연출 (ex: 보스 기절, 데미지 받기 등)
    }

    // 보스의 최종 공격 발동
    private void PerformFinalAttack()
    {
        Debug.Log("보스가 최후의 발악 공격을 발동합니다!");
        bossChargeEffect.SetActive(false);
        // 보스의 강력한 공격 발동 (플레이어에게 큰 피해)
    }
}
