using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField] protected Direction _inputDir;
    [SerializeField] protected Direction _outputDir;
    [SerializeField] protected LevelCreator _lc;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {

    }

    public virtual void OnGetEgg(BaseEgg egg)
    {
        egg.SetCurTile(this);
        egg.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public virtual void OnClick()
    {

    }

    public bool CanMoveEgg()
    {
        return _lc.CanFeed(this, _lc.GetNeighboringTile(_outputDir, this));
    }

    public virtual BaseTile GetOutputTile()
    {
        return _lc.GetNeighboringTile(_outputDir, this);
    }


    public Direction GetInputDirection()
    {
        return _inputDir;
    }

    public Direction GetOutputDirection()
    {
        return _outputDir;
    }

    public void SetLevelCreator(LevelCreator lc)
    {
        _lc = lc;
    }

    public void SetInputDirection(Direction dir)
    {
        _inputDir = dir;
        UpdateSprite();
    }

    public void SetOutputDirection(Direction dir)
    {
        _outputDir = dir;
        UpdateSprite();
    }

    protected void UpdateSprite()
    {

    }
}
