using UnityEngine;

public class GridMovement2 : MonoBehaviour
{
	[SerializeField]
	private float speed = 3;

	[SerializeField]
	private float rotationSpeed = 1;

	Vector3 rotationUp = new Vector3(0, 0, 0);
	Vector3 rotationRight = new Vector3(0, 90, 0);
	Vector3 rotationDown = new Vector3(0, 180, 0);
	Vector3 rotationLeft = new Vector3(0, 270, 0);

	Vector3 nextPosition;
	Vector3 currentDirection;
	Vector3 destination;

	private bool canMove;
	private bool canRotate;

	void Start()
	{
		currentDirection = rotationUp;
		destination = transform.position;
	}

	private void Update()
	{
		// Čtení vstupu z klávesnice
		int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
		int vertical = (int)(Input.GetAxisRaw("Vertical"));

		// Nastavení proměnných
		if (vertical != 0)
		{
			nextPosition = vertical == 1 ? Vector3.forward : Vector3.back;

			currentDirection = vertical == 1 ? rotationUp : rotationDown;

			canMove = true;
		}
		else if (horizontal != 0)
		{
			nextPosition = horizontal == 1 ? Vector3.right : Vector3.left;

			currentDirection = horizontal == 1 ? rotationRight : rotationLeft;

			canMove = true;
		}
		else
		{
			canMove = false;
		}

		// Zamknutí rotace pokud je hráč mimo pole
		canRotate = destination == transform.position;

		// Rotace hráče
		if (canRotate)
		{
			if ((transform.localEulerAngles != currentDirection))
			{
				float rotSpeed = rotationSpeed * Time.deltaTime * 1000;
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(currentDirection), rotSpeed);
				return;
			}
		}

		// Nastavení pohybu
		if (canMove)
		{
			if (Vector3.Distance(destination, transform.position) <= float.Epsilon && Valid())
			{
				destination = transform.position + nextPosition;
			}
		}

		// Posun hráče
		transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
	}

	private readonly float rayLength = 1f;
	private bool Valid()
	{
		Ray ray = new Ray(transform.position + new Vector3(0, 0, 0.25f), transform.forward);

		if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
		{
			if (hit.collider.CompareTag("Wall"))
			{
				return false;
			}
		}
		return true;
	}
}