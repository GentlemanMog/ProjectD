using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof(ItemConstructor))]
public class InventoryManager : MonoBehaviour {
	public List<ItemConstructor> items = new List<ItemConstructor>();
	//public List<ItemConstructor> items = new List<ItemConstructor>();

	void Start(){

		items.Add (new ItemConstructor ("bow",0,"An old Bow",ItemConstructor.ItemType.Weapon));
	}
}
