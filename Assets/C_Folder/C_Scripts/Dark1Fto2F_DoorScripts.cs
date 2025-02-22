using System.Collections;
using UnityEngine;

public class Dark1Fto2F_DoorScripts : MonoBehaviour
{
    public GameObject Dark1Floor;
    public GameObject Dark2Floor;

    void Start()
    {
        Dark2Floor.SetActive(false); // 기본 상태에서 비활성화
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) // 플레이어가 문에 닿았을 때
        {
            Debug.Log("문에 닿음!");
            Dark2Floor.SetActive(true);
            Dark1Floor.SetActive(false);
        }
    }
}
