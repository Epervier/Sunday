using UnityEngine;
using System.Collections;

public class MenuBase : MenuObject {
	
	public MenuDefinitions.Menus Menu;
	
	protected BetterList<SubMenuBase> m_pSubMenus;
	protected MenuSystem m_pMenuSystem;
	
	public virtual void Initialize(MenuSystem menuSystem)
	{
		m_pMenuSystem = menuSystem;
		
		MenuButton[] buttons = gameObject.GetComponentsInChildren<MenuButton>();
		for (int i = 0; i < buttons.Length; i++) {
			if( buttons[i].m_bSubMenuButton == false )
				buttons[i].Initialize(ButtonClicked);
		}
		
		SubMenuBase[] subMenus = gameObject.GetComponentsInChildren<SubMenuBase>();
		for (int i = 0; i < subMenus.Length; i++) {
			subMenus[i].Initialize(this);
		}
	}
	
	public virtual void ButtonClicked(int nAction, int nClick, int nSubClick)
	{
		switch ( (MenuDefinitions.ButtonActions)nAction ) 
		{
		case MenuDefinitions.ButtonActions.ChangeMenu:
		{
			m_pMenuSystem.OpenMenu( (MenuDefinitions.Menus)nClick );
			break;
		}
		case MenuDefinitions.ButtonActions.Previous:
		{
			m_pMenuSystem.CloseMenu();
			break;
		}
		default:
		break;
		}
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
	}
	
	
}
