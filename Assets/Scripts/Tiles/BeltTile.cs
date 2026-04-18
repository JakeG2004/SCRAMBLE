using UnityEngine;

public class BeltTile : BaseTile
{
    private Animator _anim;

    public override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        UpdateSprite();
    }

    public override void UpdateSprite()
    {
        if(!_lc || !_anim)
        {
            return;
        }

        // Straight through
        if(_lc.FindOppositeDir(_outputDir) == _inputDir)
        {
            _anim.SetBool("isBending", false);

            switch(_inputDir)
            {
                case Direction.NORTH:
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    break;

                case Direction.SOUTH:
                    transform.eulerAngles = new Vector3(0f, 0f, 180f);
                    break;

                case Direction.EAST:
                    transform.eulerAngles = new Vector3(0f, 0f, 90f);
                    break;

                case Direction.WEST:
                    transform.eulerAngles = new Vector3(0f, 0f, -90f);
                    break;
            }
        }

        // Bends
        else
        {
            _anim.SetBool("isBending", true);
            transform.localScale = Vector3.one;

            switch(_inputDir)
            {
                case Direction.NORTH:
                    // Default orientation starts here
                    if(_outputDir == Direction.EAST) {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else if(_outputDir == Direction.WEST) {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        transform.localScale = new Vector3(-1, 1, 1); // Flip X to turn West
                    }
                    break;

                case Direction.SOUTH:
                    // Upside down
                    if(_outputDir == Direction.WEST) {
                        transform.eulerAngles = new Vector3(0, 0, 180f);
                    }
                    else if(_outputDir == Direction.EAST) {
                        transform.eulerAngles = new Vector3(0, 0, 180f);
                        transform.localScale = new Vector3(-1, 1, 1); // Flip X to turn East
                    }
                    break;

                case Direction.EAST:
                    // Rotated 90 degrees clockwise (or -90 / 270)
                    if(_outputDir == Direction.SOUTH) {
                        transform.eulerAngles = new Vector3(0, 0, -90f);
                    }
                    else if(_outputDir == Direction.NORTH) {
                        transform.eulerAngles = new Vector3(0, 0, -90f);
                        transform.localScale = new Vector3(-1, 1, 1); // Flip X to turn North
                    }
                    break;

                case Direction.WEST:
                    // Rotated 90 degrees counter-clockwise
                    if(_outputDir == Direction.NORTH) {
                        transform.eulerAngles = new Vector3(0, 0, 90f);
                    }
                    else if(_outputDir == Direction.SOUTH) {
                        transform.eulerAngles = new Vector3(0, 0, 90f);
                        transform.localScale = new Vector3(-1, 1, 1); // Flip X to turn South
                    }
                    break;
            }
        }
    }
}
