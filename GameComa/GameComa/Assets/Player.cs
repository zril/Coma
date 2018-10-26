using System.Collections;
using System.Collections.Generic;
using Coma.Common;
using Coma.Common.Parameter;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public int playerId;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.Instance.PlayerId == playerId){

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Up, true));
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Down, true));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Right, true));
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Left, true));
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Up, false));
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Down, false));
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Right, false));
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Global.Instance.SendCommand(new MoveParam(Direction.Left, false));
            }

        }
	}
}
