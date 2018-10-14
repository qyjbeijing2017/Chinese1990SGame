using System.Collections;
using System.Collections.Generic;
using DaemonTools;
using UnityEngine;
public class Test10_11Config : BaseConfig {
	public int ID;
	public string Fortest1;

	public float fortest2;
	public void InitConfig (List<string> m_data) {
		int.TryParse(m_data[0],out ID);
		Fortest1 = m_data[1];
		float.TryParse(m_data[2], out fortest2);
	}

}