using System.Collections;
using UnityEngine;

public class Dark1Fto2F_DoorScripts : MonoBehaviour
{
    public GameObject Dark1Floor;
    public GameObject Dark2Floor;
    private Collider2D doorCollider;
    private bool isOnFirstFloor = true; // ���� �� ���� (true: 1��, false: 2��)

    void Start()
    {
        Dark2Floor.SetActive(false);
        doorCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Debug.Log("���� ����!");
            ToggleFloors(); // �� ��ȯ
            StartCoroutine(DisableColliderTemporarily()); // �ݶ��̴� 2�� ��Ȱ��ȭ
        }
    }

    void ToggleFloors()
    {
        if (isOnFirstFloor)
        {
            Dark1Floor.SetActive(false);
            Dark2Floor.SetActive(true);
        }
        else
        {
            Dark1Floor.SetActive(true);
            Dark2Floor.SetActive(false);
        }
        isOnFirstFloor = !isOnFirstFloor; // ���� ����
    }

    IEnumerator DisableColliderTemporarily()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
            yield return new WaitForSeconds(2f);
            doorCollider.enabled = true;
        }
    }
}
