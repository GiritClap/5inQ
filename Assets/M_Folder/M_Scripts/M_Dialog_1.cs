using System.Collections.Generic;
using UnityEngine;

public class M_Dialog_1 : M_Dialog
{
    protected override void Start()
    {

        base.Start();
    }

    protected override void GetDialogState(out int bgIndex, out int charIndex, out string charName, out int charOrder)
    {
        switch (M_DialogManager.Instance.GetDialogQueueCnt())
        {
            case 0:
                bgIndex = 1;
                charIndex = 3;
                charName = "Char_3";
                charOrder = 1;
                break;
            case 1:
                bgIndex = 1;
                charIndex = 2;
                charName = "Char_2";
                charOrder = 0;
                break;
            case 2:
                bgIndex = 0;
                charIndex = 1;
                charName = "Char_1";
                charOrder = 1;
                break;
            default:
                bgIndex = 0;
                charIndex = 0;
                charName = "Char_0";
                charOrder = 0;
                break;
        }
    }
}
