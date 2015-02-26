using UnityEngine;
using System.Collections;

//The Menu class represents the state of a UI menu and interfaces the code with the animator.
public class Menu : MonoBehaviour {

	private Animator _animator;
	private CanvasGroup _canvasGroup;

	//Determines if the menu is open on the screen
	public bool isOpen{
		get{
			return _animator.GetBool("IsOpen");
		}
		set{
			_animator.SetBool("IsOpen",value);
		}
	}

	void Awake(){
		_animator = GetComponent<Animator>();
		_canvasGroup = GetComponent<CanvasGroup>();

		var rect = GetComponent<RectTransform>();
		rect.offsetMax = rect.offsetMin = new Vector2(0,0);
	}

	void Update(){
		if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Open")){
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
		} else{
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		}
	}
}
