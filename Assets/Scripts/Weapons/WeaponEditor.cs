
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using LD42.Scripts.Weapons;

[CustomEditor(typeof(WeaponController))]

public class WeaponEditor : Editor
{

    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    WeaponController weaponTarget;
    SerializedObject weaponSerialized;


	SerializedProperty type;
	SerializedProperty weaponFire;
	SerializedProperty cooldown;
	SerializedProperty simultaneousFire;
	SerializedProperty fireLocations;

	SerializedProperty available;
	SerializedProperty quantity;
	SerializedProperty size;
    SerializedProperty damage;

    void OnEnable()
    {
		weaponTarget = (WeaponController)target;
		weaponSerialized = new SerializedObject(weaponTarget);

		type = weaponSerialized.FindProperty("type");
		weaponFire = weaponSerialized.FindProperty("weaponFire");
		cooldown = weaponSerialized.FindProperty("cooldown");
		simultaneousFire = weaponSerialized.FindProperty("simultaneousFire");
		fireLocations = weaponSerialized.FindProperty("fireLocations");

		available = weaponSerialized.FindProperty("available");
		quantity = weaponSerialized.FindProperty("quantity");
		size = weaponSerialized.FindProperty("size");
        damage = weaponSerialized.FindProperty("damage");
    }

    public override void OnInspectorGUI()
    {

        weaponSerialized.Update();

		EditorGUILayout.PropertyField(type);
		EditorGUILayout.PropertyField(weaponFire);
		EditorGUILayout.PropertyField(cooldown);
		EditorGUILayout.PropertyField(simultaneousFire);

		EditorGUILayout.PropertyField(available);
		EditorGUILayout.PropertyField(quantity);
		EditorGUILayout.PropertyField(size);
        EditorGUILayout.PropertyField(damage);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

		EditorGUILayout.LabelField("Locations of firing points:");

        EditorGUILayout.Space();

		for (int i = 0; i < fireLocations.arraySize; i++)
		{
			SerializedProperty firePos = fireLocations.GetArrayElementAtIndex(i);

			firePos.vector3Value = EditorGUILayout.Vector3Field("Firing point " + i + ":", 
			                                                    firePos.vector3Value);
            
			EditorGUILayout.LabelField("Remove an index from the List<> with a button");
			if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
			{
				fireLocations.DeleteArrayElementAtIndex(i);
			}
			EditorGUILayout.Space();

			weaponSerialized.ApplyModifiedProperties();
		}

        EditorGUILayout.Space();

        if (GUILayout.Button("Add New"))
        {
            weaponTarget.fireLocations.Add(new Vector3());
        }
    }
}