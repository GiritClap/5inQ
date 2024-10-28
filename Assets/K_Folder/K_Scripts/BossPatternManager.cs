using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternManager : MonoBehaviour
{
    public int bossHealth = 100;  // 보스 체력
    public Slider BossHpBar;
    private bool flashUsed = false;       // 섬광 패턴 사용 여부
    private bool electricShockUsed = false;  // 전기 충격파 패턴 사용 여부
    private bool finalRageUsed = false;   // 최후의 발악 패턴 사용 여부

    public BossFlashAttack flashAttackScript;      // 섬광 패턴 스크립트
    public BossElectricShock electricShockScript;  // 전기 충격파 패턴 스크립트
    public BossFinalRage finalRageScript;          // 최후의 발악 패턴 스크립트


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
        // 보스의 체력에 따라 패턴 발동
        ManageBossPatterns();
    }

    // 보스 패턴 관리 함수
    void ManageBossPatterns()
    {
        if (bossHealth <= 100 && bossHealth > 50 && !flashUsed)
        {
            Debug.Log("섬광 패턴 실행");
            flashAttackScript.TriggerFlashAttack();
            flashUsed = true;
        }

        if (bossHealth <= 50 && bossHealth > 10 && !electricShockUsed)
        {
            Debug.Log("전기 충격파 패턴 실행");
            electricShockScript.StartCoroutine("ShockWavePattern");
            electricShockUsed = true;
        }

        if (bossHealth <= 10 && !finalRageUsed)
        {
            Debug.Log("최후의 발악 패턴 실행");
            finalRageScript.StartFinalCharge();
            finalRageUsed = true;
        }
    }


    // 데미지 받는 함수 (예시)
    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log("보스가 데미지를 받음. 현재 체력: " + bossHealth);

        if (bossHealth <= 0)
        {
            Die();
        }
    }

    // 보스 사망 함수
    void Die()
    {
        Debug.Log("보스가 사망했습니다!");
        // 사망 처리 (예: 보스 사라짐, 게임 종료 등)
    }
}
