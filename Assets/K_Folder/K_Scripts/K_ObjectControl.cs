using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �ʿ�

public class K_ObjectControl : MonoBehaviour
{
    public GameObject itemPrefab;  // ����� ������ ������
    public Transform dropPoint;    // �������� ����� ��ġ
    public GameObject interactionUI; // "��ȣ�ۿ� ����" UI (Canvas�� Text)

    private bool isPlayerNearby = false;

    void Start()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // ó������ UI�� ����
        }
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
        }
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
