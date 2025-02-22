using UnityEngine;

public class C_BrightDoorController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // �� ��������Ʈ ������
    public Sprite[] openDoorSprites;      // ���� ������ �ִϸ��̼� ��������Ʈ
    public Sprite[] closeDoorSprites;     // ���� ������ �ִϸ��̼� ��������Ʈ
    public float activationDistance = 3.0f; // �÷��̾���� �Ÿ� ����
    public float frameDelay = 0.1f;         // �ִϸ��̼� ������ ����

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
            Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        if (playerTransform == null || isAnimating)
            return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= activationDistance && !isDoorOpen)
        {
            StartCoroutine(PlayAnimation(openDoorSprites, true));
        }
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
