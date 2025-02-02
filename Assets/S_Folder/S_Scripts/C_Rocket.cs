using UnityEngine;

public class C_Rocket : MonoBehaviour
{
    public Transform target; // Ÿ�� Transform (�÷��̾�)
    public float rotateSpeed = 5f; // ȸ�� �ӵ� (�������� �� ������)
    public float moveSpeed = 15f; // �̵� �ӵ�
    public int damage = 5; // �̻����� ������
    public float lifetime = 5f; // ������ ����� �ð� (��)

    private Rigidbody2D rb;

    void Start()
    {
        // Player �±׸� ���� ������Ʈ�� Ÿ������ ����
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }

        // Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();

        // ������ �ð� �Ŀ� ���� �ı�
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // ���� ��ġ�� Ÿ�� ��ġ ���� ���� ���
        Vector2 direction = (Vector2)(target.position - transform.position).normalized;

        // ���� ����� Ÿ�� ���� ���� ���� ���̸� ���
        float angle = Vector2.SignedAngle(transform.right, direction);

        // ������ ���������� ȸ�� (rotateSpeed�� ����)
        float rotationStep = Mathf.Clamp(angle, -rotateSpeed, rotateSpeed);
        transform.Rotate(0, 0, rotationStep);

        // ���������� �̵�
        rb.velocity = transform.right * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾ �������� �ְ� �̻����� �ı�
            M_PlayerHealth playerHealth = collision.GetComponent<M_PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GetDamage(damage);
            }
            Destroy(gameObject); // �̻��� �ı�
        }
    }
}
