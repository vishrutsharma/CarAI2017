using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai : MonoBehaviour {

	private List <Transform> nodes= new List<Transform>();
	public Color col;

	void OnDrawGizmos()
	{
		Gizmos.color = col;

		Transform[] pathnodes = GetComponentsInChildren <Transform>();
		nodes = new List<Transform> ();

		for (int i = 0; i < pathnodes.Length; i++) {
		
			if (pathnodes [i] != transform) {
				nodes.Add (pathnodes [i]);
			}
		
		
		}

		for (int j = 0; j < nodes.Count; j++) {
		
			Vector3 nodeelement = nodes [j].position;
			Vector3 prev = Vector3.zero;

			if (j > 0) {
			
			
				prev = nodes [j - 1].position;

			
			}

			else if (j == 0 && nodes.Count > 1) {
				prev = nodes [nodes.Count - 1].position;
			
			
			}

			Gizmos.DrawLine (prev, nodeelement);
			Gizmos.DrawWireSphere (nodeelement, 0.5f);

		
		
		
		
		}






	}

	}
