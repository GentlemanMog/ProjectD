using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent (typeof(InventoryManager))]
public class Inventory : MonoBehaviour {
	public int weaponSlot, equipmentSlot, armourSlot;
	public List<ItemConstructor> inventory = new List<ItemConstructor>();
	public List<ItemConstructor> slots = new List<ItemConstructor> ();

	private InventoryManager database;
	private bool showInventory;


	// Use this for initialization
	void Start () {
		for(int i = 0; i < (weaponSlot + armourSlot + (equipmentSlot * 2)); i++){
			slots.Add(new ItemConstructor());
			inventory.Add(new ItemConstructor());
		}

		database = GetComponent<InventoryManager> ();

		inventory [0] = database.items [0];
		//print(database.items[0].item);
		//inventory.Add (database.items [0]);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Tab)){
			showInventory = !showInventory;
		}
	}
	
	void OnGUI(){
		if (showInventory) {
			DrawInventory();
		}

		for(int i = 0; i < inventory.Count; i++){

			//GUI.Label(new Rect(10,10 * 40 ,200,50), inventory[i].item);
		}
	}

	void DrawInventory(){

		int i = 0;

		for (int x = 0; x < 1; x++) { 
			for (int y = 0; y < (equipmentSlot * 2); y++) {
				Rect slotRect = new Rect (0, y * 20, 50, 50);
				GUI.Box (new Rect (x * 20, y * 20, 50, 50), y.ToString ());

				slots[i] = inventory[i];


				if(slots[i].id != null){
					//GUI.DrawTexture(slotRect,slots[i].item);
				GUI.Label(slotRect, slots[i].item);
				}


				i++;
			}
		}
	}

	void addItem(int id){
		for (int i = 0; i < inventory.Count; i++) {
			if(inventory[i].id == null){
				for(int j = 0; j < database.items.Count; j++){
					if(database.items[j].id == id){
						inventory[i] = database.items[j];
					}
				}
				break;
			}
		}
	}

	void removeItem(int id){
		for (int i = 0; i < inventory.Count; i++) {
			if(inventory[i].id == id){
				inventory[i] = new ItemConstructor();
				break;
			}
		} 
	}



	bool invContains (int id){
		bool doesContain = false;
		for (int i = 0; i < inventory.Count; i++) { 
			doesContain	= inventory[i].id == id;
			if(doesContain){
				break;
			}
		}
		return doesContain;

	}

	void useConsumable(ItemConstructor item, int slot, bool itemUsed){
		if (itemUsed)
			inventory[slot] = new ItemConstructor();
		
	}

	void LoadInventory(){
		for (int i = 0; i < inventory.Count; i++) {
			inventory[i] = PlayerPrefs.GetInt("_INV" + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("_INV" + i)] : new ItemConstructor();
		}
	}

	void SaveInventory(){
		for (int i = 0; i < inventory.Count; i++) {
			PlayerPrefs.SetInt ("_INV" + i, inventory [i].id);
		}
	}

}
