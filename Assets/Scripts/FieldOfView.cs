using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FieldOfView : MonoBehaviour
{
	
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask Target;
	public LayerMask Obstacles;
	[HideInInspector]
	public float distance;
	
	
	public List<Transform> visibleTargets = new List<Transform>();

	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
	}


	


	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	public void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, Target);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				distance = Vector3.Distance(transform.position, target.position);
				
				
				if (!Physics.Raycast(transform.position, dirToTarget, distance, Obstacles))
				{
					visibleTargets.Add(target);

				}

                
               
			}
		}

	
	}

	public float getDistance()
    {

		return distance;
		
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}