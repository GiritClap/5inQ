using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer; // Line Renderer 컴포넌트
    public float laserDistance = 10f; // 레이저 최대 거리
    public LayerMask hitLayers; // 충돌을 감지할 레이어

    void Start()
    {
        // Line Renderer 초기화
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.enabled = false; // 기본적으로 비활성화
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 클릭
        {
            ShootLaser();
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 뗄 때
        {
            StopLaser();
        }
    }

    void ShootLaser()
    {
        // 마우스 좌표를 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 2D에서 Z축은 0으로 설정 (카메라가 Z=0 기준으로 작업)

        Vector3 startPoint = transform.position; // 레이저 시작 지점
        Vector3 direction = (mousePosition - startPoint).normalized; // 마우스 방향으로 벡터 계산

        // Raycast로 충돌 감지
        RaycastHit hit;
        Vector3 endPoint;
        if (Physics.Raycast(startPoint, direction, out hit, laserDistance, hitLayers))
        {
            endPoint = hit.point; // 충돌한 지점
        }
        else
        {
            endPoint = startPoint + direction * laserDistance; // 최대 거리까지
        }

        // Line Renderer 업데이트
        lineRenderer.SetPosition(0, startPoint); // 시작 지점
        lineRenderer.SetPosition(1, endPoint); // 끝 지점
        lineRenderer.enabled = true; // 레이저 표시
    }

    void StopLaser()
    {
        lineRenderer.enabled = false; // 레이저 비활성화
    }
}
