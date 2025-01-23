using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElectricShock : MonoBehaviour
{
    public GameObject BeforeElectricEffectPrefab; // 전기 충격 예고 효과 프리팹
    public GameObject electricShockPrefab;        // 전기 충격파 효과 프리팹
    public Transform[] shockPoints;               // 전기 충격파가 발생할 위치들
    public float initialInterval = 5f;            // 초기 충격파 간격
    public float minInterval = 1f;                // 최소 충격파 간격 (난이도 증가)
    public float difficultyIncreaseRate = 0.95f;  // 난이도 증가율 (간격 감소 비율)
    public float shockDuration = 2f;              // 충격파 지속 시간
    public float delayBeforeShock = 1f;           // 예고 효과 후 충격파까지의 딜레이

    private float currentInterval;                // 현재 충격파 발생 간격

    void Start()
    {
        currentInterval = initialInterval;
        StartCoroutine(ShockWavePattern());
    }

    // 전기 충격파 패턴
    IEnumerator ShockWavePattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentInterval);
            StartCoroutine(TriggerElectricShock());

            // 난이도 증가: 충격파 간격을 점점 줄임
            currentInterval = Mathf.Max(minInterval, currentInterval * difficultyIncreaseRate);
        }
    }

    // 무작위로 전기 충격파 발생
    IEnumerator TriggerElectricShock()
    {
        // 충격파가 발생할 위치 무작위 선택
        int randomIndex = Random.Range(0, shockPoints.Length);
        Transform shockPoint = shockPoints[randomIndex];

        // 사전 전기 충격 효과 생성
        GameObject preShockEffect = Instantiate(BeforeElectricEffectPrefab, shockPoint.position, Quaternion.identity);

        // 일정 시간 후 예고 효과 제거
        Destroy(preShockEffect, delayBeforeShock);

        // 예고 후 일정 시간 기다렸다가 전기 충격파 생성
        yield return new WaitForSeconds(delayBeforeShock);

        // 전기 충격파 효과 생성
        GameObject shockWave = Instantiate(electricShockPrefab, shockPoint.position, Quaternion.identity);

        // 일정 시간 후 충격파 제거
        Destroy(shockWave, shockDuration);
    }
}
