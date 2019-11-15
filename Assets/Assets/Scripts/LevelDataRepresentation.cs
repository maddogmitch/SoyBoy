using System;
using UnityEngine;

[Serializable]
public class LevelDataRepresentation
{
    public Vector2 playerStartPosition;
    public CameraSettingsRepresntation cameraSettings;
    public LevelItemRepresentation[] levelItems;
}
