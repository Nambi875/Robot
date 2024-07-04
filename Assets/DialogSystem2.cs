using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem2 : MonoBehaviour
{
    public GameObject uiElement;
    public TextMeshProUGUI uiText; // TextMeshProUGUI로 변경
    public List<string> messages; // 오브젝트마다 다르게 설정할 대화 목록
    private int currentMessageIndex = 0; // 현재 대화 인덱스
    Animator anim;

    private bool isPlayerInZone = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
                anim.GetBool("IsSpeaking?");
                anim.SetBool("IsSpeaking?", true);
                uiElement.SetActive(true);
                currentMessageIndex = 0;
                typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex])); // 첫 번째 대화 출력 시작
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
                typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex])); // 다음 대화 출력
            }
            else
            {
                uiElement.SetActive(false);
                anim.SetBool("IsSpeaking?", false);
                StopAllCoroutines(); // 코루틴 중지
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
            StopAllCoroutines(); // 코루틴 중지
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
            yield return new WaitForSeconds(0.05f); // 글자 간의 딜레이
        }
        isTyping = false;
    }
}







