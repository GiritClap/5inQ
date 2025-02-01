using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class M_DialogManager : MonoBehaviour
{
    public static M_DialogManager Instance;

    [Header("UI Elements")]
    public Text dialogText;        // ��ȭ �ؽ�Ʈ�� ǥ���� UI
    public GameObject dialogPanel; // ��ȭâ UI �г�
    public Image dialogPanelImage;
    public Image char_1;
    public Text char_1_name;
    public Image char_2;
    public Text char_2_name;

    [Header("Dialog Data")]
    public float typingSpeed = 0.05f; // �ؽ�Ʈ Ÿ���� �ӵ�
    private Queue<string> dialogQueue; // ��� ������ ������ ť

    private bool isTyping = false; // ���� �ؽ�Ʈ�� ��� ������ Ȯ��

    private void Awake()
    {
        // Singleton ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        dialogQueue = new Queue<string>();
        dialogPanel.SetActive(false); // �ʱ⿡�� ��ȭâ ��Ȱ��ȭ
        char_1.gameObject.SetActive(false);
        char_2.gameObject.SetActive(false);
    }

    // ��ȭ�� �����ϴ� �޼���
    public void StartDialog(List<string> dialogLines, string name)
    {
        dialogQueue.Clear();

        foreach (string line in dialogLines)
        {
            dialogQueue.Enqueue(line);
        }
        Debug.Log(dialogQueue.Count);
        dialogPanel.SetActive(true);
        char_1.gameObject.SetActive(true);
        char_2.gameObject.SetActive(false);

        DisplayNextLine(name);
    }

    // ���� ��� ���
    public void DisplayNextLine(string name)
    {
        if (isTyping) return; // Ÿ���� �߿��� ����

        if (dialogQueue.Count == 0)
        {
            EndDialog(name);
            return;
        }

        string line = dialogQueue.Dequeue();
        StartCoroutine(TypeLine(line));
    }

    // �ؽ�Ʈ�� �� ���ھ� ���
    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // ��ȭ ����
    private void EndDialog(string name)
    {
        //SceneManager.LoadScene(name);

        M_LoadingSceneController.LoadScene(name);

        //dialogPanel.SetActive(false);
        //char_1.gameObject.SetActive(false);
        //char_2.gameObject.SetActive(false);

      
    }

    public void ChangeImage(Sprite image)
    {

        dialogPanelImage.sprite = image;

    }

    public void ChangeCharImage(Sprite image, string name, int ab)
    {
        switch (ab)
        {
            case 0:

                char_1.sprite = image;
                char_1_name.text = name;
                char_1.gameObject.SetActive(true);
                char_2.gameObject.SetActive(false);
                break;
            case 1:

                char_2.sprite = image;
                char_2_name .text= name;
                char_1.gameObject.SetActive(false);
                char_2.gameObject.SetActive(true);
                break;
        }

    }

    public int GetDialogQueueCnt()
    {
        return dialogQueue.Count;
    }

}
