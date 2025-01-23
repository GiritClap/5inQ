using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElectricShock : MonoBehaviour
{
    public GameObject BeforeElectricEffectPrefab; // ���� ��� ���� ȿ�� ������
    public GameObject electricShockPrefab;        // ���� ����� ȿ�� ������
    public Transform[] shockPoints;               // ���� ����İ� �߻��� ��ġ��
    public float initialInterval = 5f;            // �ʱ� ����� ����
    public float minInterval = 1f;                // �ּ� ����� ���� (���̵� ����)
    public float difficultyIncreaseRate = 0.95f;  // ���̵� ������ (���� ���� ����)
    public float shockDuration = 2f;              // ����� ���� �ð�
    public float delayBeforeShock = 1f;           // ���� ȿ�� �� ����ı����� ������

    private float currentInterval;                // ���� ����� �߻� ����

    void Start()
    {
        currentInterval = initialInterval;
        StartCoroutine(ShockWavePattern());
    }

    // ���� ����� ����
    IEnumerator ShockWavePattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentInterval);
            StartCoroutine(TriggerElectricShock());

            // ���̵� ����: ����� ������ ���� ����
            currentInterval = Mathf.Max(minInterval, currentInterval * difficultyIncreaseRate);
        }
    }

    // �������� ���� ����� �߻�
    IEnumerator TriggerElectricShock()
    {
        // ����İ� �߻��� ��ġ ������ ����
        int randomIndex = Random.Range(0, shockPoints.Length);
        Transform shockPoint = shockPoints[randomIndex];

        // ���� ���� ��� ȿ�� ����
        GameObject preShockEffect = Instantiate(BeforeElectricEffectPrefab, shockPoint.position, Quaternion.identity);

        // ���� �ð� �� ���� ȿ�� ����
        Destroy(preShockEffect, delayBeforeShock);

        // ���� �� ���� �ð� ��ٷȴٰ� ���� ����� ����
        yield return new WaitForSeconds(delayBeforeShock);

        // ���� ����� ȿ�� ����
        GameObject shockWave = Instantiate(electricShockPrefab, shockPoint.position, Quaternion.identity);

        // ���� �ð� �� ����� ����
        Destroy(shockWave, shockDuration);
    }
}
