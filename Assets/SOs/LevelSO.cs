using UnityEngine;

[CreateAssetMenu(fileName="Level", menuName="SOs/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int levelIndex = 0;
    public Row[] levelObjects = new Row[9];
}

[System.Serializable]
public class Row
{
    public LevelObject[] rowObjects = new LevelObject[9];
}

[System.Serializable]
public class LevelObject
{
    public ObjectType type;
    public Direction input;
    public Direction output;
}

public enum ObjectType { NOTHING, COOP, TABLE, BELT, BOILER, SCRAMBLER, FRIER, SUNNY_SIDE_UPPER }
public enum Direction { NORTH, SOUTH, EAST, WEST }