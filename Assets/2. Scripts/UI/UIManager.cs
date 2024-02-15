using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject panel; // 게임 설명 패널
    private void Start()
    {
        Time.timeScale = 0; // 게임 시간을 정지하여 일시정지 효과
        PauseUi.isPaused = true;

    }
    void Update()
    {
        // 체력 텍스트 업데이트

        panelOff();
    }

    void panelOff()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(false);
            PauseUi.isPaused = false;
            Time.timeScale = 1; // 게임 시간을 정상으로 복원하여 게임 진행

        }
    }
}
