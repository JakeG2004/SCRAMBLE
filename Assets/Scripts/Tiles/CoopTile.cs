using UnityEngine;

public class CoopTile : BaseTile
{
    [SerializeField] private float _spawnPeriod = 1f;
    [SerializeField] private int _amtToSpawn = 1;
    [SerializeField] private GameObject _egg;

    private float _elapsedTime = 0f;
    private int _amtSpawned = 0;

    public override void Start()
    {
        base.Start();
        _elapsedTime = _spawnPeriod;
    }

    public override void Update()
    {
        base.Update();

        _elapsedTime += Time.deltaTime;
        if((_elapsedTime >= _spawnPeriod) && (_amtSpawned < _amtToSpawn))
        {
            SpawnEgg();
            _elapsedTime = 0f;
        }
    }

    public void SetSpawnPeriod(float period)
    {
        _spawnPeriod = period;
    }

    void SpawnEgg()
    {
        BaseTile spawnDestination = _lc.GetNeighboringTile(_outputDir, this);
        if(spawnDestination == null || !_lc.CanFeed(this, spawnDestination))
        {
            return;
        }

        SoundManager.PlayCoop();
        //_amtSpawned++;
        BaseEgg newEgg = Instantiate(_egg, spawnDestination.transform.position, Quaternion.identity).GetComponent<BaseEgg>();
        spawnDestination.OnGetEgg(newEgg);
    }
}
