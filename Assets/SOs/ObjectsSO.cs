using UnityEngine;

[CreateAssetMenu(fileName="Objects", menuName="SOs/ObjectsSO")]
public class ObjectsSO : ScriptableObject
{
    // Basic tiles
    public GameObject baseTile;
    public GameObject coopTile;
    public GameObject beltTile;
    public GameObject tableTile;

    // Machine tiles
    public GameObject boilerTile;
    public GameObject frierTile;
    public GameObject scramblerTile;
    public GameObject sunnySideUpperTile;
}