using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossFlashAttack : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public Transform boss;    // ������ Transform
    public float flashRange = 50f; // ������ �����ϴ� �ִ� �Ÿ�
    public string hideTag = "Hide"; // ���� �� �ִ� ������Ʈ�� �±�

    public Image flashImage;  // �Ͼ�� ���� UI Image

    public float flashDuration = 0.5f;  // ���� ȿ�� ���� �ð�
    public float flashDelay = 0.1f;    // ������ ���۵Ǵ� ���� �ð�
    public float fadeDuration = 1f;    // ȭ���� õõ�� �Ͼ�� ���ϴ� �ð�
    public float fadeBackDuration = 1f; // ȭ���� õõ�� ������� ���ƿ��� �ð�

    // ���� ���� �ߵ�
    public void TriggerFlashAttack()
    {
        Debug.Log("���� ������ �ߵ��Ǿ����ϴ�!");

        // �÷��̾ Hide ������Ʈ �ڿ� �ִ��� Ȯ��
        if (IsPlayerBehindHideObject())
        {
            Debug.Log("�÷��̾ ������ ���߽��ϴ�.");
        }
        else
        {
            Debug.Log("�÷��̾ ������ �¾ҽ��ϴ�!");
            // �÷��̾�� ������ �ֱ�
            StartCoroutine(FlashEffect());
        }
    }

    // �������� �÷��̾� �������� ����ĳ��Ʈ�� ��� �Լ�
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

    // ȭ�� �Ͼ�� ó���ϴ� ����Ʈ
    private IEnumerator FlashEffect()
    {
        // �Ͼ�� ȭ���� ���� ����
        flashImage.enabled = true;

        // ȭ���� õõ�� �Ͼ�� ���ϰ� �ϱ� (Alpha �� õõ�� ����)
        float elapsedTime = 0f;
        Color flashColor = flashImage.color;

        while (elapsedTime < fadeDuration)
        {
            flashColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            flashImage.color = flashColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        flashColor.a = 1f; // ���� �� ������ �Ͼ��
        flashImage.color = flashColor;

        // ���� ȿ�� ���� �ð���ŭ ��ٸ���
        yield return new WaitForSeconds(flashDuration);

        // ȭ���� õõ�� ������� ���ƿ��� �ϱ� (Alpha �� õõ�� ����)
        elapsedTime = 0f;
        while (elapsedTime < fadeBackDuration)
        {
            flashColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeBackDuration);
            flashImage.color = flashColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        flashColor.a = 0f; // ���� �� ������ �����ϰ�
        flashImage.color = flashColor;

        // ȭ���� ���� ���·� ����
        yield return new WaitForSeconds(flashDelay);

        flashImage.enabled = false;
    }
}
