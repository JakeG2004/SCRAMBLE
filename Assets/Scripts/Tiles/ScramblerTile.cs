using UnityEngine;

public class ScramblerTile : MachineTile
{
    public override void OnGetEgg(BaseEgg egg)
    {
        base.OnGetEgg(egg);
        SoundManager.PlayScrambler();
    }
}
