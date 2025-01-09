using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LineRenderer lineRenderer; // Line Renderer ������Ʈ
    public float laserDistance = 10f; // ������ �ִ� �Ÿ�
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    public LayerMask hitLayers; // �浹�� ������ ���̾�

    private Vector3 movement; // �÷��̾� �̵� ����

    void Start()
    {
        // Line Renderer �ʱ�ȭ
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.enabled = false; // �⺻������ ��Ȱ��ȭ
    }

    void Update()
    {
        // �÷��̾� �̵�
        MovePlayer();

        // ������ �߻�
        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư Ŭ��
        {
            ShootLaser();
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� �� ��
        {
            StopLaser();
        }
    }

    void MovePlayer()
    {
        // WASD �Ǵ� ȭ��ǥ Ű�� �÷��̾� �̵�
        float horizontal = Input.GetAxis("Horizontal"); // A/D, ����/������ ȭ��ǥ
        float vertical = Input.GetAxis("Vertical"); // W/S, ��/�Ʒ� ȭ��ǥ

        movement = new Vector3(horizontal, vertical, 0); // 2D�̹Ƿ� Z���� 0���� ����
        transform.position += movement * moveSpeed * Time.deltaTime; // �̵�
    }

    void ShootLaser()
    {
        // ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 2D���� Z���� 0���� ���� (ī�޶� Z=0 �������� �۾�)

        Vector3 startPoint = transform.position; // ������ ���� ����
        Vector3 direction = (mousePosition - startPoint).normalized; // ���콺 �������� ���� ���

        // Raycast�� �浹 ����
        RaycastHit hit;
        Vector3 endPoint;
        if (Physics.Raycast(startPoint, direction, out hit, laserDistance, hitLayers))
        {
            endPoint = hit.point; // �浹�� ����
        }
        else
        {
            endPoint = startPoint + direction * laserDistance; // �ִ� �Ÿ�����
        }

        // Line Renderer ������Ʈ
        lineRenderer.SetPosition(0, startPoint); // ���� ����
        lineRenderer.SetPosition(1, endPoint); // �� ����
        lineRenderer.enabled = true; // ������ ǥ��
    }

    void StopLaser()
    {
        lineRenderer.enabled = false; // ������ ��Ȱ��ȭ
    }
}
