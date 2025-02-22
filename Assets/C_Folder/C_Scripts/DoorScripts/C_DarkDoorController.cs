using UnityEngine;
using System.Collections;

public class C_DarkDoorController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 문 스프라이트 렌더러
    public Sprite[] closeDoorSprites;     // 문이 닫히는 애니메이션 스프라이트
    public float frameDelay = 0.1f;       // 애니메이션 프레임 간격

    private void Start()
    {
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        // 문 닫기 애니메이션
        foreach (Sprite sprite in closeDoorSprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(frameDelay);
        }
    }
}
