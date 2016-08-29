﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	public float interpVelocity = 1.0f;
	public float minDistance = 2.0f;
	public float followDistance = 1.0f;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;

	void Start () {
		targetPos = transform.position;
	}

	void FixedUpdate () {
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			
			Vector3 targetDirection = (target.transform.position - posNoZ);
			
			interpVelocity = targetDirection.magnitude * 5f;
			
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
			
			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);
			
		}
	}

}
