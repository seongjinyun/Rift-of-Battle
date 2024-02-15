using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    public AudioSource audioSource; // 사운드를 재생할 AudioSource
    public AudioSource atkAudioSource;
    public AudioSource backAudioSource; // background 사운드

    public Scrollbar volumeScrollbar; // 사운드 조절을 위한 스크롤바

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
    if (volumeScrollbar != null)
    {
        // 이전에 저장된 볼륨 값을 가져옴
        volumeScrollbar.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeScrollbar.onValueChanged.AddListener(OnVolumeChanged); // 스크롤바 값 변경 시 이벤트 리스너 등록

        // atkAudioSource 초기값 설정
        atkAudioSource.volume = volumeScrollbar.value;

        // backAudioSource 초기값 설정
        backAudioSource.volume = volumeScrollbar.value;
        backAudioSource.loop = true; // backAudioSource를 루프 재생하도록 설정
        backAudioSource.Play(); // backAudioSource 재생
    }
    }

    private void OnVolumeChanged(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
            atkAudioSource.volume = value; // atkAudioSource의 볼륨 조절
            backAudioSource.volume = value; // backAudioSource의 볼륨 조절
            PlayerPrefs.SetFloat("Volume", value); // 볼륨 값을 저장
        }
    }


    public void PlaySound(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }

    public void PlayAtkSound(AudioClip soundClip)
    {
        atkAudioSource.clip = soundClip;
        atkAudioSource.Play();
    }
}
