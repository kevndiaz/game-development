using Godot;
using System;

public partial class test_mouse : Node3D
{
	public const float MouseSensitivity = 0.1f; // Mouse sensitivity for camera look.
	
	
	private float cameraPitch = 0f; // Store the vertical camera rotation

	public override void _Ready()
	{
		// Hide the mouse cursor
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		// Handle mouse look
		if (@event is InputEventMouseMotion mouseEvent)
		{
			// Yaw rotation (left-right)
			RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * MouseSensitivity));
			
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		
	}
}
