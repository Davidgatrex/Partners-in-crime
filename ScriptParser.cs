using Godot;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Scripting;
public partial class ScriptParser
{
	public GameScript Parse(string text, bool isPath)
	{
		if(isPath)
			return Parse(text);
		GameScript Gs = new GameScript();
		string[] lines = text.Split("\n");
		int pos = 1;
		foreach(string s in lines){
			var Fir = s.Split(" ")[0];
			if(Fir == "[Dialog]"){
				Gs.Dialogs.Add(new Dialog(s.Split(" ")[1], lines.Skip(pos).ToArray().Join("\n").Split("[End]")[0]));
			}
			if(Fir == "[Choice]"){
				var Members = lines.Skip(pos).ToArray().Join("\n").Split("[End]")[0].Split("\n");
				
			}
			pos += 1;
		}
		GD.Print($"{Gs.Dialogs.Count} elements");

		return Gs;
	}
	
	public GameScript Parse(string path){
		return Parse(File.ReadAllText(path), false);
	}
}

public enum DialogType{
	Standard,
	Choice
}



public class Dialog{
	public string Character {get; private set;}
	public string Text {get; private set;}
	public DialogType type = DialogType.Standard;

	public Dialog(string c, string t){
		Character = c;
		Text = t;
	}
	public Dialog(string c, string t, DialogType ty){
		Character = c;
		Text = t;
		type = ty;
	}
}

public class GameScript
{
	public List<Dialog> Dialogs = new List<Dialog>();
}
