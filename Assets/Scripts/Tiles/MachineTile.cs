using UnityEngine;

public class MachineTile : BaseTile
{
    [SerializeField] private EggType _eggType;

    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);
        egg.SetEggType(_eggType);
    }
}
