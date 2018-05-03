using UnityEngine;
using System.Collections;

public class PlatformRotation : MonoBehaviour {
    Quaternion transRotate;
	public Transform Platform;
	public float ConvertDelta = 1.3f;
	private Vector2 CenterScreen;
	private Vector2 SavePosition;
	private Vector2 CurrentPosition;
	private bool Pressed = false;
	private float ForPercentage;

	[Header ("Other Settings")]
	public int CountOfFix;
	public Rigidbody2D Player;
	public GameObject FixOn;
	public GameObject ParticleGameOverSaw;

	private Vector2 i;

	void Start(){
        
        CenterScreen = new Vector2(Screen.width / 2,Screen.height / 2);
		ForPercentage = Mathf.Sqrt (Screen.width * Screen.width + Screen.height * Screen.height);
	}
		
	

	void FixedUpdate(){
		if (Pressed) {
			CurrentPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			i = SavePosition - CurrentPosition;
			if (i.x < 0)
				i = new Vector2 (-i.x, i.y);
			if (i.y < 0)
				i = new Vector2 (i.x, -i.y);
			i = new Vector2 (Mathf.Sqrt(i.x * i.x + i.y * i.y) * 100 / (ForPercentage * ConvertDelta), 0);

			if (CurrentPosition.y > CenterScreen.y) {
				if (CurrentPosition.x < SavePosition.x)
					Platform.Rotate (0,0,i.x);
				else
					Platform.Rotate (0,0,-i.x);
			}
			if (CurrentPosition.y < CenterScreen.y) {
				if (CurrentPosition.x > SavePosition.x)
					Platform.Rotate (0, 0, i.x);
				else
					Platform.Rotate (0, 0, -i.x);
			}
			if (CurrentPosition.x > CenterScreen.x) {
				if (CurrentPosition.y > SavePosition.y)
					Platform.Rotate (0,0,i.x);
				else
					Platform.Rotate (0,0,-i.x);
			}
			if (CurrentPosition.x < CenterScreen.x) {
				if (CurrentPosition.y < SavePosition.y)
					Platform.Rotate (0,0,i.x);
				else
					Platform.Rotate (0,0,-i.x);
			}
			SavePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		}
	}

}
