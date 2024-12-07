using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class K_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject[] attackPos;

    public int level = 0;

    public float coolTime = 0.75f;
    private float timer1;
    private float timer2;
    private bool doSpecial1 = false;
    private bool doSpecial2 = false;

    //레이저 공격 초안
    public GameObject laserPrefab; // 레이저 프리팹
    public Transform laserSpawnPoint; // 레이저 스폰 위치
    public float laserSpeed = 10f; // 레이저 속도
    public float laserDuration = 0.5f; // 레이저 지속 시간
    public float laserCooldown = 1f; // 레이저 쿨타임
    private float laserTimer = 0f;

    private Animator anim;
    private bool canAttack = true;

  
    private SpriteRenderer spriteRenderer;
    
    private bool isFading = false;


    // 6방향 설정
    private readonly Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0),   // 오른쪽
        new Vector2(-1, 0),  // 왼쪽
        new Vector2(1, 1),   // 대각선 오른쪽 위
        new Vector2(1, -1),   // 대각선 오른쪽 아래
        new Vector2(-1, 1),  // 대각선 왼쪽 위
        new Vector2(-1, -1)  // 대각선 왼쪽 아래
    };
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
                StartCoroutine(FireLaser());
                laserTimer = 0f; // 쿨타임 초기화
            }
        }

        // 특수 공격 1
        if (timer2 > coolTime && level >= 1)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SpecialAttack1());
                timer2 = 0f;
            }
        }

        // 애니메이션 제어
        anim.SetBool("isNAttack", doSpecial1);
        anim.SetBool("isSAttack", doSpecial2);
    }

    private IEnumerator FireLaser()
    {
        canAttack = false;
        anim.SetTrigger("FireLaser"); // 레이저 발사 애니메이션 트리거

        // 마우스 위치를 가져와서 방향을 계산
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized; // 캐릭터와 마우스 사이의 방향

        // 6방향 중 가장 가까운 방향을 선택
        Vector2 closestDirection = GetClosestDirection(direction);

        // 레이저의 시작 위치 설정 (캐릭터의 중앙에서 약간 앞쪽으로 설정)
        Vector2 laserStartPosition = (Vector2)transform.position + closestDirection * 0.5f; // 캐릭터의 중앙에서 앞쪽으로 설정

        // 레이저 인스턴스 생성
        GameObject laser = Instantiate(laserPrefab, laserStartPosition, Quaternion.identity); // 레이저 초기 위치 설정

        // 레이저의 회전 설정
        float angle = Mathf.Atan2(closestDirection.y, closestDirection.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 레이저 이동 처리 (Rigidbody2D를 사용)
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = closestDirection * laserSpeed; // 레이저 속도 적용
        }

        Destroy(laser, laserDuration); // 지속 시간 이후 레이저 삭제

        yield return new WaitForSeconds(laserCooldown); // 쿨타임
        canAttack = true;
    }

    private Vector2 GetClosestDirection(Vector2 direction)
    {
        // 가장 가까운 6방향을 선택하는 로직
        float minAngleDiff = Mathf.Infinity;
        Vector2 closestDirection = Vector2.zero;

        foreach (var dir in directions)
        {
            // 현재 방향과 6방향 사이의 각도를 계산
            float angleDiff = Mathf.Abs(Vector2.Angle(direction, dir));

            if (angleDiff < minAngleDiff)
            {
                minAngleDiff = angleDiff;
                closestDirection = dir;
            }
        }

        return closestDirection;
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

   
}
