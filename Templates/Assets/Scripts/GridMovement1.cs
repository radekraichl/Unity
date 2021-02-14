using System;
using UnityEngine;

internal class GridMovement1 : MonoBehaviour
{
	[SerializeField]
	private float rotationSpeed = 5;

	[SerializeField]
	private float movementSpeed = 5;

	private bool isMoving = false;
	private bool isRotate = false;

	private Vector3 targetRotation;
	private Vector3 targetPosition;

	private float remainingTime;
	private int rotationDirection;


	private void Update()
	{
		if (isMoving)
		{
			remainingTime -= Time.deltaTime;

			transform.position += Time.deltaTime * movementSpeed * transform.forward;

			if (remainingTime <= 0)
			{
				transform.position = targetPosition;
				isMoving = false;
			}

			return;
		}

		if (isRotate)
		{
			remainingTime -= Time.deltaTime;

			transform.Rotate(0, Time.deltaTime * rotationSpeed * rotationDirection, 0);

			if (remainingTime <= 0)
			{
				transform.eulerAngles = targetRotation;
				isRotate = false;
			}

			return;
		}

		int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
		int vertical = (int)(Input.GetAxisRaw("Vertical"));

		if (vertical == 1 && horizontal == 0)
		{
			targetPosition = transform.position + transform.forward;
			isMoving = true;
			remainingTime = 1f / movementSpeed;
		}
		else
		if (horizontal != 0)
		{
			rotationDirection = 90 * horizontal;

			targetRotation = transform.rotation.eulerAngles + rotationDirection * Vector3.up;
			isRotate = true;
			remainingTime = 1f / rotationSpeed;
		}

		Vector3 trans = transform.position;
		trans.x = (float)Math.Round(transform.position.x, 1);
		trans.z = (float)Math.Round(transform.position.z, 1);
		transform.position = trans;
	}
}