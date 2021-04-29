using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Respawn : MonoBehaviour
{
	public CinemachineVirtualCamera cinemachine;

	[SerializeField]
	private float checkDelay;

	public float sizeOfRaycast;

	[HideInInspector]
	public GameObject enemy;

	[SerializeField]
	private LayerMask pcLayer;
	private void Awake()
	{
		cinemachine = GameObject.Find("Virtual_Cam").GetComponent<CinemachineVirtualCamera>();
	}

	public void RespawnEvent()
	{
		StartCoroutine(PlayerPositionCheck());
	}

	private IEnumerator PlayerPositionCheck()
	{
		while (Physics2D.BoxCast(transform.position, new Vector2(cinemachine.m_Lens.OrthographicSize * 16 / 9 + sizeOfRaycast, cinemachine.m_Lens.OrthographicSize + sizeOfRaycast) * 4, 0f, Vector2.up, 0f, pcLayer))
		{
			//RaycastHit2D box = ;
			//if (box.collider != null) 
				Debug.Log("Le joueur est là");
			yield return new WaitForSeconds(checkDelay);
		}
		Debug.Log("Respawn");
		enemy.SetActive(true);
	}
}
