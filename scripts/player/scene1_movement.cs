using Godot;
using System;
using System.Diagnostics;
using System.Numerics;

public partial class NewScript : CharacterBody3D
{
	public float player_speed = 10f;
	public float jump_velocity = 40f;
	private Godot.Vector3 direction;
	private float gravity = 40f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		Velocity = direction.Normalized() * player_speed + Velocity.Y * Godot.Vector3.Up;

		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("jump"))
			{
				Velocity = new Godot.Vector3(Velocity.X, jump_velocity, Velocity.Z);
			}
			else
			{
				Velocity = new Godot.Vector3(Velocity.X, -0.1f, Velocity.Z);
			}
		}
		else
		{
			Velocity = new Godot.Vector3(Velocity.X, Velocity.Y + (float)(-gravity * delta), Velocity.Z);
		}

		direction = Input.GetAxis("left", "right") * Godot.Vector3.Right + Input.GetAxis("backward", "forward") * Godot.Vector3.Forward;

		MoveAndSlide();
	}
}
