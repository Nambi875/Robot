using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueManagerE : MonoBehaviour
{
    public GameObject uiElement; // ��ȭâ UI
    public TextMeshProUGUI uiText; // ��ȭ ������ ǥ���� �ؽ�Ʈ
    public List<string> messages; // ��ȭ �޽��� ����Ʈ
    public PlayableDirector timelineDirector; // Ÿ�Ӷ��� ����
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

    // Ÿ�Ӷ��ο��� ��ȣ�� �޾� ��ȭ�� �����ϴ� �޼���
    public void StartDialogue()
    {
        uiElement.SetActive(true);
        currentMessageIndex = 0;
        typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
        timelineDirector.Pause(); // ��ȭ�� ���۵Ǹ� Ÿ�Ӷ��� �Ͻ�����
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
        timelineDirector.Resume(); // ��ȭ�� ������ Ÿ�Ӷ��� �簳
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
