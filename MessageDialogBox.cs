using Godot;
using System;
using Scripting;

public partial class MessageDialogBox : Control
{
	// Called when the node enters the scene tree for the first time.
	private Label NLabel;
	private Label MLabel;
	private GameScript script;
	private bool attached = false;
	public override void _Ready()
	{
		MLabel = GetChild(0).GetChild<Label>(0);
		NLabel = GetChild(1).GetChild<Label>(0);
		
	}

	public void DoDialog(GameScript _script){
		script = _script;
		attached = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	int count = 0;
	bool rel = false;
	public override void _Process(double delta)
	{
		
		if(attached){
			if(count == script.Dialogs.Count){
				attached = false;
				count = 0;
			}
			else{
				if((Input.IsKeyPressed(Key.Space) && !rel) || count == 0){
					NLabel.Text = script.Dialogs[count].Character;
					MLabel.Text = script.Dialogs[count].Text;
					count++;
				}
				rel = Input.IsKeyPressed(Key.Space);
			}
		}
		
	}
}
