using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSO))]
public class LevelSOEditor : Editor
{
    private LevelSO levelData;
    
    // Serialized Properties for the top fields
    private SerializedProperty eggTypesProp;
    private SerializedProperty levelTimeProp;
    private SerializedProperty eggPeriodProp;
    private SerializedProperty requiredAmtProp;

    private void OnEnable()
    {
        levelData = (LevelSO)target;
        
        // Link the properties
        eggTypesProp = serializedObject.FindProperty("eggTypes");
        levelTimeProp = serializedObject.FindProperty("levelTime");
        eggPeriodProp = serializedObject.FindProperty("eggSpawnPeriod");
        requiredAmtProp = serializedObject.FindProperty("requiredAmt");
    }

    public override void OnInspectorGUI()
    {
        // Start tracking changes for SerializedProperties
        serializedObject.Update();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Level Settings", EditorStyles.boldLabel);

        // 1. Draw the top-level parameters
        EditorGUILayout.PropertyField(levelTimeProp, new GUIContent("Time Limit (sec)"));
        EditorGUILayout.PropertyField(requiredAmtProp, new GUIContent("Required Amount"));
        EditorGUILayout.PropertyField(eggPeriodProp, new GUIContent("Egg Spawn Frequency (sec)"));
        
        EditorGUILayout.Space(5);
        
        // 2. Draw the Egg Types List
        EditorGUILayout.PropertyField(eggTypesProp, new GUIContent("Egg Types Required"), true);

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Level Layout (9x9 Grid)", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("L-Click Name: Change Type | Buttons: Input/Output Dir", MessageType.None);

        // 3. Draw the Visual Grid
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        for (int y = 0; y < 9; y++)
        {
            EditorGUILayout.BeginHorizontal();
            // Center the grid slightly
            GUILayout.FlexibleSpace(); 
            for (int x = 0; x < 9; x++)
            {
                DrawCell(x, y);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        // Apply changes to the SerializedProperties (time, amount, list)
        serializedObject.ApplyModifiedProperties();

        // Manual dirtying for the non-serialized array logic inside DrawCell
        if (GUI.changed)
        {
            EditorUtility.SetDirty(levelData);
        }
    }

    private void DrawCell(int x, int y)
    {
        if (levelData.levelObjects == null || levelData.levelObjects.Length <= y) return;
        
        LevelObject obj = levelData.levelObjects[y].rowObjects[x];

        // Increased size slightly for better visibility of directions
        Rect rect = GUILayoutUtility.GetRect(60, 60, GUILayout.ExpandWidth(false));
        EditorGUI.DrawRect(rect, GetTypeColor(obj.type));

        // Placement for arrows and labels
        Rect inputRect = new Rect(rect.x + 3, rect.y + 3, 22, 22);
        Rect outputRect = new Rect(rect.x + rect.width - 25, rect.y + rect.height - 25, 22, 22);
        Rect labelRect = new Rect(rect.x, rect.y + (rect.height / 2) - 8, rect.width, 18);

        // Input Button (Top-Left)
        if (GUI.Button(inputRect, new GUIContent(GetDirectionArrow(obj.input), "Input Direction"), EditorStyles.miniButton))
        {
            Undo.RecordObject(levelData, "Change Input Direction");
            obj.input = CycleDirection(obj.input);
        }

        // Output Button (Bottom-Right)
        if (GUI.Button(outputRect, new GUIContent(GetDirectionArrow(obj.output), "Output Direction"), EditorStyles.miniButton))
        {
            Undo.RecordObject(levelData, "Change Output Direction");
            obj.output = CycleDirection(obj.output);
        }

        // Type Button
        GUIStyle centeredStyle = new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleCenter };
        if (GUI.Button(labelRect, obj.type.ToString(), centeredStyle))
        {
            CycleType(obj);
        }
        
        // Draw grid border
        Handles.DrawSolidRectangleWithOutline(rect, Color.clear, new Color(0, 0, 0, 0.4f));
    }

    private Direction CycleDirection(Direction current) => (Direction)(((int)current + 1) % 4);

    private void CycleType(LevelObject obj)
    {
        int next = ((int)obj.type + 1) % System.Enum.GetValues(typeof(ObjectType)).Length;
        Undo.RecordObject(levelData, "Change Object Type");
        obj.type = (ObjectType)next;
    }

    private Color GetTypeColor(ObjectType type)
    {
        return type switch
        {
            ObjectType.NOTHING => new Color(0.25f, 0.25f, 0.25f),
            ObjectType.BELT => new Color(0.2f, 0.5f, 0.8f),
            ObjectType.BOILER => new Color(0.8f, 0.3f, 0.2f),
            ObjectType.COOP => new Color(0.8f, 0.7f, 0.2f),
            ObjectType.TABLE => new Color(0.5f, 0.4f, 0.3f),
            ObjectType.SCRAMBLER => new Color(0.6f, 0.2f, 0.8f),
            ObjectType.FRIER => new Color(0.9f, 0.5f, 0.1f),
            ObjectType.SUNNY_SIDE_UPPER => new Color(1f, 0.9f, 0.6f),
            _ => Color.white
        };
    }

    private string GetDirectionArrow(Direction dir)
    {
        return dir switch
        {
            Direction.NORTH => "↑",
            Direction.SOUTH => "↓",
            Direction.EAST => "→",
            Direction.WEST => "←",
            _ => "•"
        };
    }
}