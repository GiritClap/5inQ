using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinalRage : MonoBehaviour
{
    public Transform[] weakPoints;  // ������ �����¿� ���� ���� (����: ��, ��, ��, ��)
    public GameObject bossChargeEffect;  // ������ ���� ������ �ð��� ȿ��
    public float timeToCharge = 10f;     // ������ ���� ������ �ð�
    public float timeBetweenHits = 2f;   // �� ���� ���̿� �÷��̾ �� �� �ִ� �ð�

    private bool[] hitWeakPoints;        // ������ ���ݹ޾Ҵ��� ����
    private bool bossCharging = false;   // ������ ���� ������ �ִ��� ����
    private float chargeTimer = 0f;      // �� ������ Ÿ�̸�
    private int currentWeakPointIndex = 0;  // �������� �����ؾ� �ϴ� ������ �ε���

    void Start()
    {
        hitWeakPoints = new bool[weakPoints.Length];
    }

    void Update()
    {
        if (bossCharging)
        {
            chargeTimer += Time.deltaTime;

            // ������ ���� ������ �ð� ���� �÷��̾ ������ ��� �������� ���ϸ� ���� �ߵ�
            if (chargeTimer >= timeToCharge)
            {
                PerformFinalAttack();
                bossCharging = false;
            }
        }
    }

    // ������ ������ ���� ������ ������
    public void StartFinalCharge()
    {
        bossCharging = true;
        chargeTimer = 0f;

        // ��� ���� �ʱ�ȭ
        for (int i = 0; i < hitWeakPoints.Length; i++)
        {
            hitWeakPoints[i] = false;
        }

        currentWeakPointIndex = 0;

        // ���� ������ �ð��� ȿ�� Ȱ��ȭ
        bossChargeEffect.SetActive(true);
    }

    // �÷��̾ ���� ���� �� ȣ��
    public void HitWeakPoint(int weakPointIndex)
    {
        // ���� �����ؾ� �ϴ� ������ �´��� Ȯ��
        if (weakPointIndex == currentWeakPointIndex && !hitWeakPoints[weakPointIndex])
        {
            hitWeakPoints[weakPointIndex] = true;
            currentWeakPointIndex++;

            Debug.Log("���� ���� ����: " + weakPointIndex);

            // �� ���� ��� ���������� ������ ����
            if (currentWeakPointIndex >= weakPoints.Length)
            {
                StopBossCharge();
            }
        }
        else
        {
            Debug.Log("�߸��� ���� ����");
        }
    }

    // ������ �� ������ ����
    private void StopBossCharge()
    {
        bossCharging = false;
        bossChargeEffect.SetActive(false);
        Debug.Log("������ �� �����⸦ �����߽��ϴ�!");
        // �߰����� ���� ���� (ex: ���� ����, ������ �ޱ� ��)
    }

    // ������ ���� ���� �ߵ�
    private void PerformFinalAttack()
    {
        Debug.Log("������ ������ �߾� ������ �ߵ��մϴ�!");
        bossChargeEffect.SetActive(false);
        // ������ ������ ���� �ߵ� (�÷��̾�� ū ����)
    }
}
