using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScoreData
{
    public int currentPoints;

    public ScoreData(int currentPoints)
    {
        this.currentPoints = currentPoints;
    }
}
