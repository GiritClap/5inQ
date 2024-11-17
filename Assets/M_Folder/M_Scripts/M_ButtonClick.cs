using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ButtonClick : MonoBehaviour
{
    public void ButtonSound(string sfxName)
    {
        M_AudioManager.Instance.Play(sfxName);
    }
}
