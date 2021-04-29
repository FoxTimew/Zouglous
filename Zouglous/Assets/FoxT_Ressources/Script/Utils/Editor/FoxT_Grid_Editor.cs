using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Fox_T_Grid))]
public class FoxT_Grid_Editor : Editor
{
	private void OnSceneGUI()
	{
		Fox_T_Grid grid = (Fox_T_Grid)target;

		if (!grid.seeOnGUI) return;

		foreach (FoxT_Node2D node in grid.node)
		{
			if (node.isWalkable())
			{
				Handles.color = Color.green;
			}
			else Handles.color = Color.red;
			Handles.RectangleHandleCap(0, node.WorldPosition(), grid.transform.rotation, grid.nodeDiamater, EventType.Repaint);
		}
	}

	
}
