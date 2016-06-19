#pragma strict

//*********************************** Character control ****************************************************************
var speed : float = 6.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;
private var moveDirection : Vector3 = Vector3.zero; 

var run : boolean = false;

var runSpeed : float = 4;       

//*********************************** Buttons booleans *****************************************************************
private var toggleABT = false;
private var toggleBBT = false;
private var toggleXBT = false;
private var toggleYBT = false;
private var toggleLBBT = false;
private var toggleRBBT = false;
private var toggleBackSelectBT = false;
private var toggleStartBT = false;
private var toggleL3BT = false;
private var toggleR3BT = false;

private var toggleLTAxis = false;
private var toggleRTAxis = false;

private var UpDownAxis = false;
private var LeftRightAxis = false;

//********************************** GUI staff *************************************************************************
var hudText : GUIStyle; 



function Start ()
{ 
  
}

function Update () {		
	
	// Buttons controller	
    ButtonsControl ();   						
	
	var controller : CharacterController = GetComponent(CharacterController);
	
    if (controller.isGrounded)
    {
       	// We are grounded, so recalculate
        // move direction directly from axes
        moveDirection = Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        
        if (Input.GetKey (KeyCode.Joystick1Button0))
        {        	
            moveDirection.y = jumpSpeed;
            
            toggleABT = true;
        }
        else
        {
        	toggleABT = false;        	
        }
        
        if ((Input.GetAxis("Horizontal2")) && (Input.GetKey (KeyCode.Joystick1Button8)))
        {
        	moveDirection *= runSpeed;
        	moveDirection.y = 0;
        }       
     }
     // Apply gravity
     moveDirection.y -= gravity * Time.deltaTime;
        
    // Move the controller
    controller.Move(moveDirection * Time.deltaTime);
}

function OnGUI ()
{
	//******************************************** GUI Information controls ***************************************************
	GUI.Label(Rect(10,290,150,30),"Button A, Jump" ,hudText);
	GUI.Label(Rect(10,310,150,30),"Button L3 + Movement = Run" ,hudText);
	GUI.Label(Rect(10,330,150,30),"Button R3, Reset look cam" ,hudText);
	GUI.Label(Rect(10,350,150,30),"Button R-Stick, look around" ,hudText);
	GUI.Label(Rect(10,370,150,30),"Button L-Stick, Movement" ,hudText);
	GUI.Label(Rect(10,390,150,30),"Turn [Mode] Button on, Movement control, L-stick is inverted" ,hudText);
	
	//******************************************** GUI Buttons toggle controls ************************************************
	GUI.contentColor = Color.blue;
	toggleABT = GUI.Toggle (Rect (10, 10, 200, 50), toggleABT, "Button A Pressed!");
	toggleBBT = GUI.Toggle (Rect (10, 30, 200, 50), toggleBBT, "Button B Pressed!");
	toggleXBT = GUI.Toggle (Rect (10, 50, 200, 50), toggleXBT, "Button X Pressed!");
	toggleYBT = GUI.Toggle (Rect (10, 70, 200, 50), toggleYBT, "Button Y Pressed!");
	toggleLBBT = GUI.Toggle (Rect (10, 90, 200, 50), toggleLBBT, "Button LB Pressed!");
	toggleRBBT = GUI.Toggle (Rect (10, 110, 200, 50), toggleRBBT, "Button RB Pressed!");
	toggleBackSelectBT = GUI.Toggle (Rect (10, 130, 200, 50), toggleBackSelectBT, "Button Back / Select Pressed!");
	toggleStartBT = GUI.Toggle (Rect (10, 150, 200, 50), toggleStartBT, "Button Start Pressed!");
	toggleL3BT = GUI.Toggle (Rect (10, 170, 200, 50), toggleL3BT, "Button L3 Pressed!");
	toggleR3BT = GUI.Toggle (Rect (10, 190, 200, 50), toggleR3BT, "Button R3 Pressed!");	
	
	//******************************************** GUI Axis controls **********************************************************
	toggleLTAxis = GUI.Toggle (Rect (10, 210, 200, 50), toggleLTAxis, "Button Axis LT Pressed!");
	toggleRTAxis = GUI.Toggle (Rect (10, 230, 200, 50), toggleRTAxis, "Button Axis RT Pressed!");
	
	UpDownAxis = GUI.Toggle (Rect (10, 250, 200, 50), UpDownAxis, "Up or Down Arrow is Pressed!");
	LeftRightAxis = GUI.Toggle (Rect (10, 270, 200, 50), LeftRightAxis, "Left or Right Arrow is Pressed!");		
		
}

function ButtonsControl ()
{
	// keys code
	
	//Joystick1Button0 = A
	//Joystick1Button1 = B
	//Joystick1Button2 = X
	//Joystick1Button3 = Y
	//Joystick1Button4 = LB
	//Joystick1Button5 = RB
	//Joystick1Button6 = Back / Select
	//Joystick1Button7 = Start
	//Joystick1Button8 = L3
	//Joystick1Button9 = R3
	
	//***************************************************************************
	
	//JoystickButton0 = A
	//JoystickButton1 = B
	//JoystickButton2 = X
	//JoystickButton3 = Y
	//JoystickButton4 = LB
	//JoystickButton5 = RB
	//JoystickButton6 = Back / Select
	//JoystickButton7 = Start
	//JoystickButton8 = L3
	//JoystickButton9 = R3	



	// Left / Right Arrows control
	if (Input.GetAxis ("LeftRightArrow"))
    {        	
		//print ("LeftRightArrow");
		UpDownAxis = true;		
    }
    else
    {
    	UpDownAxis = false;
    }
    
    // Up / Down Arrows control
	if (Input.GetAxis ("UpDownArrow"))
    {        	
		//UpDownAxis ("UpDownArrow");	
		LeftRightAxis = true;	
    }
    else
    {
    	LeftRightAxis = false;
    }


	// LT (Left trigger)
	if (Input.GetAxis ("LT"))
    {        	
		//print ("LT"); 
		toggleLTAxis = true;
    }
    else
    {
    	toggleLTAxis = false;
    }
    // RT (right trigger)
    if (Input.GetAxis ("RT"))
    {        	
       // print ("RT"); 
       toggleRTAxis = true;
    }
    else
    {
    	toggleRTAxis = false;
    }

	
	if (Input.GetKey (KeyCode.Joystick1Button1))
    {        	
    	toggleBBT = true;
    }
    else
    {
    	toggleBBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button2))
    {        	
    	toggleXBT = true;
    }
    else
    {
    	toggleXBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button3))
    {        	
    	toggleYBT = true;
    }
    else
    {
    	toggleYBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button4))
    {        	
    	toggleLBBT = true;
    }
    else
    {
    	toggleLBBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button5))
    {        	
    	toggleRBBT = true;
    }
    else
    {
    	toggleRBBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button6))
    {        	
    	toggleBackSelectBT = true;
    }
    else
    {
    	toggleBackSelectBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button7))
    {        	
    	toggleStartBT = true;
    }
    else
    {
    	toggleStartBT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button8))
    {        	
    	toggleL3BT = true;
    }
    else
    {
    	toggleL3BT = false;
    }
    
    if (Input.GetKey (KeyCode.Joystick1Button9))
    {        	
    	toggleR3BT = true;
    }
    else
    {
    	toggleR3BT = false;
    }    
}

@script ExecuteInEditMode()
@script RequireComponent(CharacterController)