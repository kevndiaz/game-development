using Godot;
using System;

public partial class BlasterD : Node3D
{
	private bool isWeaponRight = true;
	private float cameraPitch = 0f;
	public const float MouseSensitivity = 0.1f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Input(InputEvent @event)
	{
		// Handle mouse look
		if (@event is InputEventMouseMotion mouseEvent)
		{
			// Pitch rotation (up-down), clamping to avoid flipping the camera
			cameraPitch = Mathf.Clamp(cameraPitch - mouseEvent.Relative.Y * MouseSensitivity, -90, 90);
			RotationDegrees = new Vector3(cameraPitch, RotationDegrees.Y, RotationDegrees.Z);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("shift")){
			isWeaponRight = !isWeaponRight;
		}

		Vector3 weaponPositionOffset = isWeaponRight ? new Vector3(0.7f, 0f, -0.3f) : new Vector3(-0.7f, 0f, -0.3f);  // Offset change when switching weapon from right to left
		Position = Position.Lerp(weaponPositionOffset, 0.15f);

	}
}
