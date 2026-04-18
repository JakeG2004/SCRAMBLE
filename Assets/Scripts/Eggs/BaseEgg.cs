using UnityEngine;

public class BaseEgg : MonoBehaviour
{
    [SerializeField] private float _movementPeriod = 1f;
    private float _elapsedTime = 0f;
    [SerializeField] private BaseTile _curTile;

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
