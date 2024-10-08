using Godot;
using System;
using Scripting;

public partial class MessageDialogBox : Control
{
	// Called when the node enters the scene tree for the first time.
	private Label NLabel;
	private Label MLabel;
	private Panel ButtonPanel;
	private GameScript script;
	private bool attached = false;
	public event Ending reachedEnding;
	public override void _Ready()
	{
		MLabel = GetChild(0).GetChild<Label>(0);
		NLabel = GetChild(1).GetChild<Label>(1);
		ButtonPanel = GetChild<Panel>(2);
		ButtonPanel.Visible = false;
		Visible = false;

		ButtonPanel.GetChild<Button>(0).ButtonUp += () =>{
			choice = 0;
		};
		ButtonPanel.GetChild<Button>(1).ButtonUp += () =>{
			choice = 1;
		};
		ButtonPanel.GetChild<Button>(2).ButtonUp += () =>{
			choice = 2;
		};
		ButtonPanel.GetChild<Button>(3).ButtonUp += () =>{
			choice = 3;
		};
	}

	public void DoDialog(GameScript _script){
		script = _script;
		attached = true;
		count = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	int count = 0;
	bool rel = false;
	int choice = -1;
	string[] targets = {"", "", "", ""};
	bool isChoice = false;
	bool ended = false;
	public override void _Process(double delta)
	{
		/*
		if(ended){
			this.Dispose();
		}
		*/
		if(attached && !ended){
			Visible = true;
			if(count == script.Dialogs.Count){
				if(((Input.IsKeyPressed(Key.Space) && !rel && !isChoice) || (choice > -1 && isChoice))){
					attached = false;
					count = 0;
					Visible = false;
					bool tmp = isChoice;
					if(isChoice){
						GD.Print($"Choice: {targets[choice]}");
						DoDialog(new ScriptParser().Parse($"GameScripts/{targets[choice].Replace('\n', '\0')}.script"));
						isChoice = false;
						Visible = true;
					}
					if(script.Dialogs[count].Character == "[JUMP]" && !tmp){
						DoDialog(new ScriptParser().Parse($"GameScripts/{script.Dialogs[count].Text.Replace('\n', '\0')}.script"));
						Visible = true;
					}
				}
				rel = Input.IsKeyPressed(Key.Space);
			}
			else{
				if(((Input.IsKeyPressed(Key.Space) && !rel && !isChoice) || (choice > -1 && isChoice)) || count == 0){
					bool tmp = isChoice;
					if(script.Dialogs[count].Character == "[ENDING]"){
						Visible = false;
						reachedEnding.Invoke(int.Parse(script.Dialogs[count].Text));
						ended = true;
						return;
					}
					if(isChoice){
						GD.Print($"Choice: {targets[choice]}");
						DoDialog(new ScriptParser().Parse($"GameScripts/{targets[choice].Replace('\n', '\0')}.script"));
						isChoice = false;
					}
					if(script.Dialogs[count].Character == "[JUMP]" && !tmp){
						DoDialog(new ScriptParser().Parse($"GameScripts/{script.Dialogs[count].Text.Replace('\n', '\0')}.script"));
					}
					else{
						NLabel.Text = script.Dialogs[count].Character;
						MLabel.Text = script.Dialogs[count].Text;
						if(script.Dialogs[count].type == DialogType.Choice){
							int i = 0;
							isChoice = true;
							foreach(ButtonData d in script.Dialogs[count].buttons){
								var b = ButtonPanel.GetChild<Button>(i);
								b.Text = d.Text;
								b.Visible = true;
								targets[i] = d.Target;
								i++;
							}
							ButtonPanel.Size = new Vector2(ButtonPanel.Size.X, 20+(78*(i)));
							ButtonPanel.Visible = true;
						}else{
							ButtonPanel.Visible = false;
							isChoice = false;
							choice = -1;
							foreach (Button b in ButtonPanel.GetChildren()){
								b.Visible = false;
							}
						}
						count++;
					}
				}
				rel = Input.IsKeyPressed(Key.Space);
			}
		}
		
	}
}

public delegate void Ending(int i);