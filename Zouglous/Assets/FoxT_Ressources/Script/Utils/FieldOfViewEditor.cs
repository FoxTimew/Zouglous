using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathExtension;
using UnityEditor;

[CustomEditor (typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		FieldOfView fow = (FieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);

		Vector2 viewAngleA = fow.DirFromAngle(-fow.viewAngle/2, false);
		Vector2 viewAngleB = fow.DirFromAngle(fow.viewAngle/2, false);

		Handles.DrawLine(fow.transform.position, fow.transform.position + Mathe.Vector2To3(viewAngleA) * fow.viewRadius);
		Handles.DrawLine(fow.transform.position, fow.transform.position + Mathe.Vector2To3(viewAngleB) * fow.viewRadius);

		Handles.color = Color.red;
		if (fow.PlayerIsVisible)
		{
			Handles.DrawLine(fow.transform.position, fow.playableCharacter.position);
		}
	}
}
