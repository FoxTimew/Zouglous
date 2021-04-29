using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFieldOfView : MonoBehaviour
{
	private BoxCollider2D box;

	[SerializeField]
	private CinemachineVirtualCamera cinemachine;

	[SerializeField]
	private float fieldOfViewSize;
	private void Awake()
	{
		box = GetComponent<BoxCollider2D>();

		cinemachine = GameObject.Find("Virtual_Cam").GetComponent<CinemachineVirtualCamera>();
		box.size = new Vector2(cinemachine.m_Lens.OrthographicSize * 16 / 9 + fieldOfViewSize, cinemachine.m_Lens.OrthographicSize + fieldOfViewSize) * 2;

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Enemy" && collision.GetComponent<EnemyPathAI>().enabled == true)
		{
			collision.GetComponent<EnemyPathAI>().BackToPatrol();
		}
	}
}
