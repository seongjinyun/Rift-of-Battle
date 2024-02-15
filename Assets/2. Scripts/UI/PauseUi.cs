using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUi : MonoBehaviour
{
    public GameObject pausePanel; // �Ͻ����� �г�
    public GameObject ESc; // ESc��ư

    public static bool isPaused = false; // ���� �Ͻ����� ����
    public TextMeshProUGUI healthText; // HP �ؽ�Ʈ �޽� ���� ������Ʈ

    private static PauseUi instance; // PauseUi �ν��Ͻ�

    private void Awake()
    {
        // �̹� �ν��Ͻ��� �����ϸ� �ش� ���ӿ�����Ʈ�� �ı�
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // �� �̵� �ÿ��� �����ǵ��� ���ӿ�����Ʈ�� �ı����� ����
        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ�� OnSceneLoaded �޼��带 ���
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ������ �̵��� �� ESC ��ư�� Ȱ��ȭ�ϰ�, �� ���� �������� ��Ȱ��ȭ
        if (scene.name == "StartScene")
        {
            ESc.SetActive(false); // ESC ��ư
            healthText.enabled = false;
        }
        else
        {
            ESc.SetActive(true); // ESC ��ư
            healthText.enabled = true;
        }

        // ���� ������ �̵��ϸ� PauseUi ��ũ��Ʈ�� �ı����� �ʰ� ������Ʈ���� ��Ȱ��ȭ
        if (scene.name == "StartScene")
        {
            pausePanel.SetActive(false); // �Ͻ����� �г��� ��Ȱ��ȭ�Ͽ� ����
        }
    }

    void Update()
    {
        healthText.text = "ü��: " + PlayerDie.currentHealth.ToString();

        // ESC Ű�� ������ �� �Ͻ����� ó��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true; // �Ͻ����� ���·� ����
        Time.timeScale = 0; // ���� �ð��� �����Ͽ� �Ͻ����� ȿ��
        pausePanel.SetActive(true); // �Ͻ����� �г��� Ȱ��ȭ�Ͽ� ������
    }

    public void ResumeGame()
    {
        isPaused = false; // �Ͻ����� ���� ����
        Time.timeScale = 1; // ���� �ð��� �������� �����Ͽ� ���� ����
        pausePanel.SetActive(false); // �Ͻ����� �г��� ��Ȱ��ȭ�Ͽ� ����
    }

    public void OnPause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void MainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
