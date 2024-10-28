using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternManager : MonoBehaviour
{
    public int bossHealth = 100;  // ���� ü��
    public Slider BossHpBar;
    private bool flashUsed = false;       // ���� ���� ��� ����
    private bool electricShockUsed = false;  // ���� ����� ���� ��� ����
    private bool finalRageUsed = false;   // ������ �߾� ���� ��� ����

    public BossFlashAttack flashAttackScript;      // ���� ���� ��ũ��Ʈ
    public BossElectricShock electricShockScript;  // ���� ����� ���� ��ũ��Ʈ
    public BossFinalRage finalRageScript;          // ������ �߾� ���� ��ũ��Ʈ


    private void Awake()
    {
        BossSetMaxHp(bossHealth);
    }

    public void BossSetMaxHp(int health)
    {
        BossHpBar.maxValue = health;
        BossHpBar.value = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GetDamage(10);
        }
    }

    public void GetDamage(int damage)
    {
        int getDamagedHp = bossHealth - damage;
        if (getDamagedHp <= 0)
        {
            GetDie();
        }
        else
        {
            bossHealth = getDamagedHp;
            BossHpBar.value = bossHealth;
        }
    }

    public void GetDie()
    {
        // end Game
    }

    void Update()
    {
        // ������ ü�¿� ���� ���� �ߵ�
        ManageBossPatterns();
    }

    // ���� ���� ���� �Լ�
    void ManageBossPatterns()
    {
        if (bossHealth <= 100 && bossHealth > 50 && !flashUsed)
        {
            Debug.Log("���� ���� ����");
            flashAttackScript.TriggerFlashAttack();
            flashUsed = true;
        }

        if (bossHealth <= 50 && bossHealth > 10 && !electricShockUsed)
        {
            Debug.Log("���� ����� ���� ����");
            electricShockScript.StartCoroutine("ShockWavePattern");
            electricShockUsed = true;
        }

        if (bossHealth <= 10 && !finalRageUsed)
        {
            Debug.Log("������ �߾� ���� ����");
            finalRageScript.StartFinalCharge();
            finalRageUsed = true;
        }
    }


    // ������ �޴� �Լ� (����)
    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log("������ �������� ����. ���� ü��: " + bossHealth);

        if (bossHealth <= 0)
        {
            Die();
        }
    }

    // ���� ��� �Լ�
    void Die()
    {
        Debug.Log("������ ����߽��ϴ�!");
        // ��� ó�� (��: ���� �����, ���� ���� ��)
    }
}
