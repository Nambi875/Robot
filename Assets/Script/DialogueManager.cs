using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text TalkText;
    public GameObject scanObject;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        TalkText.text = "ª³ªìªÏ" + scanObject.name + "ªÎªèª¦ªÀ¡£";
    }

}
