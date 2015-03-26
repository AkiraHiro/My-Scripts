using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	bool isLocked;
	void Start () {
		SetCursorLock(true);	
	}

	void SetCursorLock(bool isLocked)
	{
		this.isLocked = isLocked;
		Screen.lockCursor = isLocked;
		Screen.showCursor = !isLocked;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
			SetCursorLock(!isLocked);

	    if (Input.GetKeyDown (KeyCode.L))
			SetCursorLock (true);
	}
}