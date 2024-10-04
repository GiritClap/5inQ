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

    // Update is called once per frame
    void Update()
    {

    }

    void FacePlayerAndToggleLaser()
    {
        // ���� �÷��̾��� x ��ǥ�� ���Ͽ� ���� ����
        if (player.position.x < transform.position.x)
        {
            // �÷��̾ ���ʿ� ���� ��
            rayser_L.SetActive(true);  // ���� ������ Ȱ��ȭ
            rayser_R.SetActive(false); // ������ ������ ��Ȱ��ȭ
        }
        else
        {
            // �÷��̾ �����ʿ� ���� ��
            rayser_L.SetActive(false); // ���� ������ ��Ȱ��ȭ
            rayser_R.SetActive(true);  // ������ ������ Ȱ��ȭ
        }
    }

    public void AttackStart()
    {
        // 1�� ���� �� ������ ���� ����
        StartCoroutine(DelayedLaserActivation());
    }

    IEnumerator DelayedLaserActivation()
    {
        yield return new WaitForSeconds(0.4f); // 1�� ���
        FacePlayerAndToggleLaser();  // 1�� �� ������ Ȱ��ȭ
    }

    public void AttackStop()
    {
        // ������ ������ �ߴ��� �� ��� ������ ��Ȱ��ȭ
        rayser_L.SetActive(false);
        rayser_R.SetActive(false);
    }
}



