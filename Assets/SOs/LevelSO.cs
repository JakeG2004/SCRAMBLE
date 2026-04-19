using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName="Level", menuName="SOs/LevelSO")]
public class LevelSO : ScriptableObject
{
    public List<EggType> eggTypes = new();
    public float levelTime = 120f;
    public float eggSpawnPeriod = 10f;
    public int requiredAmt = 0;
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