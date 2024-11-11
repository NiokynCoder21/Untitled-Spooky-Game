using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//	This script handles automatic opening and closing sliding doors
//	It is fired by triggers and the door closes if found no character in the trigger area


//	Door status
public enum DoubleSlidingDoorStatus {
	Closed,
	Open,
	Animating
}

[RequireComponent(typeof(AudioSource))]
public class DoubleSlidingDoorController : MonoBehaviour {

    private DoubleSlidingDoorStatus status = DoubleSlidingDoorStatus.Closed;

    [SerializeField]
    private Transform halfDoorLeftTransform; // Left panel of the sliding door
    [SerializeField]
    public Transform halfDoorRightTransform; // Right panel of the sliding door

    [SerializeField]
    private float slideDistance = 0.88f; // Sliding distance to open each panel of the door

    [SerializeField]
    private float speed = 1f; // Speed for opening and closing the door

    [SerializeField]
    private float closeDelay = 2f; // Delay in seconds before closing the door after it has opened

    private Vector3 leftDoorClosedPosition;
    private Vector3 leftDoorOpenPosition;
    private Vector3 rightDoorClosedPosition;
    private Vector3 rightDoorOpenPosition;

    private int objectsOnDoorArea = 0;
    private float openTime; // To track when the door was opened

    // Sound Fx
    [SerializeField]
    private AudioClip doorOpeningSoundClip;
    [SerializeField]
    public AudioClip doorClosingSoundClip;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        leftDoorClosedPosition = new Vector3(0f, 0f, 0f);
        leftDoorOpenPosition = new Vector3(0f, 0f, slideDistance);

        rightDoorClosedPosition = new Vector3(0f, 0f, 0f);
        rightDoorOpenPosition = new Vector3(0f, 0f, -slideDistance);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == DoubleSlidingDoorStatus.Open && objectsOnDoorArea == 0)
        {
            if (Time.time - openTime >= closeDelay)
            {
                StartCoroutine(CloseDoors());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (status == DoubleSlidingDoorStatus.Closed && objectsOnDoorArea == 0)
        {
            StartCoroutine(OpenDoors());
        }

        if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Characters"))
        {
            objectsOnDoorArea++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Characters"))
        {
            objectsOnDoorArea--;
        }
    }

    IEnumerator OpenDoors()
    {
        if (doorOpeningSoundClip != null)
        {
            audioSource.PlayOneShot(doorOpeningSoundClip, 0.7f);
        }

        status = DoubleSlidingDoorStatus.Animating;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorClosedPosition, leftDoorOpenPosition, t);
            halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorClosedPosition, rightDoorOpenPosition, t);
            yield return null;
        }

        status = DoubleSlidingDoorStatus.Open;
        openTime = Time.time; // Record the time when the door opened
    }

    IEnumerator CloseDoors()
    {
        if (doorClosingSoundClip != null)
        {
            audioSource.PlayOneShot(doorClosingSoundClip, 0.7f);
        }

        status = DoubleSlidingDoorStatus.Animating;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorOpenPosition, leftDoorClosedPosition, t);
            halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorOpenPosition, rightDoorClosedPosition, t);
            yield return null;
        }

        status = DoubleSlidingDoorStatus.Closed;
    }
}
