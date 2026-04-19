using UnityEngine;

public class BaseEgg : MonoBehaviour
{
    [SerializeField] private float _movementPeriod = 1f;
    [SerializeField] private BaseTile _curTile;

    [SerializeField] private Sprite _rawEggSprite;
    [SerializeField] private Sprite _friedEggSprite;
    [SerializeField] private Sprite _boiledEggSprite;
    [SerializeField] private Sprite _scrambledEggSprite;
    [SerializeField] private Sprite _sunnySideUpEggSprite;
    
    private EggType _eggType;
    private float _elapsedTime = 0f;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= _movementPeriod)
        {
            MoveEgg();
            _elapsedTime = 0f;
        }
    }

    public void SetEggType(EggType newType)
    {
        _eggType = newType;

        switch (_eggType)
        {
            case EggType.RAW:
                _renderer.sprite = _rawEggSprite;
                break;

            case EggType.FRIED:
                _renderer.sprite = _friedEggSprite;
                break;

            case EggType.SCRAMBLED:
                _renderer.sprite = _scrambledEggSprite;
                break;

            case EggType.SUNNY_SIDE_UP:
                _renderer.sprite = _sunnySideUpEggSprite;
                break;

            case EggType.BOILED:
                _renderer.sprite = _boiledEggSprite;
                break;
        }
    }

    public EggType GetEggType()
    {
        return _eggType;
    }

    public void SetCurTile(BaseTile tile)
    {
        _curTile = tile;
    }

    public virtual void MoveEgg()
    {
        if(!_curTile.CanMoveEgg())
        {
            return;
        }

        _curTile.GetOutputTile().OnGetEgg(this);
    }
}

public enum EggType
{
    RAW,
    FRIED,
    SCRAMBLED,
    SUNNY_SIDE_UP,
    BOILED
}
