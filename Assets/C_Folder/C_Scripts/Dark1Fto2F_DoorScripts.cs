using System.Collections;
using UnityEngine;

public class Dark1Fto2F_DoorScripts : MonoBehaviour
{
    public GameObject Dark1Floor;
    public GameObject Dark2Floor;

    void Start()
    {
        Dark2Floor.SetActive(false); // �⺻ ���¿��� ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) // �÷��̾ ���� ����� ��
        {
            Debug.Log("���� ����!");
            Dark2Floor.SetActive(true);
            Dark1Floor.SetActive(false);
        }
    }
}
