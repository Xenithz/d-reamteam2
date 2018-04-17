using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameHover : MonoBehaviour 
{
	private Text myText;

	[SerializeField]
	private string debugString;

	public void Awake()
	{
		myText = GetComponentInChildren<Text>();
	}

	public void Update()
	{
		myText.text = GetComponent<PhotonView>().owner.NickName;
		debugString = myText.text;
	}
}
