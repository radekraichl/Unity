using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {


    public AudioClip canHitSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {	
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {

                Debug.DrawLine (ray.origin, hit.point);

                if (hit.transform.tag == "ShootingObject")
                {
                    audioSource.pitch = Random.Range(0.9f, 1.3f);
                    audioSource.Play();
                    GameManager.SP.RemoveObject(hit.transform);
                }else if(hit.transform.tag == "Can"){
                    audioSource.PlayOneShot(canHitSound);

                    Vector3 explosionPos = transform.position;
                    hit.rigidbody.AddExplosionForce(5000, explosionPos, 25.0f, 1.0f);
                }

            }
        }
	}
}
