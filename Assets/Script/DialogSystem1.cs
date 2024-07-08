using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerZone : MonoBehaviour
{
    public GameObject uiElement;
    public TextMeshProUGUI uiText;
    public List<string> messages;
    private int currentMessageIndex = 0;

    private bool isPlayerInZone = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        if (uiElement == null || uiText == null)
        {
            Debug.LogError("UI Element or UI Text is not assigned!");
            return;
        }
        uiElement.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (!uiElement.activeSelf)
            {
                uiElement.SetActive(true);
                currentMessageIndex = 0;
                typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
            }
            else if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                uiText.text = messages[currentMessageIndex];
                isTyping = false;
            }
            else if (!isTyping && currentMessageIndex < messages.Count - 1)
            {
                currentMessageIndex++;
                typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
            }
            else
            {
                uiElement.SetActive(false);
                StopAllCoroutines();
            }
            Debug.Log("UI Element Toggled: " + uiElement.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            Debug.Log("Player Entered Trigger Zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            uiElement.SetActive(false);
            StopAllCoroutines();
            Debug.Log("Player Exited Trigger Zone");
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        uiText.text = "";
        foreach (char c in text.ToCharArray())
        {
            uiText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }
}








