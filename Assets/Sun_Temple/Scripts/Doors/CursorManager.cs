using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SunTemple{
	
	public class CursorManager : MonoBehaviour {

		public static CursorManager instance;

		public Sprite defaultCursor;
		public Sprite lockedCursor;
		public Sprite doorCursor;
		public Sprite chestCursor;

		public GameObject progress;
		public GameObject progressFill;


		private UnityEngine.UI.Image img;

		private SpriteRenderer SpriteRendererProgress;
		private SpriteRenderer SpriteRendererprogressFill;
		private float progressOrginialPos;
		private float progressOrginialScale;

		void Awake () {
			instance = this;
			img = GetComponent<UnityEngine.UI.Image> ();
			SpriteRendererProgress = progress.GetComponent<SpriteRenderer>();
			SpriteRendererprogressFill = progressFill.GetComponent<SpriteRenderer>();

			progressOrginialPos = SpriteRendererprogressFill.transform.localPosition.x;
			progressOrginialScale = SpriteRendererprogressFill.transform.localScale.x;

			//SetProress(25);
		}



		public void SetCursorToLocked(){
			img.sprite = lockedCursor;
			SpriteRendererProgress.enabled = false;
			SpriteRendererprogressFill.enabled = false;
		}

		public void SetCursorToDoor(){
			img.sprite = doorCursor;
			SpriteRendererProgress.enabled = true;
			SpriteRendererprogressFill.enabled = true;
		}


		public void SetCursorToChest()
		{
			img.sprite = chestCursor;
			SpriteRendererProgress.enabled = true;
			SpriteRendererprogressFill.enabled = true;
		}
		public void SetCursorToDefault(){
			img.sprite = defaultCursor;
			SpriteRendererProgress.enabled = false;
			SpriteRendererprogressFill.enabled = false;
		}

		public void SetProress(float percent)
		{
			//Debug.Log("------------>"+ percent);
			float factor1 = 0.16f;
			float factor2 = 0.0025f;
			SpriteRendererprogressFill.transform.localScale= new Vector3(progressOrginialScale * (percent*factor1), SpriteRendererprogressFill.transform.localScale.y, SpriteRendererprogressFill.transform.localScale.z);
			SpriteRendererprogressFill.transform.localPosition = new Vector3(progressOrginialPos + (percent*factor2), SpriteRendererprogressFill.transform.localPosition.y, SpriteRendererprogressFill.transform.localPosition.z);
		}
		


	}
}
