using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
	[SerializeField] private Transform targetToFollow;
	[SerializeField] private float objectWidth;
	[SerializeField] private float objectHeight;
	// Start is called before the first frame update
	void Start()
	{
		objectWidth = targetToFollow.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = targetToFollow.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(
			Mathf.Clamp(targetToFollow.position.x, -objectWidth, objectWidth),
			Mathf.Clamp(targetToFollow.position.y, -objectHeight, objectHeight),
			transform.position.z
		);
	}
}

// public class PSBoundariesOrthographic : MonoBehaviour {
//     public Camera MainCamera;
//     private Vector2 screenBounds;
//     private float objectWidth;
//     private float objectHeight;

//     // Use this for initialization
//     void Start () {
//         screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
//         objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
//         objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
//     }

//     // Update is called once per frame
//     void LateUpdate(){
//         Vector3 viewPos = transform.position;
//         viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
//         viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
//         transform.position = viewPos;
//     }
// }
