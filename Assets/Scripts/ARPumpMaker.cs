using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace UnityARInterface
{
	public class ARPumpMaker : ARBase {

		public GameObject pumpPrefab;
		public float createHeight;
		
		private MaterialPropertyBlock props;
		private bool firstClick = true;

		// Use this for initialization
		void Start () {
			props = new MaterialPropertyBlock ();

		}

		void CreatePump(Vector3 atPosition)
		{
			GameObject pumpGameObject = Instantiate (pumpPrefab, atPosition, Quaternion.identity);
				
			firstClick = false;
			/*
			var arInterface = ARInterface.GetInterface();
				if(arInterface!=null)
				arInterface.StopService();*/
			float r = Random.Range(0.0f, 1.0f);
			float g = Random.Range(0.0f, 1.0f);
			float b = Random.Range(0.0f, 1.0f);

			props.SetColor("_InstanceColor", new Color(r, g, b));

			MeshRenderer renderer = pumpGameObject.GetComponent<MeshRenderer>();
			renderer.SetPropertyBlock(props);

			
		}

		// Update is called once per frame
		void Update () {
			if (!isActiveAndEnabled)
				return;
			
			if (Input.GetMouseButton (0)) {
				if (firstClick) {
					var camera = GetCamera ();

					Ray ray = camera.ScreenPointToRay (Input.mousePosition);

					int layerMask = 1 << LayerMask.NameToLayer ("ARGameObject"); // Planes are in layer ARGameObject

					RaycastHit rayHit;
					if (Physics.Raycast (ray, out rayHit, float.MaxValue, layerMask)) {
						Vector3 position = rayHit.point;
						CreatePump (new Vector3 (position.x, position.y + createHeight, position.z));
					}
					
				}
			}
			/*else {
				firstClick = true;
			}*/

		}

	}
}