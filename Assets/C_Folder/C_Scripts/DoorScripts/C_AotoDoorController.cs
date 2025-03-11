using UnityEngine;

public class C_AotoDoorController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 문 스프라이트 렌더러
    public Sprite[] openDoorSprites;      // 문이 열리는 애니메이션 스프라이트
    public Sprite[] closeDoorSprites;     // 문이 닫히는 애니메이션 스프라이트
    public float activationDistance = 3.0f; // 플레이어와의 거리 기준
    public float frameDelay = 0.1f;         // 애니메이션 프레임 간격

    private Transform playerTransform;
    private bool isDoorOpen = false;
    private bool isAnimating = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        if (playerTransform == null || isAnimating)
            return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // 플레이어가 문보다 아래에 있고, 문과의 거리가 기준 이하일 때 문을 연다.
        if (distance <= activationDistance && !isDoorOpen && playerTransform.position.y < transform.position.y)
        {
            StartCoroutine(PlayAnimation(openDoorSprites, true));
        }
        // 플레이어가 문을 벗어났거나 문이 이미 열려있을 때 문을 닫는다.
        else if (distance > activationDistance && isDoorOpen)
        {
            StartCoroutine(PlayAnimation(closeDoorSprites, false));
        }
    }

    private System.Collections.IEnumerator PlayAnimation(Sprite[] sprites, bool opening)
    {
        isAnimating = true;
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(frameDelay);
        }
        isDoorOpen = opening;
        isAnimating = false;
    }
}
