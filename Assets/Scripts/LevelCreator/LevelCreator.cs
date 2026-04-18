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

    // Gets the neighboring tile given a direction and a tile. Returns null if invalid
    public BaseTile GetNeighboringTile(Direction dir, BaseTile tile)
    {
        // Calculate an offset based on dir and tile
        int offset = 0;
        int curIdx = tile.transform.GetSiblingIndex();
        switch (dir)
        {
            case Direction.NORTH:
                offset = -9;
                break;

            case Direction.SOUTH:
                offset = 9;
                break;

            case Direction.EAST:
                offset = 1;
                break;

            case Direction.WEST:
                offset = -1;
                break;

            default:
                return null;
        }

        // Check bounds
        int newIdx = curIdx + offset;
        if(newIdx < 0 || newIdx > 80)
        {
            return null;
        }

        Transform newTile = tile.transform.parent.GetChild(newIdx);

        return newTile.GetComponent<BaseTile>(); 
    }

    public bool CanFeed(BaseTile src, BaseTile dst)
    {
        if(src == null || dst == null)
        {
            return false;
        }

        if(src.GetOutputDirection() != FindOppositeDir(dst.GetInputDirection()))
        {
            return false;
        }

        return true;
    }

    private Direction FindOppositeDir(Direction dir)
    {
        switch (dir)
        {
            case Direction.NORTH:
                return Direction.SOUTH;

            case Direction.SOUTH:
                return Direction.NORTH;

            case Direction.EAST:
                return Direction.WEST;

            case Direction.WEST:
                return Direction.EAST;
        }

        return Direction.NORTH;
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
                newTile.SetLevelCreator(this);

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