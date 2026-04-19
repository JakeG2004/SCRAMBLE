using UnityEngine;
using System.Collections;

public class TableTile : BaseTile
{
    [SerializeField] private EggType _desiredType;
    [SerializeField] private SpriteRenderer _eggSprite;
    [SerializeField] private GameObject _orderGraphic;

    [SerializeField] private Sprite _rawEggSprite;
    [SerializeField] private Sprite _friedEggSprite;
    [SerializeField] private Sprite _boiledEggSprite;
    [SerializeField] private Sprite _scrambledEggSprite;
    [SerializeField] private Sprite _sunnySideUpEggSprite;

    private bool _isHappy = false;
    private bool _isMad = false;
    private SpriteRenderer _renderer;
    private Animator _anim;

    public override void Start()
    {
        base.Start();
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        StartCoroutine(WaitAndOrder());
    }

    public override void OnHoverOver()
    {
        _orderGraphic.SetActive(true);
    }

    public override void OnHoverExit()
    {
        _orderGraphic.SetActive(false);
    }

    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);

        if(_isHappy || _isMad)
        {
            Destroy(egg.gameObject);
            return;
        }

        if(egg.GetEggType() != _desiredType)
        {
            _isMad = true;
            Destroy(egg.gameObject);
            StartCoroutine(GetMad());
            return;
        }

        _isHappy = true;
        Destroy(egg.gameObject);
        StartCoroutine(GetHappy());
    }

    // Chooses a new order
    public void CreateNewOrder()
    {
        _desiredType = (EggType)Random.Range(0, 4);
        switch (_desiredType)
        {
            case EggType.RAW:
                _eggSprite.sprite = _rawEggSprite;
                break;

            case EggType.FRIED:
                _eggSprite.sprite = _friedEggSprite;
                break;

            case EggType.SCRAMBLED:
                _eggSprite.sprite = _scrambledEggSprite;
                break;

            case EggType.SUNNY_SIDE_UP:
                _eggSprite.sprite = _sunnySideUpEggSprite;
                break;

            case EggType.BOILED:
                _eggSprite.sprite = _boiledEggSprite;
                break;
        }

        StartCoroutine(ShowOrder());
    }

    private IEnumerator WaitAndOrder()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 5f));
        CreateNewOrder();
    }

    private IEnumerator ShowOrder()
    {
        _orderGraphic.SetActive(true);
        SoundManager.PlayHm();
        yield return new WaitForSeconds(3);
        _orderGraphic.SetActive(false);
    }

    // Waits for 0.5 - 5 seconds then creates a new order
    private IEnumerator GetHappy()
    {
        LevelManager.Instance.ServeCorrect();
        SoundManager.PlayYum();
        _anim.SetBool("isHappy", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("isHappy", false);
        _isHappy = false;

        CreateNewOrder();
    }

    private IEnumerator GetMad()
    {
        LevelManager.Instance.ServeIncorrect();
        SoundManager.PlayEw();
        _anim.SetBool("isMad", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("isMad", false);

        _isMad = false;
    }
}
