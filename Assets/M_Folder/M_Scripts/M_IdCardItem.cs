using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IdCardItem : MonoBehaviour
{
    public int id;
    M_IdCardActivator activator; // ObjectActivator ��ũ��Ʈ ����

    private void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Player").GetComponent<M_IdCardActivator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� �ݴ� ���
        {
            activator.ActivateNextObject(id); // ���� ������Ʈ Ȱ��ȭ
            Destroy(gameObject); // ������ ����
        }
    }

  
}
