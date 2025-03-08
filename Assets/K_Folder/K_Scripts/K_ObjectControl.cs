using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �ʿ�

public class K_ObjectControl : MonoBehaviour
{
    public GameObject itemPrefab;  // ����� ������ ������
    public Transform dropPoint;    // �������� ����� ��ġ
    public GameObject interactionUI; // "��ȣ�ۿ� ����" UI (Canvas�� Text)
    //public Text cardKeyUI;  // ī��Ű UI �ؽ�Ʈ

    private bool isPlayerNearby = false;
    //private string cardKey = "";  // ī��Ű ���� ����

    void Start()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // ó������ UI�� ����
        }

       /* if (cardKeyUI != null)
        {
            cardKeyUI.text = "Card Key: None";  // ���� �� ī��Ű ���� �ʱ�ȭ
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
            Debug.Log("������ ���!");

            // GŰ�� ������ �� UI �����
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
    }

    private void AcquireCardKey(string newCardKey)
    {
        // ī��Ű�� ȹ������ ��
       /* cardKey = newCardKey;
        Debug.Log("Card Key Acquired: " + cardKey);*/

        // UI�� ī��Ű ���� ������Ʈ
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
                interactionUI.SetActive(true); // �÷��̾ ��������� UI ǥ��
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
                interactionUI.SetActive(false); // �÷��̾ �־����� UI ����
            }
        }
    }
}