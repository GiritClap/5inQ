using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtoH_DoorScripts : MonoBehaviour
{

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) // �÷��̾ ���� ����� ��
        {
            SceneManager.LoadScene("HiddenMapScene");
        }
    }

    
}
