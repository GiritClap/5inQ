using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_ObjectControl : MonoBehaviour
{
    public GameObject itemPrefab;  // ����� ������ ������
    public Transform dropPoint;    // �������� ����� ��ġ

    private bool isPlayerNearby = false;

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
            Debug.Log("near");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
