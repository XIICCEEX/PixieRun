using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip mainBGM; // เพลงเดียวใช้ทั้ง SampleScene และ Ver2

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ตรวจว่าต้องเล่นเพลงไหม
        if (!audioSource.isPlaying && (scene.name == "SampleScene" || scene.name == "Ver2"))
        {
            PlayBGM(mainBGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null || audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ResumeBGM()
    {
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}