using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoxT_Pumpkin_Attack))]
public class FoxT_Pumpkin_Attack_Editor : Editor
{
	private void OnSceneGUI()
	{
		FoxT_Pumpkin_Attack att = (FoxT_Pumpkin_Attack)target;

		Vector3 position;

		if (!att.upPosition)
		{
			position = att.transform.position - new Vector3(0f, .75f, 0f);
		}
		else position = att.transform.position + new Vector3(0f, .15f, 0f);

		Handles.color = Color.red;

		Vector3[] vertex =
		{
			new Vector3 (position.x - att.boxColliderSize.x / 2, position.y - att.boxColliderSize.y / 2, position.z),
			new Vector3 (position.x - att.boxColliderSize.x / 2, position.y + att.boxColliderSize.y / 2, position.z),
			new Vector3 (position.x + att.boxColliderSize.x / 2, position.y + att.boxColliderSize.y / 2, position.z),
			new Vector3 (position.x + att.boxColliderSize.x / 2, position.y - att.boxColliderSize.y / 2, position.z)
		};

		Handles.DrawLine(vertex[0], vertex[1]);
		Handles.DrawLine(vertex[1], vertex[2]);
		Handles.DrawLine(vertex[2], vertex[3]);
		Handles.DrawLine(vertex[3], vertex[0]);

		Handles.color = Color.yellow - new Color(0f, 0f, 0f, 0.75f);
		Handles.DrawWireArc(att.transform.position, Vector3.forward, Vector3.up, 360, att.attackRadius);
	}
}
