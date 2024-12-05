using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScripts : MonoBehaviour
{
    public GameObject OneFloor;
    public GameObject TwoFloor;
    public GameObject check;
    public GameObject check1;

    // Start is called before the first frame update
    void Start()
    {
        TwoFloor.SetActive(false); // �⺻ ���¿��� ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Stair"))
        {
            TwoFloor.SetActive(true);
            OneFloor.SetActive(false);
            StartCoroutine(ActivateCheckAfterDelay());
        }
        else if(coll.CompareTag("Onefloor"))
        {
            TwoFloor.SetActive(false);
            OneFloor.SetActive(true);
            check.SetActive(false);
            check1.SetActive(false);
        }
    }

    private IEnumerator ActivateCheckAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� ���
        check.SetActive(true);                // check Ȱ��ȭ
        check1.SetActive(true);
    }
}