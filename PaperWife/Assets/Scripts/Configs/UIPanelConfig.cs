using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelConfig : BaseConfig{
	public string NameId;
	public string Path;
	public override void InitConfig (List<string> m_data){
		NameId = m_data[0];
		Path = m_data[1];
	}

}
