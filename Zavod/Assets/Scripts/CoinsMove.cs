using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsMove : MonoBehaviour {

	void FixedUpdate () {
        if (Mathf.Abs(GameObject.Find("Player").transform.position.x-transform.position.x)<10f)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Player").transform.position+transform.up, Time.deltaTime * (10/Mathf.Abs(GameObject.Find("Player").transform.position.x - transform.position.x)));
        }
		
	}
}
