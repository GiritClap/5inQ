using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_DialogSample : MonoBehaviour
{
    public List<string> sampleDialog = new List<string>
{
    "���� ����ٰ�",
    "��ũ��Ʈ § �κ��� �־��ָ�",
    "�� ������ �� ���̶�� ���ϴ�",
    "�б� ó���� ���� �ϵ� �ڵ��̶� ���� ���� �ʿ��մϴٿ�"
};

    public List<Sprite> changeImgList = new List<Sprite>();

    public List<Sprite> charList = new List<Sprite>();

    private void Start()
    {
        // ������ ���� ���� �� ��ȭ ����
        M_DialogManager.Instance.StartDialog(sampleDialog);
        ChangeBgImg();
        ChangeCharImg();
    }

    private int currentBgImageIndex = -1; // ���� ������ �̹��� �ε��� ����

    private void ChangeBgImg()
    {
        int newImageIndex;

        // ��ȭ ���¿� ���� ������ �̹��� �ε��� ����
        if (M_DialogManager.Instance.GetDialogQueueCnt() == 0 || M_DialogManager.Instance.GetDialogQueueCnt() == 1)
        {
            newImageIndex = 1; 
        }
        else
        {
            newImageIndex = 0; 
        }

        // ���� �̹����� �����ϸ� �������� ����
        if (currentBgImageIndex != newImageIndex)
        {
            currentBgImageIndex = newImageIndex;
            M_DialogManager.Instance.ChangeImage(changeImgList[newImageIndex]);
        }
    }

    private int currentCharImageIndex = -1; // ���� ������ ĳ���� �̹��� �ε��� ����

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

        // ���� �̹����� �����ϸ� �������� ����
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
