using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

    public Camera MainCamera;
    public float CameraSpeed;

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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MainCamera.transform.position += new Vector3(0, -1) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MainCamera.transform.position += new Vector3(0, 1) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MainCamera.transform.position += new Vector3(-1, 0) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MainCamera.transform.position += new Vector3(1, 0) * Time.deltaTime * CameraSpeed;
        }

    }
}
