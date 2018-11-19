using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class Level1DialogConfig : BaseConfig {

	public int ID;
	public Level1Disposition.MainDisPosition myDisposition;
	public string Dialog1;
	public string Dialog2;
	public string Dialog3;
	public string Dialog4;
	public string Dialog5;
	public Vector3 dispositonValue;
	public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
		int dispositon = 1;
		int.TryParse(m_data[1], out dispositon);
		myDisposition = (Level1Disposition.MainDisPosition)dispositon;
		Dialog1 = m_data[2];
		Dialog2 = m_data[3];
		Dialog3 = m_data[4];
		Dialog4 = m_data[5];
		Dialog5 = m_data[6];
		string[] dispositionVec3 =  m_data[7].Split('#');
		dispositonValue = Vector3.zero;
		float.TryParse(dispositionVec3[0], out dispositonValue.x);
		float.TryParse(dispositionVec3[1], out dispositionvalue.y);
		float.TryParse(dispositionVec3[2], out dispositionvalue.z);
    }
	
}
public class Level1Disposition
{
	public float Angry;
	public float Sad;
	public float Complacent;
	public enum MainDisPosition{
		e_angry = 1,
		e_sad = 2,
		e_complacent = 3
	}

	public MainDisPosition mainDisPosition;

	public Level1Disposition(float angry, float sad, float complacent){
		Angry = angry;
		Sad = sad;
		Complacent = complacent;

		if(Angry >= Sad){
			if(Angry>= complacent){
				mainDisPosition = MainDisPosition.e_angry;
			}
			else
			{
				mainDisPosition = MainDisPosition.e_complacent;
			}
		}
		else
		{
			if(Sad>= complacent){
				mainDisPosition = MainDisPosition.e_sad;
			}
			else
			{
				mainDisPosition = MainDisPosition.e_complacent;
			}
		}
		
	}
	
}
