using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxT_Pumpkin_Attack : MonoBehaviour
{
	public float attackRadius;
	public Vector2 boxColliderSize;
	[HideInInspector]
	public bool upPosition { get { return this.GetComponent<FoxT_Pumpkin_Controller>().upDirection; } }

	[SerializeField]
	private LayerMask pcLayer;

	private void Awake()
	{
		if (attackRadius > boxColliderSize.x)
		{
			boxColliderSize.x = attackRadius;
			Debug.LogWarning("The Damage Radius variable is set to " + boxColliderSize.x + " because the Attack Radius variable can not be above Damage radius on x axe.");
		}
		if (attackRadius > boxColliderSize.y)
		{
			boxColliderSize.y = attackRadius;
			Debug.LogWarning("The Damage Radius variable is set to " + boxColliderSize.y + " because the Attack Radius variable can not be above Damage radius on y axe.");
		}
	}

	public void Attack(int damage)
	{
		Vector3 position;
		if (!upPosition)
		{
			position = transform.position - new Vector3(0f, .75f, 0f);
		}
		else position = transform.position + new Vector3(0f, .15f, 0f);

		RaycastHit2D hit = Physics2D.BoxCast(position, boxColliderSize, 0f, Vector2.up, 0f, pcLayer);
		if (hit.collider != null)
		{
			hit.collider.GetComponent<FoxT_Controller>().TakeDamage(damage);
		}
	}
}
