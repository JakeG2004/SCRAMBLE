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

    public BaseTile GetTile()
    {
        return _tile;
    }
}
