using UnityEngine;

public class DirectionSelector : MonoBehaviour
{
    private BaseTile _tile;
    [SerializeField] private bool _hasClicked = false;

    void Awake()
    {
        _tile = transform.parent.GetComponent<BaseTile>();
    }

    public void SetDirection(Direction dir)
    {
        SoundManager.PlayPop();
        
        // Handle setting source first if not clicked
        if(!_hasClicked)
        {
            _tile.SetInputDirection(dir);
            _hasClicked = true;

        }

        else
        {
            _tile.SetOutputDirection(dir);
            _hasClicked = false;
        }
    }

    void OnEnable()
    {
        _hasClicked = false;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.localScale = transform.parent.localScale;
    }

    public BaseTile GetTile()
    {
        return _tile;
    }
}
