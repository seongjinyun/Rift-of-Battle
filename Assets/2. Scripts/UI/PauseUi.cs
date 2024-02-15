using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUi : MonoBehaviour
{
    public GameObject pausePanel; // 일시정지 패널
    public GameObject ESc; // ESc버튼

    public static bool isPaused = false; // 게임 일시정지 여부
    public TextMeshProUGUI healthText; // HP 텍스트 메시 프로 오브젝트

    private static PauseUi instance; // PauseUi 인스턴스

    private void Awake()
    {
        // 이미 인스턴스가 존재하면 해당 게임오브젝트를 파괴
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동 시에도 유지되도록 게임오브젝트를 파괴하지 않음
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때마다 호출되는 이벤트에 OnSceneLoaded 메서드를 등록
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 시작 씬으로 이동할 때 ESC 버튼을 활성화하고, 그 외의 씬에서는 비활성화
        if (scene.name == "StartScene")
        {
            ESc.SetActive(false); // ESC 버튼
            healthText.enabled = false;
        }
        else
        {
            ESc.SetActive(true); // ESC 버튼
            healthText.enabled = true;
        }

        // 시작 씬으로 이동하면 PauseUi 스크립트를 파괴하지 않고 오브젝트들을 비활성화
        if (scene.name == "StartScene")
        {
            pausePanel.SetActive(false); // 일시정지 패널을 비활성화하여 숨김
        }
    }

    void Update()
    {
        healthText.text = "체력: " + PlayerDie.currentHealth.ToString();

        // ESC 키를 눌렀을 때 일시정지 처리
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
        isPaused = true; // 일시정지 상태로 설정
        Time.timeScale = 0; // 게임 시간을 정지하여 일시정지 효과
        pausePanel.SetActive(true); // 일시정지 패널을 활성화하여 보여줌
    }

    public void ResumeGame()
    {
        isPaused = false; // 일시정지 상태 해제
        Time.timeScale = 1; // 게임 시간을 정상으로 복원하여 게임 진행
        pausePanel.SetActive(false); // 일시정지 패널을 비활성화하여 숨김
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
