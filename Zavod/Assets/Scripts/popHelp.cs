using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popHelp : MonoBehaviour {

    public GameObject cel;

	void Start () {
		
	}

    public void DelPopup()
    {
        if (cel.GetComponent<InvButtons>().isEvent)
        {
            Destroy(this.gameObject);
            cel.GetComponent<InvButtons>().isEvent = false;
        }
    }


}
