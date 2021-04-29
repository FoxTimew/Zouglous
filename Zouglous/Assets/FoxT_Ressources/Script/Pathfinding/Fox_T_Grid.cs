using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathExtension;

public class Fox_T_Grid : MonoBehaviour
{
	public bool seeOnGUI = true;

	public int sizeX, sizeY;

	public float nodeDiamater;

	[HideInInspector]
	public FoxT_Node2D[,] node;

	[SerializeField]
	private LayerMask obstacle;

	private void Awake()
	{
		CreateGrid();
	}

	private void CreateGrid()
	{
		node = new FoxT_Node2D[sizeX, sizeY];

		Vector3 worldBottomLeft = this.transform.position - Vector3.right * nodeDiamater / 2 - Vector3.up * nodeDiamater / 2;

		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				bool walkable;
				Vector3 nodePosition = worldBottomLeft + new Vector3(nodeDiamater * x, nodeDiamater * y, 0f);

				RaycastHit2D hit = Physics2D.BoxCast(nodePosition, new Vector2(nodeDiamater / 2, nodeDiamater / 2), 0f, Vector2.up, 0f, obstacle);
				if (hit.collider != null)
				{
					walkable = false;
				}
				else walkable = true;

				node[x, y] = new FoxT_Node2D(walkable, nodePosition, x, y);
			}
		}
	}
}
