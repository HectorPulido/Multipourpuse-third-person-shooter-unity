using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject crossHair;
	public bool lockCursor;

    public bool Aim;

	public float yaw;
	public float pitch;
	[Range(0.1f,5)]
	public float mousePitchSensitivity = 2;
	[Range(0.1f,5)]
	public float mouseYawSensitivity = 2;
	public Vector2 pitchMinMax = new Vector2 (-30, 60);

	[Range(0,10)]
	public float playerModelOffset = 5;
	public float rotationSmoothTime = 0.2f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	public Transform target;
	public Transform spine;
	public float distFromTarget = 30;
	public float zoomDist = 10;
	public float originalDist;
	public float time = 0.25f;
	public float zoomTime = 0.5f;
	Vector3 vel;
	private int screenCount = 0;

	void Start() 
	{
		originalDist = distFromTarget;

        Cursor.visible = false;
		if (lockCursor)
        {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

    RaycastHit rh;
    public Vector3 CameraForward()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rh, 100))
            return rh.point;
        return transform.forward * 100;
    }

    void LateUpdate ()
    {
		//Rotate Camera
		yaw += InputManager.HorizontalMouse * mouseYawSensitivity;
		pitch += InputManager.VerticalMouse * mousePitchSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, 
                                                new Vector3 (pitch, yaw), 
                                                ref rotationSmoothVelocity, 
                                                rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * distFromTarget;

		if (Aim) {
			rotationSmoothTime = 0;
			distFromTarget = Mathf.SmoothStep (distFromTarget, zoomDist, zoomTime);
			transform.position = target.position - transform.forward * distFromTarget + transform.right * playerModelOffset;
            crossHair.SetActive(true);
        } else {
			rotationSmoothTime = 0.2f;
			pitchMinMax = new Vector2 (-30, 60);
			distFromTarget = Mathf.SmoothStep (distFromTarget, originalDist, zoomTime);
			crossHair.SetActive (false);
		}
	}
}
