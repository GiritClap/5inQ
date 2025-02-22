using UnityEngine;
using System.Collections;

public class C_DarkDoorController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // �� ��������Ʈ ������
    public Sprite[] closeDoorSprites;     // ���� ������ �ִϸ��̼� ��������Ʈ
    public float frameDelay = 0.1f;       // �ִϸ��̼� ������ ����

    private void Start()
    {
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        // �� �ݱ� �ִϸ��̼�
        foreach (Sprite sprite in closeDoorSprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(frameDelay);
        }
    }
}
