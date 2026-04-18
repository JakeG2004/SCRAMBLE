using UnityEngine;

public class BoilerTile : MachineTile
{
    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);
        SoundManager.PlayBoiler();
    }
}
