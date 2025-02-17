using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueManager : MonoBehaviour
{
    public GameObject uiElement; 
    public TextMeshProUGUI uiText; 
    public List<string> messages;
    public PlayableDirector timelineDirector; 
    public AudioSource audioSource; 
    public AudioClip typingSound; 

    public float typingSpeed = 0.05f; 
    private int currentMessageIndex = 0; 
    private bool isTyping = false;
    private Coroutine typingCoroutine; 

    public float delayBeforeNextDialogue = 2f; 

    private void Start()
    {
        if (uiElement == null || uiText == null || timelineDirector == null || audioSource == null || typingSound == null)
        {
            return;
        }
        uiElement.SetActive(false);
    }

    public void StartDialogue()
    {
        uiElement.SetActive(true);
        currentMessageIndex = 0;
        typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
    }


    private void ShowNextDialogue()
    {
        if (currentMessageIndex < messages.Count - 1)
        {
            currentMessageIndex++;
            typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
        }
        else
        {
            EndDialogue();
        }
    }


    private void EndDialogue()
    {
        uiElement.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        uiText.text = "";


        string[] parts = text.Split(new char[] { ':' }, 2); 

        if (parts.Length > 1)
        {
            string speaker = parts[0];
            string content = parts[1];


            uiText.text = speaker + ":";


            foreach (char c in content.ToCharArray())
            {
                uiText.text += c;


                if (typingSound != null)
                {
                    audioSource.PlayOneShot(typingSound);
                }
                else
                {
                    Debug.LogWarning("Typing sound is not assigned.");
                }

                yield return new WaitForSeconds(typingSpeed); 
            }
        }
        else
        {
            uiText.text = text; 
        }

        isTyping = false;

        yield return new WaitForSeconds(delayBeforeNextDialogue);
        ShowNextDialogue();
    }
}