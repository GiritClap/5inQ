using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class K_GameManager : MonoBehaviour
{
    public static K_GameManager Instance;

    public Text cardKeyUI;  // ī��Ű UI �ؽ�Ʈ
    public GameObject cardKeyPanel; // UI �г� (Ȱ��ȭ/��Ȱ��ȭ ����)
    public Text fakeMessageUI; // ��¥ ī��Ű �޽��� UI
    public List<string> cardKeyNames = new List<string> { "Silver Key", "Gold Key", "Bronze Key", "Diamond Key", "Master Key" };

    private List<string> acquiredCardKeys = new List<string>();
    private string realCardKey = "";  // ��¥ ī��Ű

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

        AssignRandomRealCardKey(); // ���� ���� �� �����ϰ� 1���� ī��Ű�� ��¥�� ����

        if (cardKeyPanel != null)
        {
            cardKeyPanel.SetActive(false);
        }
        if (fakeMessageUI != null)
        {
            fakeMessageUI.gameObject.SetActive(false); // ó���� ����
        }
    }

    // �������� 1���� ī��Ű�� ��¥�� ����
    private void AssignRandomRealCardKey()
    {
        int randomIndex = Random.Range(0, cardKeyNames.Count);
        realCardKey = cardKeyNames[randomIndex];
        Debug.Log("��¥ ī��Ű��: " + realCardKey);
    }

    // ī��Ű�� ȹ���ϴ� �Լ�
    public void AcquireCardKey(string newCardKey)
    {
        if (!acquiredCardKeys.Contains(newCardKey))
        {
            acquiredCardKeys.Add(newCardKey);
        }

        UpdateCardKeyUI();

        if (newCardKey == realCardKey)
        {
            Debug.Log("��¥ ī��Ű�� ȹ���߽��ϴ�!");
            ShowFakeMessage("��¥ ī��Ű�� ȹ���߽��ϴ�!", Color.green);
        }
        else
        {
            Debug.Log("��¥ ī��Ű�� ȹ���߽��ϴ�!");
            ShowFakeMessage("�̰� ��¥����!", Color.red);
        }
    }

    // UI ������Ʈ
    private void UpdateCardKeyUI()
    {
        if (cardKeyUI != null)
        {
            cardKeyUI.text = "ȹ���� ī��Ű: \n";
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

    // ��¥ ī��Ű �޽��� ���
    private void ShowFakeMessage(string message, Color color)
    {
        if (fakeMessageUI != null)
        {
            fakeMessageUI.text = message;
            fakeMessageUI.color = color;
            fakeMessageUI.gameObject.SetActive(true);

            // 2�� �� �޽��� �����
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
