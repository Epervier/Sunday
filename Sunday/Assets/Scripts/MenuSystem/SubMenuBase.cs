using UnityEngine;
using System.Collections;

public class SubMenuBase : MenuObject {
	
	public int m_nSubMenuID;
	protected MenuBase m_pParent;
	public virtual void Initialize(MenuBase parent)
	{
		m_pParent = parent;
		
		MenuButton[] buttons = gameObject.GetComponentsInChildren<MenuButton>();
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].Initialize(ButtonClicked);
		}
	}
	
	public virtual void ButtonClicked(int nAction, int nClick, int nSubClick)
	{
		
	}
}
