using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Utility;

namespace LD42.Scripts.Weapons
{
	[System.Serializable]
	public class WeaponList : List<Weapon>, ISerializationCallbackReceiver
	{
		//private List<Weapon> weapons;

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {

        }

        void OnGUI()
		{
			GUILayout.Label("Size" + Capacity);
			Capacity = GUILayout.TextField(Capacity.ToString()).Call(int.Parse);
        }
	}
}