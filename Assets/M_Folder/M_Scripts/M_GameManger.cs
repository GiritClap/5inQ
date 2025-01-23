using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_GameManger : MonoBehaviour
{
    private static M_GameManger instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

           
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
          
            Destroy(this.gameObject);
        }
    }

    public static M_GameManger Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    bool[] player = {true, false, false}; // 0 = a, 1 = b, 2 = c
    int num = 0;

    public void StartBtn()
    {
        SceneManager.LoadScene("M_DialogTest");
    }

    public void SelectBtn(GameObject selectPanel)
    {
        selectPanel.SetActive(true);
    }

    public void SelectQuitBtn(GameObject selectPanel)
    {
        selectPanel.SetActive(false);
    }

    public void ABtn()
    {
        for(int i = 0; i < player.Length; i++)
        {
            player[i] = false;
        }
        player[0] = true; // 0 = a, 1 = b, 2 = c
        num = 0;

        StartBtn();
    }

    public void BBtn()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = false;
        }
        player[1] = true; // 0 = a, 1 = b, 2 = c
        num = 1;
        StartBtn();
    }

    public void CBtn()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = false;
        }
        player[2] = true; // 0 = a, 1 = b, 2 = c
        num = 2;
        StartBtn();
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public int CurrentPlayer()
    {
        return num;
    }
}
