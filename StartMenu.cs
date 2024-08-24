using Godot;
using System;

public partial class StartMenu : Panel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var a = new Scripting.ScriptParser().Parse("GameScripts/test.script");
		GetParent<MessageDialogBox>().reachedEnding += (i)=>{
			GD.Print($"Ending {i}");
		};
		GetParent<MessageDialogBox>().DoDialog(a);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
