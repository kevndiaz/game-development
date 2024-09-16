using Godot;
using System;

public partial class Bullet : Node3D
{
	private MeshInstance3D mesh;
	private RayCast3D ray;
	private const float SPEED = 40.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		ray = GetNode<RayCast3D>("RayCast3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Transform.Basis * new Vector3(0, 0, -SPEED) * (float)delta;
	}
}
