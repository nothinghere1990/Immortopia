using System;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public class CamPosRot
{
    [HorizontalGroup("Position and Rotation")]
    public Vector3 pos, rot;
}