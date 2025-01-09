using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer; // Line Renderer ������Ʈ
    public float laserDistance = 10f; // ������ �ִ� �Ÿ�
    public LayerMask hitLayers; // �浹�� ������ ���̾�

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
        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư Ŭ��
        {
            ShootLaser();
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� �� ��
        {
            StopLaser();
        }
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
