using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_DialogSample : MonoBehaviour
{
    public List<string> sampleDialog = new List<string>
{
    "대충 여기다가",
    "스크립트 짠 부분을 넣어주면",
    "잘 적용이 될 것이라고 봅니다",
    "분기 처리는 현재 하드 코딩이라 추후 업뎃 필요합니다요"
};

    public List<Sprite> changeImgList = new List<Sprite>();

    public List<Sprite> charList = new List<Sprite>();

    private void Start()
    {
        // 디버깅용 게임 시작 시 대화 시작
        M_DialogManager.Instance.StartDialog(sampleDialog);
        ChangeBgImg();
        ChangeCharImg();
    }

    private int currentBgImageIndex = -1; // 현재 설정된 이미지 인덱스 추적

    private void ChangeBgImg()
    {
        int newImageIndex;

        // 대화 상태에 따라 변경할 이미지 인덱스 결정
        if (M_DialogManager.Instance.GetDialogQueueCnt() == 0 || M_DialogManager.Instance.GetDialogQueueCnt() == 1)
        {
            newImageIndex = 1; 
        }
        else
        {
            newImageIndex = 0; 
        }

        // 현재 이미지와 동일하면 변경하지 않음
        if (currentBgImageIndex != newImageIndex)
        {
            currentBgImageIndex = newImageIndex;
            M_DialogManager.Instance.ChangeImage(changeImgList[newImageIndex]);
        }
    }

    private int currentCharImageIndex = -1; // 현재 설정된 캐릭터 이미지 인덱스 추적

    private void ChangeCharImg()
    {
        int newCharIndex;
        string name;
        int order;


        switch (M_DialogManager.Instance.GetDialogQueueCnt())
        {
            case 0:
                newCharIndex = 3;
                name = "Char_3";
                order = 1;
                break;
            case 1:
                newCharIndex = 2; 
                name = "Char_2";
                order = 0;
                break;
            case 2:
                newCharIndex = 1;
                name = "Char_1";
                order = 1;
                break;
            default: 
                newCharIndex = 0;
                name = "Char_0";
                order = 0;
                break;
        }

        // 현재 이미지와 동일하면 변경하지 않음
        if (currentCharImageIndex != newCharIndex)
        {
            currentCharImageIndex = newCharIndex;
           
            M_DialogManager.Instance.ChangeCharImage(charList[newCharIndex], name, order);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && M_DialogManager.Instance.dialogPanel.activeSelf)
        {
            M_DialogManager.Instance.DisplayNextLine();
            ChangeBgImg();
            ChangeCharImg();
        }


    }


}
