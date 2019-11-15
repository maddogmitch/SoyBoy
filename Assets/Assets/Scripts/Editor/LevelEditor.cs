using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
   public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Save Level"))
        {
        }
        Level level = (Level)target;
        level.transform.position = Vector3.zero;
        level.transform.rotation = Quaternion.identity;

        var levelRoot = GameObject.Find("Level");
        var ldr = new LevelDataRepresentation();
        var levelItems = new List<LevelItemRepresentation>();

        foreach(Transform t in levelRoot.transform)
        {
            var sr = t.GetComponent<SpriteRenderer>();
            var li = new LevelItemRepresentation();
            {
                position = t.position;
                rotation = t.rotation;
            };

            if(t.name.Contains(" "))
            {
                li.prefabName = t.name.Substring(0, t.name.IndexOf(" "));
            }
            else
            {
                li.prefabName = t.name;
            }

            if(sr != null)
            {
                li.spriteLayer = sr.sortingLayerName;
                li.spriteColor = sr.color;
                li.spriteOrder = sr.sortingOrder;
            }

            levelItems.Add(li);
        }
    }
}
