using System.Collections.Generic;
using UnityEngine;

public abstract class M_Dialog : MonoBehaviour
{
    public List<string> dialogList = new List<string>();
    public List<Sprite> bgImgList = new List<Sprite>();
    public List<Sprite> charList = new List<Sprite>();

    public string nextSceneName;

    private int currentBgImageIndex = -1; // ���� ������ ��� �̹��� �ε��� ����
    private int currentCharImageIndex = -1; // ���� ������ ĳ���� �̹��� �ε��� ����

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

        // �б� ó���� ���� Ŭ�������� ����
        GetDialogState(out newBgImageIndex, out newCharIndex, out name, out order);

        // ĳ���� �̹��� ����
        if (currentCharImageIndex != newCharIndex)
        {
            currentCharImageIndex = newCharIndex;
            M_DialogManager.Instance.ChangeCharImage(charList[newCharIndex], name, order);
        }

        // ��� �̹��� ����
        if (currentBgImageIndex != newBgImageIndex)
        {
            currentBgImageIndex = newBgImageIndex;
            M_DialogManager.Instance.ChangeImage(bgImgList[newBgImageIndex]);
        }
    }

    /// <summary>
    /// ��ȭ ���¿� ���� �̹��� ���¸� �����մϴ�.
    /// ���� Ŭ�������� �����ؾ� �մϴ�.
    /// </summary>
    protected abstract void GetDialogState(out int bgIndex, out int charIndex, out string charName, out int charOrder);
}
