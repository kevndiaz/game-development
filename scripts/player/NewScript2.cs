using Godot;
using System;
using System.Diagnostics;
using System.Numerics;

public partial class NewScript2 : RigidBody3D
{
	public float player_speed = 1000f;
	public float jump_velocity = 10f;
	private Godot.Vector3 direction;
	private float gravity = 0.5f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Kevin Diaz");
		Debug.WriteLine("Kevin Diaz(2)");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("jump"))
		{
			ApplyImpulse(Godot.Vector3.Zero, new Godot.Vector3(0, jump_velocity, 0));
		}

		if (Input.IsActionPressed("forward"))
		{
			ApplyImpulse(Godot.Vector3.Zero, new Godot.Vector3(player_speed * (float)delta, 0, 0));
		}

		//direction = Input.GetAxis("left", "right") * Godot.Vector3.Right + Input.GetAxis("backward", "forward") * Godot.Vector3.Forward;

	}
}
