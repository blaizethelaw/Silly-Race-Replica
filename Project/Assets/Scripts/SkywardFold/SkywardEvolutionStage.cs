using System;
using UnityEngine;

[Serializable]
public class SkywardEvolutionStage
{
    public string stageName;
    public string tierName;
    [TextArea] public string description;
    public GameObject modelPrefab;
    public float modelScale = 1f;
    public bool enablesAutoFire;
    public bool hasSwingWingAnimation;
    public bool hasDroneWingmen;
    public bool hasPlasmaTrail;
}
