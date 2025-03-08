using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IdCardItem : MonoBehaviour
{
    public int id;
    M_IdCardActivator activator; // ObjectActivator 스크립트 연결

    private void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Player").GetComponent<M_IdCardActivator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 아이템을 줍는 경우
        {
            activator.ActivateNextObject(id); // 다음 오브젝트 활성화
            Destroy(gameObject); // 아이템 삭제
        }
    }

  
}
