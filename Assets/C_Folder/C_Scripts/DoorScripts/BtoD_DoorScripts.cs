using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtoD_DoorScripts : MonoBehaviour
{

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) // 플레이어가 문에 닿았을 때
        {
            SceneManager.LoadScene("DarkMapScene");
        }
    }

    
}
