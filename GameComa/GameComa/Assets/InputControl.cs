using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

    public Camera MainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileUIAlt");
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileUIAlt"));
        }

        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileUI");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileUI"));
        }
    }
}
