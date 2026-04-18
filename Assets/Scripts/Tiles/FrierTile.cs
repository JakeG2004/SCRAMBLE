using UnityEngine;

public class FrierTile : MachineTile
{
    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);
        SoundManager.PlayFrier();
    }
}
