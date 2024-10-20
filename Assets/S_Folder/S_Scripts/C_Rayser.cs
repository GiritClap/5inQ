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
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾��� Transform ��������
    }

    // �������� �÷��̾ �ٶ󺸵��� z�� ȸ��
    void RotateLaserToFacePlayer(GameObject laser)
    {
        Vector3 directionToPlayer = player.position - laser.transform.position; // �÷��̾� ���� ����
        directionToPlayer.z = 0; // z�� ȸ���� ����

        // ������ ����Ͽ� z�� ȸ���� ����
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        
        // �������� z�� ȸ���� ���� (xy�� ����)
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

            
            // �÷��̾ ���� ���ʿ� ���� �� rayser_L�� Ȱ��ȭ
            rayser_L.SetActive(true);  // ���� ������ Ȱ��ȭ
            rayser_R.SetActive(false); // ������ ������ ��Ȱ��ȭ
            RotateLaserToFacePlayer(rayser_L);
        }
        else
        {
            
            // �÷��̾ ���� �����ʿ� ���� �� rayser_R�� Ȱ��ȭ
            rayser_L.SetActive(false); // ���� ������ ��Ȱ��ȭ
            rayser_R.SetActive(true);  // ������ ������ Ȱ��ȭ
            RotateLaserToFacePlayer(rayser_R);
        }
    }

    public void AttackStart()
    {
        // 0.4�� ���� �� ������ ���� ����
        StartCoroutine(DelayedLaserActivation());
    }

    IEnumerator DelayedLaserActivation()
    {
        yield return new WaitForSeconds(0.4f); // 0.4�� ���
        FacePlayerAndToggleLaser();  // ������ Ȱ��ȭ �� ȸ��
    }

    public void AttackStop()
    {
        // ������ ������ �ߴ��� �� ��� ������ ��Ȱ��ȭ
        rayser_L.SetActive(false);
        rayser_R.SetActive(false);
    }
}
