using System.Collections;
using System.Collections.Generic;
using DaemonTools;
using UnityEngine;

public abstract class UIBase : MonoBehaviour {
	/// <summary>
	/// 当打开ui时调用
	/// </summary>
	/// <param name="IsfirstOpen">是否第一次打开</param>
	/// <param name="value">自定义数值</param>
	public abstract void show (bool IsfirstOpen, object[] value);
	/// <summary>
	/// 关闭ui时调用
	/// </summary>
	public abstract void close ();
}