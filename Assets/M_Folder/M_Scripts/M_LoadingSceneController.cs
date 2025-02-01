using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Slider progressbar;


    private void Start()
    {
        StartCoroutine(LoadSceneProcess());    
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("M_LoadingScene");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressbar.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressbar.value = Mathf.Lerp(0.9f, 1f, timer);
                if(progressbar.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
