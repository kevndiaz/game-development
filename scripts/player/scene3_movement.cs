using Godot;
using System;
using System.IO;

public partial class scene3_movement : CharacterBody3D
{
	public const float Speed = 10f;
	public const float JumpVelocity = 10f;
	public const float Gravity = 40f;
	private Vector3 direction;
	public const float MouseSensitivity = 0.1f;
	private float smoothness = 0.2f;
	private float zoomDistance = 2.0f;


	/////////////////////////////////////////////

	private Node3D camera;
	private bool isCameraRight = true;
	private bool isFirstPerson = false;
	private bool isScoped = false;
	public float CameraOffset = 1.5f;
	public float CameraHeight = 0f;
	public float CameraDistance = 0f;
	private float cameraPitch = 0f; 


	public override void _Ready()
	{
		// Hide the mouse cursor
		Input.MouseMode = Input.MouseModeEnum.Captured;

		camera = GetNode<Node3D>("Camera Controller");
	}


	public override void _Input(InputEvent @event)
	{
		// Handle mouse look
		if (@event is InputEventMouseMotion mouseEvent)
		{
			// Yaw rotation (left-right)
			RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * MouseSensitivity));
			GetNode<Node3D>("Camera Controller").RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * MouseSensitivity));

			// Pitch rotation (up-down), clamping to avoid flipping the camera
			cameraPitch = Mathf.Clamp(cameraPitch - mouseEvent.Relative.Y * MouseSensitivity, -90, 90);
			camera.RotationDegrees = new Vector3(cameraPitch, camera.RotationDegrees.Y, camera.RotationDegrees.Z);
			
		}
	}


	public override void _PhysicsProcess(double delta)
	{
		Velocity = direction.Normalized() * Speed + Velocity.Y * Vector3.Up;

		// Handle Jump.
		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("jump")){
				Velocity = new Vector3(Velocity.X, JumpVelocity, Velocity.Z);
			} else {
				Velocity = new Vector3(Velocity.X, -0.1f, Velocity.Z);
			}
		} else { // Gravity
			Velocity = new Vector3(Velocity.X, Velocity.Y + -Gravity * (float)delta, Velocity.Z);
		}
		
		// Direction Inputs
		var input_dir = Input.GetVector("left", "right", "forward", "backward");
		direction = Transform.Basis * new Vector3(input_dir.X, 0, input_dir.Y);

		// Executes Movement
		MoveAndSlide();
		
		if(Input.IsActionJustPressed("shift")){
			isCameraRight = !isCameraRight;
		}

		if(Input.IsActionJustPressed("first-person")){
			isFirstPerson = !isFirstPerson;
		}

        // Hold right-click to scope in
		if(Input.IsActionJustPressed("right-click")){
			isScoped = !isScoped;
		} else if(Input.IsActionJustReleased("right-click")){
			isScoped = !isScoped;
		}

		zoomDistance = isScoped ? 1.0f : 0f;

		CameraOffset = isFirstPerson ? 0 : 1.5f; // Changes cam offset to 0 if first person is true
		smoothness = isFirstPerson ? 1.0f: 0.2f;

		float horizontalOffset = isCameraRight ? CameraOffset : -CameraOffset; // Switch the side in which the camera is positioned in 3rd person mode

		Vector3 desiredCameraPosition = Position // Character's position
										- Transform.Basis.Z * CameraDistance // Offset behind the character
										+ Transform.Basis.X * horizontalOffset // Offset to the side
										+ Vector3.Up * CameraHeight; // Raise the camera height


		// Handle specific camera location for switching sides
		GetNode<Node3D>("Camera Controller").Position = GetNode<Node3D>("Camera Controller").Position.Lerp(desiredCameraPosition, smoothness);
		
		
		// Handle specific camera location for first/third person
		Node3D cameraLocation = GetNode<Node3D>("Camera Controller/Camera Location");
		Vector3 targetLocation = isFirstPerson ? new Vector3(0, 0, 0) : new Vector3(0, 2, 4 - zoomDistance);
		cameraLocation.Position = cameraLocation.Position.Lerp(targetLocation, smoothness);		
	}
}
