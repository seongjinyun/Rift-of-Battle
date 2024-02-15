using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject panel; // ���� ���� �г�
    private void Start()
    {
        Time.timeScale = 0; // ���� �ð��� �����Ͽ� �Ͻ����� ȿ��
        PauseUi.isPaused = true;

    }
    void Update()
    {
        // ü�� �ؽ�Ʈ ������Ʈ

        panelOff();
    }

    void panelOff()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(false);
            PauseUi.isPaused = false;
            Time.timeScale = 1; // ���� �ð��� �������� �����Ͽ� ���� ����

        }
    }
}
