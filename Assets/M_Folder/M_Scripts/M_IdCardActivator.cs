using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IdCardActivator : MonoBehaviour
{
    public List<GameObject> objects; // Ȱ��ȭ�� ������Ʈ ����Ʈ

    void Start()
    {
        // ������ �� ��� ������Ʈ ��Ȱ��ȭ
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    // Ư�� ������Ʈ�� �ֿ� �� ȣ���� �Լ�
    public void ActivateNextObject(int id)
    {

        objects[id].SetActive(true);

    }
}
