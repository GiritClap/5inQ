using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_FirstSceneManager : MonoBehaviour
{
    int curPlayerNum = 0;
    GameObject curPlayer;

    [Header("Player")]
    public GameObject[] player;
    public GameObject playerSpawnPos;
/*
    [Header("Enemy")]
    public GameObject[] enemy;
    public GameObject[] aSpawnPos;
    public GameObject[] bSpawnPos;
    public GameObject[] cSpawnPos;
*/

    // Start is called before the first frame update
    void Start()
    {
        curPlayerNum = M_GameManger.Instance.CurrentPlayer();
        switch(curPlayerNum)
        {
            case 0:
                curPlayer = Instantiate(player[0], playerSpawnPos.transform.position, Quaternion.identity);
                break;
            case 1:
                curPlayer = Instantiate(player[1], playerSpawnPos.transform.position, Quaternion.identity);
                break;
            case 2:
                curPlayer = Instantiate(player[2], playerSpawnPos.transform.position, Quaternion.identity);
                break;

        }

        //SpawnEnemy();
    }

/*    
    void SpawnEnemy()
    {
        for(int i = 0; i < aSpawnPos.Length; i++)
        {
            Instantiate(enemy[0], aSpawnPos[i].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < bSpawnPos.Length; i++)
        {
            Instantiate(enemy[1], bSpawnPos[i].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < cSpawnPos.Length; i++)
        {
            Instantiate(enemy[2], cSpawnPos[i].transform.position, Quaternion.identity);
        }
    }*/
  
   
}
