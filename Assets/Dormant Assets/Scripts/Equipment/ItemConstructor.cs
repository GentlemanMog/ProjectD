using UnityEngine;
using System.Collections;

	[System.Serializable]
public class ItemConstructor {

	public string item;
	public int id;
	public string description;
	public Sprite icon;
	public ItemType type;

	public enum ItemType
	{
		Weapon,
		Equipment,
		Consumable,
		Armour
	}

	public ItemConstructor(string itemName, int ID, string desc, ItemType tOi)
	{
		item = itemName;
		id = ID;
		icon = Resources.Load<Sprite> (itemName + "_8");
		description = desc;
		type = tOi;
	}

	public ItemConstructor(){
		id = -1;
	}
}
