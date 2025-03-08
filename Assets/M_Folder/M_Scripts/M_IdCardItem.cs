using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IdCardItem : MonoBehaviour
{
    public int id;
    M_IdCardActivator activator; // ObjectActivator 스크립트 연결

    public float amplitude = 0.5f; // 떠오르는 높이 (진폭)
    public float speed = 2f; // 움직이는 속도
    public float floatSpeed = 2f; // 위아래 속도
    public float fadeSpeed = 0.5f; // 점점 사라지는 속도

    private Vector3 startPos;

    private bool isPlayerNearby = false;

    private SpriteRenderer spriteRenderer;
    private float alpha = 1f; // 초기 투명도

    private void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Player").GetComponent<M_IdCardActivator>();
        startPos = transform.position; // 초기 위치 저장
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 가져오기

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") ) // 플레이어가 아이템을 줍는 경우
        {
            isPlayerNearby = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 아이템을 줍는 경우
        {
            isPlayerNearby = false;

        }
    }

    void Update()
    {
        // Sin 함수를 이용해 Y 위치를 변경 (부드러운 위아래 움직임)
        FloatEffect();

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.K))
        {
            if(id == 10)
            {
                StartCoroutine(FadeOutEffect()); // 사라지는 효과 실행
                return;
            }
            activator.ActivateNextObject(id); // 다음 오브젝트 활성화
            Destroy(gameObject); // 아이템 삭제
        }

    }
    void FloatEffect()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time * floatSpeed) * amplitude, startPos.z);
    }

    IEnumerator FadeOutEffect()
    {
        while (alpha > 0f) // alpha가 0보다 클 동안 계속 실행
        {
            alpha -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(0f, 0f, 0f, alpha);
            yield return null; // 다음 프레임까지 대기
        }

        Destroy(gameObject); // 완전히 투명해지면 삭제
    }
}
