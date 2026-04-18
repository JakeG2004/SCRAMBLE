using UnityEngine;

public class BaseEgg : MonoBehaviour
{
    [SerializeField] private float _movementPeriod = 1f;
    [SerializeField] private BaseTile _curTile;
    
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
                _renderer.color = Color.green;
                break;

            case EggType.FRIED:
                _renderer.color = Color.red;
                break;

            case EggType.SCRAMBLED:
                _renderer.color = Color.yellow;
                break;

            case EggType.SUNNY_SIDE_UP:
                _renderer.color = Color.blue;
                break;

            case EggType.BOILED:
                _renderer.color = Color.white;
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
