using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private int sceneIndexOnFinishVideo;
    public VideoPlayer videoPlayer;

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    private void Start()
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.menuMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            AudioManager.Instance.introMusicEventInstance.start();
        }

    }
    private void OnVideoFinished(VideoPlayer vp)
    {
        if(AudioManager.Instance != null)
        {
            //AudioManager.Instance.menuMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            AudioManager.Instance.introMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            AudioManager.Instance.gameplayMusicEventInstance.start();
        }

        SceneManager.LoadScene(sceneIndexOnFinishVideo);
    }
}
