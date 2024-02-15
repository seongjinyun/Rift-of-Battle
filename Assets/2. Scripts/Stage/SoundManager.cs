using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱��� �ν��Ͻ�

    public AudioSource audioSource; // ���带 ����� AudioSource
    public AudioSource atkAudioSource;
    public AudioSource backAudioSource; // background ����

    public Scrollbar volumeScrollbar; // ���� ������ ���� ��ũ�ѹ�

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �ı����� �ʵ��� ����
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
        // ������ ����� ���� ���� ������
        volumeScrollbar.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeScrollbar.onValueChanged.AddListener(OnVolumeChanged); // ��ũ�ѹ� �� ���� �� �̺�Ʈ ������ ���

        // atkAudioSource �ʱⰪ ����
        atkAudioSource.volume = volumeScrollbar.value;

        // backAudioSource �ʱⰪ ����
        backAudioSource.volume = volumeScrollbar.value;
        backAudioSource.loop = true; // backAudioSource�� ���� ����ϵ��� ����
        backAudioSource.Play(); // backAudioSource ���
    }
    }

    private void OnVolumeChanged(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
            atkAudioSource.volume = value; // atkAudioSource�� ���� ����
            backAudioSource.volume = value; // backAudioSource�� ���� ����
            PlayerPrefs.SetFloat("Volume", value); // ���� ���� ����
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
