using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainTitleMenuManager : MonoBehaviour
{
    public AudioSource buttonSound; // ��ư �Ҹ�
    public Image blackScreen; // ������ �̹���
    public float fadeDuration = 2.0f; // ���̵�ƿ� ���� �ð�

    // ���ο� �÷��� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void NewGame()
    {
        // "NewGameScene"�̶�� �̸��� ������ �̵�
        StartCoroutine(PlayNewGameSequence());
    }

    // �̾ �ϱ� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void ContinueGame()
    {
        // ���⼭�� ����� ���� �����͸� �ҷ������� �����ؾ� ��
        // ���� ���, PlayerPrefs���� �����͸� �ҷ����� ���:
        // int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        // SceneManager.LoadScene(savedLevel);
        Debug.Log("�̾ �ϱ� ����� ���� �������� �ʾҽ��ϴ�.");
    }

    // �ɼ� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void Options()
    {
        // �ɼ� �޴��� �̵��ϰų�, �ɼ� UI�� Ȱ��ȭ
        Debug.Log("�ɼ� �޴��� ���� ����� ���� �������� �ʾҽ��ϴ�.");
    }

    // ���� ������ ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void QuitGame()
    {
        // �����Ϳ��� ���Ḧ �õ��ϸ� �۵����� �����Ƿ� ���� ó��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    private IEnumerator PlayNewGameSequence()
    {
        // �Ҹ� ���
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // 2�� ���
        yield return new WaitForSeconds(2.0f);

        // ���̵�ƿ� ����
        yield return StartCoroutine(FadeOut());

        // ���ο� �� �ε�
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