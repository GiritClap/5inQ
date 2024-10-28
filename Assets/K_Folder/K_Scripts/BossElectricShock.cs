using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElectricShock : MonoBehaviour
{
    public GameObject BeforeElectricEffectPrefab; // ���� ��� ���� ȿ�� ������
    public GameObject electricShockPrefab; // ���� ����� ȿ�� ������
    public Transform[] shockPoints;        // ���� ����İ� �߻��� ��ġ��
    public float initialInterval = 5f;     // �ʱ� ����� ����
    public float minInterval = 1f;         // �ּ� ����� ���� (���̵� ����)
    public float difficultyIncreaseRate = 0.95f; // ���̵� ������ (���� ���� ����)
    public float shockDuration = 2f;       // ����� ���� �ð�

    private float currentInterval;         // ���� ����� �߻� ����

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
            TriggerElectricShock();

            // ���̵� ����: ����� ������ ���� ����
            currentInterval = Mathf.Max(minInterval, currentInterval * difficultyIncreaseRate);
        }
    }

    // �������� ���� ����� �߻�
    void TriggerElectricShock()
    {
        // ����İ� �߻��� ��ġ ������ ����
        int randomIndex = Random.Range(0, shockPoints.Length);
        Transform shockPoint = shockPoints[randomIndex];

        //���� ���� ��� ȿ�� ����
        GameObject presicionElectric = Instantiate(BeforeElectricEffectPrefab, shockPoint.position,Quaternion.identity);

        Destroy(presicionElectric);

        // ���� ����� ȿ�� ����
        GameObject shockWave = Instantiate(electricShockPrefab, shockPoint.position, Quaternion.identity);

        // ���� �ð� �� ����� ����
        Destroy(shockWave, shockDuration);
    }
}
