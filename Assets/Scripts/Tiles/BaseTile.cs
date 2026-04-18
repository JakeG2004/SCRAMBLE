using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField] protected Direction _inputDir;
    [SerializeField] protected Direction _outputDir;

    public virtual void OnGetEgg()
    {

    }

    public virtual void OnClick()
    {
        
    }

    public Direction GetInputDirection()
    {
        return _inputDir;
    }

    public Direction GetOutputDirection()
    {
        return _outputDir;
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
