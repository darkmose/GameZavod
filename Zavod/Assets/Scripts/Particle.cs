using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

	void Update () {
        Vector2 vector = transform.position;
        vector.x= GameObject.Find("Player").transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, vector, Time.deltaTime*20);
    }
}
