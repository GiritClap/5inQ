using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Rayser : MonoBehaviour
{
    Animator animator;
    public Transform player;

    public GameObject rayser_L;
    public GameObject rayser_R;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어의 Transform 가져오기
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FacePlayerAndToggleLaser()
    {
        // 적과 플레이어의 x 좌표를 비교하여 방향 결정
        if (player.position.x < transform.position.x)
        {
            // 플레이어가 왼쪽에 있을 때
            rayser_L.SetActive(true);  // 왼쪽 레이저 활성화
            rayser_R.SetActive(false); // 오른쪽 레이저 비활성화
        }
        else
        {
            // 플레이어가 오른쪽에 있을 때
            rayser_L.SetActive(false); // 왼쪽 레이저 비활성화
            rayser_R.SetActive(true);  // 오른쪽 레이저 활성화
        }
    }

    public void AttackStart()
    {
        // 1초 지연 후 레이저 공격 시작
        StartCoroutine(DelayedLaserActivation());
    }

    IEnumerator DelayedLaserActivation()
    {
        yield return new WaitForSeconds(0.4f); // 1초 대기
        FacePlayerAndToggleLaser();  // 1초 후 레이저 활성화
    }

    public void AttackStop()
    {
        // 레이저 공격을 중단할 때 모든 레이저 비활성화
        rayser_L.SetActive(false);
        rayser_R.SetActive(false);
    }
}



