using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public HexGrid m_pHexGrid;
	
	// Use this for initialization
	void Start () {
		m_pHexGrid.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		if( m_pHexGrid != null )
			m_pHexGrid.UpdateObject(dt);
	}
}
