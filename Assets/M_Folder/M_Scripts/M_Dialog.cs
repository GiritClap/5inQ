using System.Collections.Generic;
using UnityEngine;

public abstract class M_Dialog : MonoBehaviour
{
    public List<string> dialogList = new List<string>();
    public List<Sprite> bgImgList = new List<Sprite>();
    public List<Sprite> charList = new List<Sprite>();

    public string nextSceneName;

    private int currentBgImageIndex = -1; // 현재 설정된 배경 이미지 인덱스 추적
    private int currentCharImageIndex = -1; // 현재 설정된 캐릭터 이미지 인덱스 추적

    protected virtual void Start()
    {
        if (dialogList.Count > 0)
        {
            M_DialogManager.Instance.StartDialog(dialogList ,nextSceneName);
            ChangeImages();
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && M_DialogManager.Instance.dialogPanel.activeSelf)
        {
            M_DialogManager.Instance.DisplayNextLine(nextSceneName);
            ChangeImages();
        }
    }

    protected virtual void ChangeImages()
    {
        int newBgImageIndex;
        int newCharIndex;
        string name;
        int order;

        // 분기 처리를 하위 클래스에서 구현
        GetDialogState(out newBgImageIndex, out newCharIndex, out name, out order);

        // 캐릭터 이미지 변경
        if (currentCharImageIndex != newCharIndex)
        {
            currentCharImageIndex = newCharIndex;
            M_DialogManager.Instance.ChangeCharImage(charList[newCharIndex], name, order);
        }

        // 배경 이미지 변경
        if (currentBgImageIndex != newBgImageIndex)
        {
            currentBgImageIndex = newBgImageIndex;
            M_DialogManager.Instance.ChangeImage(bgImgList[newBgImageIndex]);
        }
    }

    /// <summary>
    /// 대화 상태에 따라 이미지 상태를 설정합니다.
    /// 하위 클래스에서 구현해야 합니다.
    /// </summary>
    protected abstract void GetDialogState(out int bgIndex, out int charIndex, out string charName, out int charOrder);
}
