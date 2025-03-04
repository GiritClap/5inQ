using System.Collections;
using UnityEngine;

public class Dark1Fto2F_DoorScripts : MonoBehaviour
{
    public GameObject Dark1Floor;
    public GameObject Dark2Floor;
    private Collider2D doorCollider;
    private bool isOnFirstFloor = true; // 현재 층 상태 (true: 1층, false: 2층)

    void Start()
    {
        Dark2Floor.SetActive(false);
        doorCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Debug.Log("문에 닿음!");
            ToggleFloors(); // 층 전환
            StartCoroutine(DisableColliderTemporarily()); // 콜라이더 2초 비활성화
        }
    }

    void ToggleFloors()
    {
        if (isOnFirstFloor)
        {
            Dark1Floor.SetActive(false);
            Dark2Floor.SetActive(true);
        }
        else
        {
            Dark1Floor.SetActive(true);
            Dark2Floor.SetActive(false);
        }
        isOnFirstFloor = !isOnFirstFloor; // 상태 변경
    }

    IEnumerator DisableColliderTemporarily()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
            yield return new WaitForSeconds(2f);
            doorCollider.enabled = true;
        }
    }
}
