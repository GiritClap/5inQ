using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_FirstSceneManager : MonoBehaviour
{
    int curPlayerNum = 0;
    GameObject curPlayer;
    public GameObject[] player;
    // Start is called before the first frame update
    void Start()
    {
        curPlayerNum = M_GameManger.Instance.CurrentPlayer();
        Debug.Log(curPlayerNum);
        switch(curPlayerNum)
        {
            case 0:
                curPlayer = Instantiate(player[0]);
                break;
            case 1:
                curPlayer = Instantiate(player[1]);
                break;
            case 2:
                curPlayer = Instantiate(player[2]);
                break;

        }
    }

  
   
}
