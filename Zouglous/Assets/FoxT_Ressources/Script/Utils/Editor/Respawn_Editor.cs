using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Respawn))]
public class Respawn_Editor : Editor
{
	private void OnSceneGUI()
	{
		Respawn resp = (Respawn)target;
		Handles.color = Color.green;

		Vector3[] vertex =
		{
			new Vector3(resp.cinemachine.m_Lens.OrthographicSize * 16 / 9 + resp.sizeOfRaycast, resp.cinemachine.m_Lens.OrthographicSize + resp.sizeOfRaycast, 0f),
			new Vector3(resp.cinemachine.m_Lens.OrthographicSize * 16 / 9 + resp.sizeOfRaycast, (resp.cinemachine.m_Lens.OrthographicSize + resp.sizeOfRaycast) * -1, 0f),
			new Vector3((resp.cinemachine.m_Lens.OrthographicSize * 16 / 9 + resp.sizeOfRaycast) * -1, (resp.cinemachine.m_Lens.OrthographicSize + resp.sizeOfRaycast) * -1, 0f),
			new Vector3((resp.cinemachine.m_Lens.OrthographicSize * 16 / 9 + resp.sizeOfRaycast) * -1, resp.cinemachine.m_Lens.OrthographicSize + resp.sizeOfRaycast, 0f)
		};

		for (int i = 0; i < vertex.Length; i++)
		{
			Handles.DrawLine(resp.transform.position + vertex[i] * 2, resp.transform.position + vertex[(i + 1) % vertex.Length] * 2);
		}
	}
}
