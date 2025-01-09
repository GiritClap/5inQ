using UnityEngine;

public class C_Rocket : MonoBehaviour
{
    public Transform target; // 타겟 Transform (플레이어)
    public float rotateSpeed = 5f; // 회전 속도 (낮을수록 더 직선적)
    public float moveSpeed = 10f; // 이동 속도
    public int damage = 5; // 미사일의 데미지

    private Rigidbody2D rb;

    void Start()
    {
        // Player 태그를 가진 오브젝트를 타겟으로 설정
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }

        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // 현재 위치와 타겟 위치 간의 방향 계산
        Vector2 direction = (Vector2)(target.position - transform.position).normalized;

        // 현재 방향과 타겟 방향 간의 각도 차이를 계산
        float angle = Vector2.SignedAngle(transform.right, direction);

        // 각도를 점진적으로 회전 (rotateSpeed에 따라)
        float rotationStep = Mathf.Clamp(angle, -rotateSpeed, rotateSpeed);
        transform.Rotate(0, 0, rotationStep);

        // 직선적으로 이동
        rb.velocity = transform.right * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어에 데미지를 주고 미사일을 파괴
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GetDamage(damage);
            }
            Destroy(gameObject); // 미사일 파괴
        }
    }
}
