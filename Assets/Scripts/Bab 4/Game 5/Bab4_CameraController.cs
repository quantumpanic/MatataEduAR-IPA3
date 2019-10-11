using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_CameraController : MonoBehaviour {

	[Header("Status")]
	public float smoothTime = .15f;
	public float yMaxValue, yMinValue;
	public float xMaxValue, xMinValue;

	[Header("Reference To Another Object")]
	public Transform target;

	[Header("Others")]
	Vector3 velocity = Vector3.zero;

	void FixedUpdate(){
		
		Vector3 targetPos = target.position;

		targetPos.y = Mathf.Clamp (target.position.y + 5, yMinValue, yMaxValue);

		targetPos.x = Mathf.Clamp (target.position.x, xMinValue, xMaxValue);

		targetPos.z = transform.position.z;

		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothTime);
	}


//	FocusArea focusArea;
//
////	public Vector3 offset;
//
//	public Vector2 focusAreaSize;
//
//	void Start(){
//		focusArea = new FocusArea (target.);
//	}
//
//	struct FocusArea{
//		
//		public Vector2 centere;
//		float left, right, top, bottom;
//
//		public FocusArea(Bounds targetBounds, Vector2 size){
//			
//			left = targetBounds.center.x - size.x/2;
//			right = targetBounds.center.x + size.x/2;
//			bottom = targetBounds.min.y;
//			top = targetBounds.min.y + size.y;
//			centere = new Vector2((left + right) / 2, (top + bottom) / 2);
//
//		}
//
//		public void Update(Bounds targetBounds){
//			
//			float shiftX = 0;
//			if(targetBounds.min.x < left){
//				shiftX = targetBounds.min.x - left;
//			}else if(targetBounds.max.x > right){
//				shiftX = targetBounds.max.x - right;
//			}
//
//			left += shiftX;
//			right += shiftX;
//
//			float shiftY = 0;
//			if(targetBounds.min.y < bottom){
//				shiftY = targetBounds.min.y - bottom;
//			}else if(targetBounds.max.y > top){
//				shiftY = targetBounds.max.y - top;
//			}
//
//			bottom += shiftY;
//			top += shiftY;
//
//			centere = new Vector2((left + right) / 2, (top + bottom) / 2);
//		}
//
//	}

	// Use this for initialization
//	void Start () {
//		
//		offset = transform.position - player.transform.position;
//
//	}
//
//	// Update is called once per frame
//	void Update () {
//		
//		transform.position = player.transform.position + offset;
//
//	}
}