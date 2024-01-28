using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public EventInstance menuMusicEventInstance { get; private set; }
    public EventInstance introMusicEventInstance { get; private set; }
    public EventInstance gameplayMusicEventInstance { get; private set; }

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeMenuMusic(FmodEvents.Instance.MenuMusic);
        InitializeGameplayMusic(FmodEvents.Instance.GameplayMusic);
        InitializeIntroMusic(FmodEvents.Instance.IntroMusic);

        menuMusicEventInstance.start();
        gameplayMusicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        introMusicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE) ;
    }
    private void InitializeMenuMusic(EventReference musicEventReference)
    {
        menuMusicEventInstance = CreateInstance(musicEventReference);
    }
    private void InitializeIntroMusic(EventReference musicEventReference)
    {
        introMusicEventInstance = CreateInstance(musicEventReference);
    }
    private void InitializeGameplayMusic(EventReference musicEventReference)
    {
        gameplayMusicEventInstance = CreateInstance(musicEventReference);
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void OnDestroy()
    {
        CleanUp();
    }
    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        // stop all of the event emitters, because if we don't they may hang around in other scenes
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
