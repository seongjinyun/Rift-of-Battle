using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class donStageCanvas : MonoBehaviour
{
    public static int currentStage = 1; // 현재 스테이지 변수
    public TextMeshProUGUI stageText;

    private void Awake()
    {
        if (FindObjectOfType<donStageCanvas>() != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // 다른 씬으로 전환되어도 삭제되지 않도록 설정
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "StartScene" || scene.name == "Tutorial")
        {
            stageText.gameObject.SetActive(false);
        }
        else
        {
            stageText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        stageText.text = currentStage + " STAGE";
    }
}
