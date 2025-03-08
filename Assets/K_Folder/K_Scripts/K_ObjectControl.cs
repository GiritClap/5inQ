using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 필요

public class K_ObjectControl : MonoBehaviour
{
    public GameObject itemPrefab;  // 드랍될 아이템 프리팹
    public Transform dropPoint;    // 아이템이 드랍될 위치
    public GameObject interactionUI; // "상호작용 가능" UI (Canvas의 Text)
    //public Text cardKeyUI;  // 카드키 UI 텍스트

    private bool isPlayerNearby = false;
    //private string cardKey = "";  // 카드키 정보 저장

    void Start()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // 처음에는 UI를 숨김
        }

       /* if (cardKeyUI != null)
        {
            cardKeyUI.text = "Card Key: None";  // 시작 시 카드키 정보 초기화
        }*/
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        if (itemPrefab != null && dropPoint != null)
        {
            Instantiate(itemPrefab, dropPoint.position, Quaternion.identity);
            Debug.Log("아이템 드랍!");

            // G키를 눌렀을 때 UI 숨기기
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
    }

    private void AcquireCardKey(string newCardKey)
    {
        // 카드키를 획득했을 때
       /* cardKey = newCardKey;
        Debug.Log("Card Key Acquired: " + cardKey);*/

        // UI에 카드키 정보 업데이트
        /*if (cardKeyUI != null)
        {
            cardKeyUI.text = "Card Key: " + cardKey;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionUI != null)
            {
                interactionUI.SetActive(true); // 플레이어가 가까워지면 UI 표시
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionUI != null)
            {
                interactionUI.SetActive(false); // 플레이어가 멀어지면 UI 숨김
            }
        }
    }
}