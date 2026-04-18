using UnityEngine;

public class DirectionButtonController : MonoBehaviour
{
    private DirectionSelector _ds;
    [SerializeField] private Direction dir;
    [SerializeField] private KeyCode _dirKey;

    private SpriteRenderer _renderer;
    private Color _baseColor = Color.white;

    void OnEnable()
    {
        _ds = transform.parent.GetComponent<DirectionSelector>();    
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown(_dirKey))
        {
            _ds.SetDirection(dir);
        }

        if(_ds.GetTile().GetInputDirection() == dir)
        {
            _baseColor = Color.red;
        }
        else if(_ds.GetTile().GetOutputDirection() == dir)
        {
            _baseColor = Color.green;
        }
        else
        {
            _baseColor = Color.white;
        }

        _renderer.color = _baseColor;
    }
}
