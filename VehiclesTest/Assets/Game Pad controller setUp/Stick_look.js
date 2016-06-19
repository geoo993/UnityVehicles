#pragma strict

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
@AddComponentMenu("Camera-Control/Mouse Look")

public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
var axes : RotationAxes = RotationAxes.MouseXAndY;
var sensitivityX : float = 15;
var sensitivityY : float = 15;

var minimumX : float = -360;
var maximumX :float = 360;

var minimumY: float  = -60;
var maximumY : float = 60;

var rotationY : float = 0;

function Update ()
{
	if (axes == RotationAxes.MouseXAndY)
	{
		var rotationX : float = transform.localEulerAngles.y + Input.GetAxis("Mouse XX") * sensitivityX;
		
		rotationY += Input.GetAxis("Mouse YY") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
	else if (axes == RotationAxes.MouseX)
	{
		transform.Rotate(0, Input.GetAxis("Mouse XX") * sensitivityX, 0);
	}
	else
	{
		rotationY += Input.GetAxis("Mouse YY") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
	}
	
	
	// press JoystickButton9 = R3 , to reset rotationY.
	if (Input.GetKey(KeyCode.JoystickButton9))
	{			
		rotationY = 0;		
	}	
}
	
function Start ()
{
		// Make the rigid body not change rotation
	if (GetComponent.<Rigidbody>())
	{
		GetComponent.<Rigidbody>().freezeRotation = true;
	}
}