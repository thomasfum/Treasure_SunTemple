using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Management;

namespace SunTemple{


	public class CharController_Motor : MonoBehaviour {

		public float speed = 10.0f;
		public float sensitivity = 60.0f;
		CharacterController character;
		public GameObject cam;
		//float moveFB, moveLR;	
		//float rotHorizontal, rotVertical;
		//public bool webGLRightClickRotation = true;
		//float gravity = -9.8f;
		public TextMeshProUGUI textMeshProUGUI_Debug;
		private Vector2 currentRotation;
		public float maxYAngle = 80f;
		private bool allowMove=true;
		private bool allowMoveNext = false;
		//bool firstMove = true;
		//private bool needPad = false;
		//public Canvas m_Canvas;
		//string debugText;

		Vector3 pOld;

		[SerializeField] private bl_Joystick Joystick;//Joystick reference for assign in inspector
		[SerializeField] private float Speed = 5;

		Quaternion prevRotation;

		void Start(){
			textMeshProUGUI_Debug.text = "";

			character = GetComponent<CharacterController> ();

			//webGLRightClickRotation = false;

			if (Application.platform == RuntimePlatform.WebGLPlayer) {
				//webGLRightClickRotation = true;
				sensitivity = sensitivity * 1.5f;
				speed = speed * 1f;
			}

			if (Application.platform == RuntimePlatform.Android)
			{
				//webGLRightClickRotation = true;
				sensitivity = sensitivity * 0.02f;
				speed = speed * 0.5f; 
				//textMeshProUGUI_Debug.text = "A";
			}
			//firstMove = true;
			//manage unvisible screen part in VR
			if (XRGeneralSettings.Instance != null)
			{
				if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
				{
					textMeshProUGUI_Debug.GetComponent<RectTransform>().localPosition += new Vector3(Screen.width / 30, 0, 0);
				}
			}
		}


		public void AllowMove(bool _allow)
		{
			//allowMove = _allow;
			if (_allow == true)
				allowMoveNext = true;
			else
				allowMove = false;

		}
		float fps = 0;
		void OnGUI()
		{
			fps = (9.0f * fps + 1.0f / Time.deltaTime) / 10.0f;
			//GUI.Label(new Rect(50 + (Screen.width / 30), 50 + (Screen.height / 30), 200, 100), "FPS: " + (int)fps);
			textMeshProUGUI_Debug.text = "FPS: " + (int)fps;
		}

		void FixedUpdate()
		{
			/*
#if UNITY_IOS || UNITY_ANDROID && !UNITY_EDITOR
        if (XRGeneralSettings.Instance != null)
        {
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                Joystick.gameObject.SetActive(false);
                needPad = false;
            }
            else
                needPad = true;
        }
#else
				needPad = true;
#endif
*/
			/*
			if (firstMove == true)
			{
				firstMove = false;
				character.SimpleMove(cam.transform.forward*50);
				Debug.Log("---> First move");
			}
			*/
			//moveFB = Input.GetAxis ("Horizontal") * speed;
			//moveLR = Input.GetAxis ("Vertical") * speed;

			//rotHorizontal = Input.GetAxisRaw ("Mouse X") * sensitivity;
			//rotVertical = Input.GetAxisRaw ("Mouse Y") * sensitivity;


			//------------------------------------------------------------------------------------------------------
			// Rotate camera with mouse or touch

				Vector3 pos = new Vector3();
				pos.x = Input.GetAxis("Mouse X");
				pos.y = Input.GetAxis("Mouse Y");
				if (Input.GetMouseButton(0))
				{
					currentRotation.x += pos.x * sensitivity/20;
					currentRotation.y -= pos.y * sensitivity/20;
					currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
					currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
					transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
				}

			/*
			//------------------------------------------------------------------------------------------------------
			// Rotate camera with pad
			if (needPad == true)
			{


				Vector2 tempVector = Vector2.zero;
#if UNITY_IOS || UNITY_ANDROID && !UNITY_EDITOR
           Vector3 pos = Input.GetTouch(touchID).position;
#else
				Vector3 pos = Input.mousePosition;
#endif
				RectTransformUtility.ScreenPointToLocalPointInRectangle(m_Canvas.transform as RectTransform, pos, m_Canvas.worldCamera, out tempVector);
				Vector3 p= m_Canvas.transform.TransformPoint(tempVector);


				float v = p.y-pOld.y;
				float h = p.x - pOld.x;

				pOld = p;

				//float v = Joystick.Vertical; //get the vertical value of joystick
				//float h = Joystick.Horizontal;//get the horizontal value of joystick
				//Debug.Log("---> " + v + " , " + h);

				currentRotation.x += h * Speed*2;  //1.5 android??
				currentRotation.y -= v * Speed / 1.2f;//1.5 android??
													  //currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
				currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

				//Debug.Log("---> "  + h+ "= "+ currentRotation.x);

				transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
			}
			*/

			
			if (allowMoveNext == true)
			{
				if (prevRotation != cam.transform.rotation)
				{
					allowMove = true;
					allowMoveNext = false;
				}
			}
			
			prevRotation = cam.transform.rotation;

			float angle = cam.transform.rotation.eulerAngles.x;
			if ((angle > 8) && (angle < 38))
			{
				Vector3 dir=new Vector3();
				if ((angle > 8) && (angle < 8+15))
					dir = (cam.transform.forward * angle*speed / 50);
				if ((angle >= 8+15) && (angle < 38))
					dir = (cam.transform.forward * (38-angle) * speed / 50);
				dir.y = 0;

				if(allowMove== true)
					character.SimpleMove(dir);
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
			/*
			if (Input.GetMouseButton(1))
			{
				currentRotation.x += Input.GetAxis("Mouse X") * sensitivity/50;
				currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity/50;
				currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
				currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
				transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
			}
			*/


			
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