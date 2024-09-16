using Godot;
using System;

public partial class Reticle1 : CenterContainer
{

	private Line2D top;
	private Line2D bottom;
	private Line2D left;
	private Line2D right;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		top = GetNode<Line2D>("top");
		bottom = GetNode<Line2D>("bottom");
		left = GetNode<Line2D>("left");
		right = GetNode<Line2D>("right");

		top.Position = new Vector2(0, -20);
		bottom.Position = new Vector2(0, 20);
		left.Position = new Vector2(-20, 0);
		right.Position = new Vector2(20, 0);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		if(Input.IsActionJustPressed("right-click")){
			top.Position = new Vector2(0, 0);
			bottom.Position = new Vector2(0, 0);
			left.Position = new Vector2(0, 0);
			right.Position = new Vector2(0, 0);
			
		} else if(Input.IsActionJustReleased("right-click")){
			top.Position = new Vector2(0, -20);
			bottom.Position = new Vector2(0, 20);
			left.Position = new Vector2(-20, 0);
			right.Position = new Vector2(20, 0);
		}

	}
}
