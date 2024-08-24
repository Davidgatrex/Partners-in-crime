using Godot;
using System;

public partial class ArrowContinue : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	float y;
	public override void _Ready()
	{
		y = Position.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	int mult = 1;
	int c = 0;
	public override void _Process(double delta)
	{
		Translate(new Vector2(0, 10*mult * (float)delta));
		c++;
		if(c == 20){
			mult *= -1;
			c = -20;
		}
	}
}
