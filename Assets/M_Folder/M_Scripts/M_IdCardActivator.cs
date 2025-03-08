using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IdCardActivator : MonoBehaviour
{
    public List<GameObject> objects; // 활성화할 오브젝트 리스트

    void Start()
    {
        // 시작할 때 모든 오브젝트 비활성화
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    // 특정 오브젝트를 주울 때 호출할 함수
    public void ActivateNextObject(int id)
    {

        objects[id].SetActive(true);

    }
}
