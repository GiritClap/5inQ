using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IdCardItem : MonoBehaviour
{
    public int id;
    M_IdCardActivator activator; // ObjectActivator ��ũ��Ʈ ����

    public float amplitude = 0.5f; // �������� ���� (����)
    public float speed = 2f; // �����̴� �ӵ�
    public float floatSpeed = 2f; // ���Ʒ� �ӵ�
    public float fadeSpeed = 0.5f; // ���� ������� �ӵ�

    private Vector3 startPos;

    private bool isPlayerNearby = false;

    private SpriteRenderer spriteRenderer;
    private float alpha = 1f; // �ʱ� ����

    private void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Player").GetComponent<M_IdCardActivator>();
        startPos = transform.position; // �ʱ� ��ġ ����
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ��������

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") ) // �÷��̾ �������� �ݴ� ���
        {
            isPlayerNearby = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� �ݴ� ���
        {
            isPlayerNearby = false;

        }
    }

    void Update()
    {
        // Sin �Լ��� �̿��� Y ��ġ�� ���� (�ε巯�� ���Ʒ� ������)
        FloatEffect();

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.K))
        {
            if(id == 10)
            {
                StartCoroutine(FadeOutEffect()); // ������� ȿ�� ����
                return;
            }
            activator.ActivateNextObject(id); // ���� ������Ʈ Ȱ��ȭ
            Destroy(gameObject); // ������ ����
        }

    }
    void FloatEffect()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time * floatSpeed) * amplitude, startPos.z);
    }

    IEnumerator FadeOutEffect()
    {
        while (alpha > 0f) // alpha�� 0���� Ŭ ���� ��� ����
        {
            alpha -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(0f, 0f, 0f, alpha);
            yield return null; // ���� �����ӱ��� ���
        }

        Destroy(gameObject); // ������ ���������� ����
    }
}
