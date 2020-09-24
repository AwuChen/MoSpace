using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class LinkTab : MonoBehaviour 
{

	public InputField Field;
    public string link;
    public void OpenLink()
	{
		Application.OpenURL(Field.text);
	}

	public void OpenLinkJS()
	{
		Application.ExternalEval("window.open('"+link+"');");
	}

	public void OpenLinkJSPlugin()
	{
		#if !UNITY_EDITOR
		openWindow(Field.text);
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}