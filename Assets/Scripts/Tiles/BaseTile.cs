using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField] protected Direction _inputDir;
    [SerializeField] protected Direction _outputDir;
    [SerializeField] protected LevelCreator _lc;
    protected ObjectType _objectType;
    protected bool _hasEgg = false;

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public void SetObjectType(ObjectType newType)
    {
        _objectType = newType;
    }

    public ObjectType GetObjectType()
    {
        return _objectType;
    }

    public virtual void OnGetEgg(BaseEgg egg)
    {
        if(this is not TableTile)
        {
            _hasEgg = true;
        }

        egg.SetCurTile(this);
        egg.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        PlaySound();
    }

    public virtual void OnRemoveEgg()
    {
        _hasEgg = false;
    }

    public bool HasEgg()
    {
        return _hasEgg;
    }

    private void OnMouseOver()
    {
        OnHoverOver();
    }

    private void OnMouseExit()
    {
        OnHoverExit();
    }

    public virtual void OnHoverExit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public virtual void OnHoverOver()
    {
        transform.GetChild(0).gameObject.SetActive(true);
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

    public virtual void UpdateSprite()
    {

    }

    public virtual void PlaySound()
    {

    }
}
