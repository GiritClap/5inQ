using System.Collections;
using UnityEngine;

public class BtoD_DoorScripts : MonoBehaviour
{
    public GameObject LightFloor;
    public GameObject DarkFloor;
    public GameObject Door; // �� ������Ʈ�� �߰��� ����

    void Start()
    {
        DarkFloor.SetActive(false); // �⺻ ���¿��� ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) // �÷��̾ ���� ����� ��
        {
            DarkFloor.SetActive(true);
            LightFloor.SetActive(false);
            StartCoroutine(HideDoorAfterDelay(1f)); // 2�� �� ���� ����� �ڷ�ƾ ����
        }
    }

    IEnumerator HideDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 2�� ���
        Door.SetActive(false); // ���� ��Ȱ��ȭ�Ͽ� ����
    }
}
