using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : Mechanism {

    InputIO[] io;

    void Start () {
        InputCount = 8;
        io = new InputIO[InputCount];
        Type = "Capsule";
        EnergyCell = 10000f;
        EnergyCurrent = 0;
        io[0] = transform.GetChild(0).GetComponent<InputIO>();
    }

	void Update () {
		
	}
}
