using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class K_GameManager : MonoBehaviour
{
    public static K_GameManager Instance;

    public Text cardKeyUI;  // 카드키 UI 텍스트
    public GameObject cardKeyPanel; // UI 패널 (활성화/비활성화 가능)
    public Text fakeMessageUI; // 가짜 카드키 메시지 UI
    public List<string> cardKeyNames = new List<string> { "Silver Key", "Gold Key", "Bronze Key", "Diamond Key", "Master Key" };

    private List<string> acquiredCardKeys = new List<string>();
    private string realCardKey = "";  // 진짜 카드키

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AssignRandomRealCardKey(); // 게임 시작 시 랜덤하게 1개의 카드키를 진짜로 설정

        if (cardKeyPanel != null)
        {
            cardKeyPanel.SetActive(false);
        }
        if (fakeMessageUI != null)
        {
            fakeMessageUI.gameObject.SetActive(false); // 처음엔 숨김
        }
    }

    // 랜덤으로 1개의 카드키를 진짜로 설정
    private void AssignRandomRealCardKey()
    {
        int randomIndex = Random.Range(0, cardKeyNames.Count);
        realCardKey = cardKeyNames[randomIndex];
        Debug.Log("진짜 카드키는: " + realCardKey);
    }

    // 카드키를 획득하는 함수
    public void AcquireCardKey(string newCardKey)
    {
        if (!acquiredCardKeys.Contains(newCardKey))
        {
            acquiredCardKeys.Add(newCardKey);
        }

        UpdateCardKeyUI();

        if (newCardKey == realCardKey)
        {
            Debug.Log("진짜 카드키를 획득했습니다!");
            ShowFakeMessage("진짜 카드키를 획득했습니다!", Color.green);
        }
        else
        {
            Debug.Log("가짜 카드키를 획득했습니다!");
            ShowFakeMessage("이건 가짜였다!", Color.red);
        }
    }

    // UI 업데이트
    private void UpdateCardKeyUI()
    {
        if (cardKeyUI != null)
        {
            cardKeyUI.text = "획득한 카드키: \n";
            foreach (string key in acquiredCardKeys)
            {
                cardKeyUI.text += "- " + key + "\n";
            }

            if (cardKeyPanel != null)
            {
                cardKeyPanel.SetActive(true);
            }
        }
    }

    // 가짜 카드키 메시지 출력
    private void ShowFakeMessage(string message, Color color)
    {
        if (fakeMessageUI != null)
        {
            fakeMessageUI.text = message;
            fakeMessageUI.color = color;
            fakeMessageUI.gameObject.SetActive(true);

            // 2초 후 메시지 숨기기
            StartCoroutine(HideFakeMessage());
        }
    }

    private IEnumerator HideFakeMessage()
    {
        yield return new WaitForSeconds(2f);
        if (fakeMessageUI != null)
        {
            fakeMessageUI.gameObject.SetActive(false);
        }
    }
}
