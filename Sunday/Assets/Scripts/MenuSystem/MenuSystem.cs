using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MenuSystem : MonoBehaviour {
	
	public MenuDefinitions.Menus InitialMenu = MenuDefinitions.Menus.Main;
	public bool m_bAutoLaunch = false;
	public GameObject m_pInputBlocker;
	public MenuBase[] MenuPrefabs;
	
	private Dictionary<int, MenuBase> m_pMenus;
	private Stack<int> m_pMenuList;
	private MenuBase m_pCurrentMenu = null;
	
	private int nNextMenu = -1;
	
	public void Initialize()
	{
		SetInput(false);
		m_pMenus = new Dictionary<int, MenuBase>();
		m_pMenuList = new Stack<int>();
		
		AudioController.PlayMusic( "MainMusic" );
			
		for (int i = 0; i < MenuPrefabs.Length; i++) {
			GameObject go = NGUITools.AddChild(this.gameObject, MenuPrefabs[i].gameObject);
			MenuBase menu = go.GetComponent<MenuBase>();
			menu.Initialize(this);
			menu.SetState(false);
			m_pMenus.Add( (int)MenuPrefabs[i].Menu, menu );
		}
		
		if( m_bAutoLaunch == true )
			OpenMenu(InitialMenu);
		else
		{
			MenuBase temp = GetComponentInChildren<MenuBase>();
			if( temp != null)
			{
				temp.Initialize(this);
				temp.SetState(true, TransitionEnd);
			}
		}
	
	}
	
	public void OpenMenu( MenuDefinitions.Menus menu )
	{
		if( m_pCurrentMenu != null && m_pCurrentMenu.Menu == menu)
			return;
		
		MenuBase result = null;
		if( m_pMenus.TryGetValue((int)menu, out result) == true)
		{
			SetInput(false);
		
			nNextMenu = (int)menu;
			if( m_pCurrentMenu != null )
				m_pCurrentMenu.SetState(false, TransitionEnd);
			else
				TransitionEnd();
		}
	}
	
	private void TransitionEnd()
	{
		if( nNextMenu >= 0 )
		{
			SetInput(false);
			MenuBase result = null;
			if( m_pMenus.TryGetValue(nNextMenu, out result) == true)
			{
				result.SetState(true, TransitionEnd);
				m_pCurrentMenu = result;
				
				m_pMenuList.Push(nNextMenu);
			}
			nNextMenu = -1;
		}
		else
		{
			SetInput(true);
		}
	}
	
	public void CloseMenu()
	{
		if( m_pCurrentMenu == null)
			return;
		
		SetInput(false);
		
		nNextMenu = m_pMenuList.Pop();
		if( m_pMenuList.Count != 0)
			nNextMenu = m_pMenuList.Peek();
		else
			nNextMenu = -1;
		
		m_pCurrentMenu.SetState(false, TransitionEnd);

	}
	
	public void OpenMenuWithHeirarchy(MenuDefinitions.Menus[] menuList)
	{
		if( m_pCurrentMenu != null)
			m_pCurrentMenu.SetState(false);
		
		m_pCurrentMenu = null;
		m_pMenuList.Clear();
		
		for (int i = 0; i < menuList.Length; i++) {
			m_pMenuList.Push( (int)menuList[i] );
		}
		
		OpenMenu( (MenuDefinitions.Menus)m_pMenuList.Peek() );
	}
	
	public void Start()
	{
		Initialize();
	}
	
	public void Update()
	{
		float dt = Time.deltaTime;
		if( m_pCurrentMenu != null )
			m_pCurrentMenu.UpdateObject(dt);
	}
	
	public void SetInput(bool bEnabled)
	{
		NGUITools.SetActive(m_pInputBlocker, !bEnabled);
	}
}
