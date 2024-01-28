using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    [field: SerializeField] public EventReference Alarm { get; private set; }
    [field: SerializeField] public EventReference UIClick { get; private set; }
    [field: SerializeField] public EventReference Defeat { get; private set; }
    [field: SerializeField] public EventReference GameplayMusic { get; private set; }
    [field: SerializeField] public EventReference MenuMusic { get; private set; }
    [field: SerializeField] public EventReference IntroMusic { get; private set; }
    [field: SerializeField] public EventReference Laughter { get; private set; }
    [field: SerializeField] public EventReference Shoot { get; private set; }
    [field: SerializeField] public EventReference Siren { get; private set; }
    [field: SerializeField] public EventReference Jump { get; private set; }
}
