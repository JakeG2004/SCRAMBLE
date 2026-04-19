using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    private AudioSource _as;

    // UI Sounds
    [SerializeField] private List<AudioClip> _popSounds = new();
    [SerializeField] private List<AudioClip> _uiSounds = new();
    [SerializeField] private List<AudioClip> _successSounds = new();
    [SerializeField] private List<AudioClip> _failSounds = new();
    
    // Customer Sounds
    [SerializeField] private List<AudioClip> _yumSounds = new();
    [SerializeField] private List<AudioClip> _ewSounds = new();
    [SerializeField] private List<AudioClip> _hmSounds = new();

    // Machine sounds
    [SerializeField] private List<AudioClip> _boilerSounds = new();
    [SerializeField] private List<AudioClip> _scramblerSounds = new();
    [SerializeField] private List<AudioClip> _sunnySounds = new();
    [SerializeField] private List<AudioClip> _frierSounds = new();
    [SerializeField] private List<AudioClip> _coopSounds = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
                
                if (clickedObject != null && clickedObject.GetComponent<Button>() != null)
                {
                    SoundManager.PlayPop();
                }
            }
        }
    }

    private void InternalPlayRandom(List<AudioClip> sounds)
    {
        if (sounds == null || sounds.Count == 0) return;
        
        AudioClip randomSound = sounds[Random.Range(0, sounds.Count)];
        _as.PlayOneShot(randomSound);
    }

    // --- Public Static API ---

    public static void PlayPop() => Instance?.InternalPlayRandom(Instance._popSounds);
    public static void PlayUI() => Instance?.InternalPlayRandom(Instance._uiSounds);
    public static void PlaySuccess() => Instance?.InternalPlayRandom(Instance._successSounds);
    public static void PlayFail() => Instance?.InternalPlayRandom(Instance._failSounds);

    public static void PlayYum() => Instance?.InternalPlayRandom(Instance._yumSounds);
    public static void PlayEw() => Instance?.InternalPlayRandom(Instance._ewSounds);
    public static void PlayHm() => Instance?.InternalPlayRandom(Instance._hmSounds);

    public static void PlayBoiler() => Instance?.InternalPlayRandom(Instance._boilerSounds);
    public static void PlayScrambler() => Instance?.InternalPlayRandom(Instance._scramblerSounds);
    public static void PlaySunny() => Instance?.InternalPlayRandom(Instance._sunnySounds);
    public static void PlayFrier() => Instance?.InternalPlayRandom(Instance._frierSounds);
    public static void PlayCoop() => Instance?.InternalPlayRandom(Instance._coopSounds);
}