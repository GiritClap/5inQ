using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TitleSceneBgm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        M_AudioManager.Instance.Play("MainBgm_1", 0.3f, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
