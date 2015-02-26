using UnityEngine;
using System.Collections;


//The menu manager controls the switching between UI menu's.
public class MenuManager : System.Object {

    private static Menu CurrentMenu;
    private static MenuManager instance;

	public void Start(){
		ShowMenu(CurrentMenu);
	}

	//Show menu shows the current menu. If null is passed it, it shows nothing while disabling the previous menu.
	public static void ShowMenu(Menu menu){
		if(CurrentMenu != null || menu == null){
			CurrentMenu.isOpen = false;
		}
		CurrentMenu = menu;
		if(menu != null){
			CurrentMenu.isOpen = true;
		}
	}
}
