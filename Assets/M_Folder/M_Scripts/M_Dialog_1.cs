using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Dialog_1 : M_Dialog
{
    public GameObject panel1;
    public GameObject panel2;

    protected override void Start()
    {
        M_DialogManager.Instance.StartDialogPanel();
        // panel1�� �ʱ� ��ġ�� ���� (y=100)
        panel1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 100f);
        panel2.SetActive(false);
        StartCoroutine(MovePanel());
    }

    protected override void GetDialogState(out int bgIndex, out int charIndex, out string charName, out int charOrder)
    {
        switch (M_DialogManager.Instance.GetDialogQueueCnt())
        {
            case 0:
                bgIndex = 0;
                charIndex = 1;
                charName = "Char_1";
                charOrder = 1;
                break;
            case 1:
                bgIndex = 0;
                charIndex = 0;
                charName = "Char_0";
                charOrder = 0;
                break;
            case 2:
                bgIndex = 0;
                charIndex = 1;
                charName = "Char_1";
                charOrder = 1;
                break;
            default:
                bgIndex = 0;
                charIndex = 0;
                charName = "Char_0";
                charOrder = 0;
                break;
        }
    }

    IEnumerator MovePanel()
    {
        // panel1�� RectTransform�� ������
        RectTransform panelRect = panel1.GetComponent<RectTransform>();
        Vector2 startPos = new Vector2(0f, -50f);  // �ʱ� ��ġ (y=100)
        Vector2 endPos = new Vector2(0f, 100f);    // ��ǥ ��ġ (y=-50)
        float duration = 3f; // �̵� �ð��� ���� (1��)
        float timeElapsed = 0f;

        // panel1�� y���� �ε巴�� ��ȭ��Ű��
        while (timeElapsed < duration)
        {
            // y���� �ε巴�� �̵�
            float newY = Mathf.Lerp(startPos.y, endPos.y, timeElapsed / duration);
            panelRect.anchoredPosition = new Vector2(startPos.x, newY);

            timeElapsed += Time.deltaTime; // �ð� ���
            yield return null; // ���� �����ӱ��� ��ٸ�
        }

        // �� ��ġ�� ��Ȯ�� ����
        //panelRect.anchoredPosition = endPos;
        panel2.SetActive(true);  // �̵��� ������ panel2 Ȱ��ȭ
        base.Start();

    }
}
