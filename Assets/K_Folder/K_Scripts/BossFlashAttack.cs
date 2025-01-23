using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossFlashAttack : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public Transform boss;    // 보스의 Transform
    public float flashRange = 50f; // 섬광이 도달하는 최대 거리
    public string hideTag = "Hide"; // 숨을 수 있는 오브젝트의 태그

    public Image flashImage;  // 하얗게 변할 UI Image

    public float flashDuration = 0.5f;  // 섬광 효과 지속 시간
    public float flashDelay = 0.1f;    // 섬광이 시작되는 지연 시간
    public float fadeDuration = 1f;    // 화면이 천천히 하얗게 변하는 시간
    public float fadeBackDuration = 1f; // 화면이 천천히 원래대로 돌아오는 시간

    // 섬광 패턴 발동
    public void TriggerFlashAttack()
    {
        Debug.Log("섬광 패턴이 발동되었습니다!");

        // 플레이어가 Hide 오브젝트 뒤에 있는지 확인
        if (IsPlayerBehindHideObject())
        {
            Debug.Log("플레이어가 섬광을 피했습니다.");
        }
        else
        {
            Debug.Log("플레이어가 섬광에 맞았습니다!");
            // 플레이어에게 데미지 주기
            StartCoroutine(FlashEffect());
        }
    }

    // 보스에서 플레이어 방향으로 레이캐스트를 쏘는 함수
    private bool IsPlayerBehindHideObject()
    {
        Vector2 directionToPlayer = (player.position - boss.position).normalized;
        float distanceToPlayer = Vector2.Distance(boss.position, player.position);

        RaycastHit2D hit;

        Debug.DrawRay(boss.position, directionToPlayer * distanceToPlayer, Color.red, 2f);

        hit = Physics2D.Raycast(boss.position, directionToPlayer, distanceToPlayer);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(hideTag))
            {
                return true;
            }
        }

        return false;
    }

    // 화면 하얗게 처리하는 이펙트
    private IEnumerator FlashEffect()
    {
        // 하얗게 화면을 덮기 시작
        flashImage.enabled = true;

        // 화면이 천천히 하얗게 변하게 하기 (Alpha 값 천천히 증가)
        float elapsedTime = 0f;
        Color flashColor = flashImage.color;

        while (elapsedTime < fadeDuration)
        {
            flashColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            flashImage.color = flashColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        flashColor.a = 1f; // 끝날 때 완전히 하얗게
        flashImage.color = flashColor;

        // 섬광 효과 지속 시간만큼 기다리기
        yield return new WaitForSeconds(flashDuration);

        // 화면이 천천히 원래대로 돌아오게 하기 (Alpha 값 천천히 감소)
        elapsedTime = 0f;
        while (elapsedTime < fadeBackDuration)
        {
            flashColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeBackDuration);
            flashImage.color = flashColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        flashColor.a = 0f; // 끝날 때 완전히 투명하게
        flashImage.color = flashColor;

        // 화면을 원래 상태로 복귀
        yield return new WaitForSeconds(flashDelay);

        flashImage.enabled = false;
    }
}
