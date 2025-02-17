using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainTitleMenuManager : MonoBehaviour
{
    public AudioSource buttonSound; // 버튼 소리
    public Image blackScreen; // 검은색 이미지
    public float fadeDuration = 2.0f; // 페이드아웃 지속 시간

    // 새로운 플레이 버튼을 눌렀을 때 호출되는 함수
    public void NewGame()
    {
        // "NewGameScene"이라는 이름의 씬으로 이동
        StartCoroutine(PlayNewGameSequence());
    }

    // 이어서 하기 버튼을 눌렀을 때 호출되는 함수
    public void ContinueGame()
    {
        // 여기서는 저장된 게임 데이터를 불러오도록 구현해야 함
        // 예를 들어, PlayerPrefs에서 데이터를 불러오는 경우:
        // int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        // SceneManager.LoadScene(savedLevel);
        Debug.Log("이어서 하기 기능은 아직 구현되지 않았습니다.");
    }

    // 옵션 버튼을 눌렀을 때 호출되는 함수
    public void Options()
    {
        // 옵션 메뉴로 이동하거나, 옵션 UI를 활성화
        Debug.Log("옵션 메뉴를 여는 기능은 아직 구현되지 않았습니다.");
    }

    // 게임 나가기 버튼을 눌렀을 때 호출되는 함수
    public void QuitGame()
    {
        // 에디터에서 종료를 시도하면 작동하지 않으므로 예외 처리
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    private IEnumerator PlayNewGameSequence()
    {
        // 소리 재생
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // 2초 대기
        yield return new WaitForSeconds(2.0f);

        // 페이드아웃 시작
        yield return StartCoroutine(FadeOut());

        // 새로운 씬 로드
        SceneManager.LoadScene("scene1");
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color color = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }
    }
}