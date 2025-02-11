using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class K_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;

    public int level = 0;

    public float coolTime = 0.75f;
    private float timer1;
    private float timer2;
    private bool doSpecial1 = false;
    private bool doSpecial2 = false;

    // 레이저 공격
    public GameObject laserPrefab; // 레이저 프리팹
    public Transform laserSpawnPoint; // 레이저 스폰 위치
    public float laserSpeed = 10f; // 레이저 속도
    public float laserDuration = 0.5f; // 레이저 지속 시간
    public float laserCooldown = 1f; // 레이저 쿨타임
    private float laserTimer = 0f;

    private Animator anim;
    private bool canAttack = true;

    private SpriteRenderer spriteRenderer;

    // C_Skill 이미지 오브젝트
    public GameObject C_Skill; // Canvas의 C_Skill 이미지

    private bool isCSkillActive = false; // 이미지가 이미 활성화되었는지 확인하는 플래그

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // C_Skill 초기 상태 비활성화
        if (C_Skill != null)
        {
            C_Skill.SetActive(false);
        }
    }

    void Update()
    {
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;

        laserTimer += Time.deltaTime;

        if (laserTimer > laserCooldown && canAttack)
        {
            if (Input.GetMouseButtonDown(0)) // 좌클릭으로 레이저 발사
            {
                StartCoroutine(Attack());
                laserTimer = 0f; // 쿨타임 초기화
            }
        }

        // 특수 공격 1 (레벨 3 이상에서만 가능)
        if (timer2 > coolTime && level >= 3)
        {
            if (Input.GetMouseButtonDown(1)) // 우클릭으로 특수 공격 발동
            {
                StartCoroutine(SpecialAttack1());
                timer2 = 0f;
            }
        }

        // 레벨 3이 되었을 때 C_Skill 이미지 활성화
        if (level >= 3 && !isCSkillActive)
        {
            ActivateCSkillImage();
        }

        // 애니메이션 제어
        anim.SetBool("isNAttack", doSpecial1);
        anim.SetBool("isSAttack", doSpecial2);
    }

    private void ActivateCSkillImage()
    {
        if (C_Skill != null)
        {
            C_Skill.SetActive(true); // 이미지 활성화
            Debug.Log("C_Skill 이미지가 활성화되었습니다!");
            isCSkillActive = true; // 플래그 설정
        }
    }

    private IEnumerator Attack()
    {
        doSpecial1 = true;
        anim.SetBool("isNAttack", doSpecial1);

        // 공격 로직 처리
        yield return new WaitForSeconds(0.2f);

        doSpecial1 = false;
    }

    private IEnumerator SpecialAttack1()
    {
        doSpecial2 = true;
        anim.SetBool("isSAttack", doSpecial2);

        // 특수 공격 1 로직 처리
        yield return new WaitForSeconds(0.5f);

        doSpecial2 = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Level"))
        {
            level++; // 레벨 증가
            Debug.Log("레벨업! 현재 레벨: " + level);
            Destroy(collision.gameObject); // 충돌한 오브젝트 삭제
        }
    }

}
