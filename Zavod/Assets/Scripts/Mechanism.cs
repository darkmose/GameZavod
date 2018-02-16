using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    protected int InputCount { get; set; }
    protected string Type { get; set; }
    protected GameObject[] AlignObjects { get; set; }
    public float EnergyCell { get; set; }
    public float EnergyCurrent { get; set; }
}

