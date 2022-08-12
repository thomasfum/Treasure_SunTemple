using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
	
	namespace SunTemple{


	public class CharController_Motor : MonoBehaviour {

		public float speed = 10.0f;
		public float sensitivity = 60.0f;
		CharacterController character;
		public GameObject cam;
		float moveFB, moveLR;	
		float rotHorizontal, rotVertical;
		public bool webGLRightClickRotation = true;
		float gravity = -9.8f;
		public TextMeshProUGUI textMeshProUGUI_Debug;
		private Vector2 currentRotation;
		public float maxYAngle = 80f;
		private bool allowMove=true;
		//string debugText;


		void Start(){
			textMeshProUGUI_Debug.text = "";

			character = GetComponent<CharacterController> ();

			webGLRightClickRotation = false;

			if (Application.platform == RuntimePlatform.WebGLPlayer) {
				webGLRightClickRotation = true;
				sensitivity = sensitivity * 1.5f;
			}

			if (Application.platform == RuntimePlatform.Android)
			{
				//webGLRightClickRotation = true;
				sensitivity = sensitivity * 0.05f;
				speed = speed * 0.5f; 
				//textMeshProUGUI_Debug.text = "A";
			}
			


		}


		public void AllowMove(bool _allow)
		{
			allowMove = _allow;
		}


		void FixedUpdate(){
			
			moveFB = Input.GetAxis ("Horizontal") * speed;
			moveLR = Input.GetAxis ("Vertical") * speed;

			rotHorizontal = Input.GetAxisRaw ("Mouse X") * sensitivity;
			rotVertical = Input.GetAxisRaw ("Mouse Y") * sensitivity;


			
			float angle = cam.transform.rotation.eulerAngles.x;
			if ((angle > 8) && (angle < 35))
			{
				//moveLR = angle *speed / 50;
				//textMeshProUGUI_Debug.text = "A=" + angle + " FB="+ moveFB + " LR=" + moveLR;
				Vector3 dir = (cam.transform.forward * angle*speed / 50);
				dir.y = 0;
				//transform.position += dir;
				if(allowMove== true)
					character.Move(dir * Time.fixedDeltaTime);
			}
			else
			{
				//textMeshProUGUI_Debug.text = "S=" + angle + " FB=" + moveFB+ " LR=" + moveLR;
			}
				
			/*
			Vector3 movement = new Vector3 (moveFB, gravity, moveLR);

			if (webGLRightClickRotation) {
				if (Input.GetKey (KeyCode.Mouse0)) {
					CameraRotation (cam, rotHorizontal, rotVertical);
				}
			} else if (!webGLRightClickRotation) {
				CameraRotation (cam, rotHorizontal, rotVertical);
			}

			movement = transform.rotation * movement;
			character.Move (movement * Time.fixedDeltaTime);
			*/

			//------------------------------------------------------------------------------------------------------
			// Rotate camera with mouse in unity editor

			if (Input.GetMouseButton(1))
			{
				currentRotation.x += Input.GetAxis("Mouse X") * sensitivity/50;
				currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity/50;
				currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
				currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
				transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
			}


			
		}




		/*
	

		void CameraRotation(GameObject cam, float rotHorizontal, float rotVertical){	

			transform.Rotate (0, rotHorizontal * Time.fixedDeltaTime, 0);
			cam.transform.Rotate (-rotVertical * Time.fixedDeltaTime, 0, 0);



			if (Mathf.Abs (cam.transform.localRotation.x) > 0.7) {

				float clamped = 0.7f * Mathf.Sign (cam.transform.localRotation.x); 

				Quaternion adjustedRotation = new Quaternion (clamped, cam.transform.localRotation.y, cam.transform.localRotation.z, cam.transform.localRotation.w);
				cam.transform.localRotation = adjustedRotation;
			}


		}

		*/


	}



}