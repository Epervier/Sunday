using UnityEngine;
using System.Collections;

public class MenuDefinitions : object{

	public enum Menus { Title, Main, Setup, Settings, Credits, TechTree };
	public enum ButtonActions { ChangeMenu, ChangeSubMenu, OpenDialog, Previous, MenuSpecific };
	public enum Dialogs { Basic, Purchase, Quick, Error };
	public enum TransitionTypes { Fade, Translate, Spin, Scale };
	public enum Directions { Center, Left, Right, Up, Down };
	
}