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

    // 레이저가 플레이어를 바라보도록 z축 회전
    void RotateLaserToFacePlayer(GameObject laser)
    {
        Vector3 directionToPlayer = player.position - laser.transform.position; // 플레이어 방향 벡터
        directionToPlayer.z = 0; // z축 회전만 적용

        // 각도를 계산하여 z축 회전만 적용
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        
        // 레이저의 z축 회전만 설정 (xy는 고정)
        if (player.position.x < transform.position.x)
        {
            laser.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer+180);
        }
        else
        {

            laser.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
        }
        

        //laser.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
    }

    void FacePlayerAndToggleLaser()
    {
        if (player.position.x < transform.position.x)
        {

            
            // 플레이어가 적의 왼쪽에 있을 때 rayser_L만 활성화
            rayser_L.SetActive(true);  // 왼쪽 레이저 활성화
            rayser_R.SetActive(false); // 오른쪽 레이저 비활성화
            RotateLaserToFacePlayer(rayser_L);
        }
        else
        {
            
            // 플레이어가 적의 오른쪽에 있을 때 rayser_R만 활성화
            rayser_L.SetActive(false); // 왼쪽 레이저 비활성화
            rayser_R.SetActive(true);  // 오른쪽 레이저 활성화
            RotateLaserToFacePlayer(rayser_R);
        }
    }

    public void AttackStart()
    {
        // 0.4초 지연 후 레이저 공격 시작
        StartCoroutine(DelayedLaserActivation());
    }

    IEnumerator DelayedLaserActivation()
    {
        yield return new WaitForSeconds(0.4f); // 0.4초 대기
        FacePlayerAndToggleLaser();  // 레이저 활성화 및 회전
    }

    public void AttackStop()
    {
        // 레이저 공격을 중단할 때 모든 레이저 비활성화
        rayser_L.SetActive(false);
        rayser_R.SetActive(false);
    }
}
