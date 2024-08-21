using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueManagerE : MonoBehaviour
{
    public GameObject uiElement; // 대화창 UI
    public TextMeshProUGUI uiText; // 대화 내용을 표시할 텍스트
    public List<string> messages; // 대화 메시지 리스트
    public PlayableDirector timelineDirector; // 타임라인 디렉터
    private int currentMessageIndex = 0;

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        if (uiElement == null || uiText == null || timelineDirector == null)
        {
            Debug.LogError("UI Element, UI Text or Timeline Director is not assigned!");
            return;
        }
        uiElement.SetActive(false);
    }

    // 타임라인에서 신호를 받아 대화를 시작하는 메서드
    public void StartDialogue()
    {
        uiElement.SetActive(true);
        currentMessageIndex = 0;
        typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
        timelineDirector.Pause(); // 대화가 시작되면 타임라인 일시정지
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping && uiElement.activeSelf)
        {
            ShowNextDialogue();
        }
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
        timelineDirector.Resume(); // 대화가 끝나면 타임라인 재개
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
