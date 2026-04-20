using UnityEngine;

public class CoopTile : BaseTile
{
    [SerializeField] private float _spawnPeriod = 1f;
    [SerializeField] private GameObject _egg;

    [SerializeField] private float _elapsedTime = 0f;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        _elapsedTime += Time.deltaTime;
        if((_elapsedTime >= _spawnPeriod))
        {
            SpawnEgg();
            _elapsedTime = 0f;
        }
    }

    public override void OnHoverOver()
    {
        
    }

    public void SetSpawnPeriod(float period)
    {
        _spawnPeriod = period;
        _elapsedTime = _spawnPeriod;
    }

    void SpawnEgg()
    {
        BaseTile spawnDestination = _lc.GetNeighboringTile(_outputDir, this);
        if(spawnDestination == null || spawnDestination.HasEgg())
        {
            return;
        }

        SoundManager.PlayCoop();
        BaseEgg newEgg = Instantiate(_egg, spawnDestination.transform.position, Quaternion.identity).GetComponent<BaseEgg>();
        spawnDestination.OnGetEgg(newEgg);
    }
}
