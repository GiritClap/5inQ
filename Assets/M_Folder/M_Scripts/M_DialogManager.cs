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
    public Text dialogText;        // 대화 텍스트를 표시할 UI
    public GameObject dialogPanel; // 대화창 UI 패널
    public Image dialogPanelImage;
    public Image char_1;
    public Text char_1_name;
    public Image char_2;
    public Text char_2_name;

    [Header("Dialog Data")]
    public float typingSpeed = 0.05f; // 텍스트 타이핑 속도
    private Queue<string> dialogQueue; // 대사 내용을 저장할 큐

    private bool isTyping = false; // 현재 텍스트를 출력 중인지 확인

    private void Awake()
    {
        // Singleton 설정
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
        dialogPanel.SetActive(false); // 초기에는 대화창 비활성화
        char_1.gameObject.SetActive(false);
        char_2.gameObject.SetActive(false);
    }

    // 대화를 시작하는 메서드
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

    // 다음 대사 출력
    public void DisplayNextLine(string name)
    {
        if (isTyping) return; // 타이핑 중에는 무시

        if (dialogQueue.Count == 0)
        {
            EndDialog(name);
            return;
        }

        string line = dialogQueue.Dequeue();
        StartCoroutine(TypeLine(line));
    }

    // 텍스트를 한 글자씩 출력
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

    // 대화 종료
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
