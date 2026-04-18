using UnityEngine;

public class SunnySideUpperTile : MachineTile
{
    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);
        SoundManager.PlaySunny();
    }
}
