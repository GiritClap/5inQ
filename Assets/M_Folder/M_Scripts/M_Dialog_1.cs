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
        // panel1의 초기 위치를 설정 (y=100)
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
        // panel1의 RectTransform을 가져옴
        RectTransform panelRect = panel1.GetComponent<RectTransform>();
        Vector2 startPos = new Vector2(0f, -50f);  // 초기 위치 (y=100)
        Vector2 endPos = new Vector2(0f, 100f);    // 목표 위치 (y=-50)
        float duration = 3f; // 이동 시간을 설정 (1초)
        float timeElapsed = 0f;

        // panel1의 y값을 부드럽게 변화시키기
        while (timeElapsed < duration)
        {
            // y값만 부드럽게 이동
            float newY = Mathf.Lerp(startPos.y, endPos.y, timeElapsed / duration);
            panelRect.anchoredPosition = new Vector2(startPos.x, newY);

            timeElapsed += Time.deltaTime; // 시간 경과
            yield return null; // 다음 프레임까지 기다림
        }

        // 끝 위치에 정확히 도달
        //panelRect.anchoredPosition = endPos;
        panel2.SetActive(true);  // 이동이 끝나면 panel2 활성화
        base.Start();

    }
}
