using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelSO _level;
    [SerializeField] private ObjectsSO _objects;

    [SerializeField] private float _tileGap = 1f;

    void Start()
    {
        if(_level == null)
        {
            Debug.Log("No Level!");
            return;
        }

        CreateLevel();
    }

    // Creates the  grid of tiles
    void CreateLevel()
    {
        for(int i = 0; i < 9; i++)
        {
            Row curRow = _level.levelObjects[i];

            for(int j = 0; j < 9; j++)
            {
                LevelObject curObject = curRow.rowObjects[j];
                GameObject objectToInstance = GetObjectToInstance(curObject.type);

                Vector3 newPos = new Vector3((j - 4) * _tileGap , ((8 - i) - 4) * _tileGap, 0);
                GameObject newObject = Instantiate(objectToInstance, newPos, Quaternion.identity, this.gameObject.transform);

                BaseTile newTile = newObject.GetComponent<BaseTile>();

                newTile.SetInputDirection(curObject.input);
                newTile.SetOutputDirection(curObject.output);

            }
        }
    }

    // Gets the type of tile to instance given the current type
    GameObject GetObjectToInstance(ObjectType curType)
    {
        switch(curType)
        {
            case ObjectType.COOP:
                return _objects.coopTile;

            case ObjectType.BELT:
                return _objects.beltTile;

            case ObjectType.TABLE:
                return _objects.tableTile;

            case ObjectType.BOILER:
                return _objects.boilerTile;

            case ObjectType.SCRAMBLER:
                return _objects.scramblerTile;

            case ObjectType.FRIER:
                return _objects.frierTile;

            case ObjectType.SUNNY_SIDE_UPPER:
                return _objects.sunnySideUpperTile;
        }

        return _objects.baseTile;
    }

    // Prints the text of the tile types
    void PrintLevel()
    {
        foreach(Row row in _level.levelObjects)
        {
            string curRow = "";
            foreach(LevelObject item in row.rowObjects)
            {
                curRow = curRow + item.type + " ";
            }

            Debug.Log(curRow);
        }
    }
}