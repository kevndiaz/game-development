using Godot;
using System;

public partial class scene2_movement : CharacterBody3D
{
	public const float Speed = 10f;
	public const float JumpVelocity = 10f;
	public const float Gravity = 40f;
	private Vector3 direction;

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
		direction = Input.GetAxis("left", "right") * Vector3.Right;

		// Executes Movement
		MoveAndSlide();

		// Camera Controller
		GetNode<Node3D>("Camera Controller").Position = GetNode<Node3D>("Camera Controller").Position.Lerp(Position, 0.05f);

		
	}
}
