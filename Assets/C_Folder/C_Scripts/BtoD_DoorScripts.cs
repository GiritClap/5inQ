using System.Collections;
using UnityEngine;

public class BtoD_DoorScripts : MonoBehaviour
{
    public GameObject LightFloor;
    public GameObject DarkFloor;
    public GameObject Door; // 문 오브젝트를 추가로 지정

    void Start()
    {
        DarkFloor.SetActive(false); // 기본 상태에서 비활성화
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) // 플레이어가 문에 닿았을 때
        {
            DarkFloor.SetActive(true);
            LightFloor.SetActive(false);
            StartCoroutine(HideDoorAfterDelay(1f)); // 2초 후 문을 숨기는 코루틴 실행
        }
    }

    IEnumerator HideDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 2초 대기
        Door.SetActive(false); // 문을 비활성화하여 숨김
    }
}
