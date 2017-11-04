using UnityEngine;
using UnityEditor;
using LowPolyCloudGenerator;

[CustomEditor(typeof(CloudGenerator))]
public class CloudEditor : Editor
{
    CloudGenerator primitive;

    SerializedProperty fill;
    SerializedProperty offset;
    SerializedProperty space;
    SerializedProperty verts;
    SerializedProperty children;

    int seed;
     
    void OnEnable ()
    {
        primitive = (CloudGenerator)target;

        fill = serializedObject.FindProperty("fill");
        offset = serializedObject.FindProperty("addPos");
        space = serializedObject.FindProperty("space");
        verts = serializedObject.FindProperty("vertexCount");
        children = serializedObject.FindProperty("MeshInChildren");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Low Poly Cloud Generator by NoobStudios", EditorStyles.centeredGreyMiniLabel);

        EditorGUILayout.Space();
        

        EditorGUILayout.PropertyField(verts, new GUIContent("Vertices "));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(offset, new GUIContent("Offset "));
      
        if (offset.boolValue == true)
        {
            EditorGUILayout.PropertyField(space, new GUIContent("   Offset Value "));
        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(fill, new GUIContent("Fill "));

        if (!primitive.fill)
        {
            EditorGUILayout.PropertyField(children, new GUIContent("    Mesh in Children "));
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.BeginVertical(EditorStyles.helpBox);

        if (GUILayout.Button("Initialize"))
        {
            primitive.Initialize();
        }

        GUILayout.BeginHorizontal();
        

        if (GUILayout.Button ("Add Sphere"))
        {
            GameObject sphere = new GameObject ("Sphere");

            sphere.transform.position = primitive.transform.position;

            sphere.transform.SetParent(primitive.transform);

            sphere.AddComponent<Primitive>();
            sphere.AddComponent<MeshFilter>();
            sphere.AddComponent<MeshRenderer>();
        }

        if (GUILayout.Button ("Remove Sphere"))
        {
            Primitive[] primitives = primitive.GetComponentsInChildren<Primitive>();

            DestroyImmediate(primitives[primitives.Length - 1].gameObject);
        }

        GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();

      

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        seed = EditorGUILayout.IntField("Seed ", seed);

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Preview"))
        {
            Vector3 pos = primitive.transform.position;
            primitive.transform.position = Vector3.zero;

            primitive.Generate(seed);
            primitive.transform.position = pos;
        }

        if (GUILayout.Button("Preview Random"))
        {
            Vector3 pos = primitive.transform.position;
            primitive.transform.position = Vector3.zero;

            seed = Random.Range(0, 100000);
            primitive.Generate(seed);

            primitive.transform.position = pos;
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Export"))
        {
            primitive.Generate(seed);

            Vector3 pos = primitive.transform.position;
            primitive.transform.position = Vector3.zero;
                
            ObjExporter.Export(primitive.gameObject);

            primitive.transform.position = pos;
        }

        if (GUILayout.Button("Export Random"))
        {
            seed = Random.Range(0, 100000);

            Vector3 pos = primitive.transform.position;
            primitive.transform.position = Vector3.zero;

            primitive.Generate(seed);

            ObjExporter.Export(primitive.gameObject);

            primitive.transform.position = pos;
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        

        serializedObject.ApplyModifiedProperties();   
    }
}
