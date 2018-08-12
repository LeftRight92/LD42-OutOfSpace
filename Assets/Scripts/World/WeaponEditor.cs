
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using LD42.Scripts.World;

[CustomEditor(typeof(Weapon))]

public class WeaponEditor : Editor
{

    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    Weapon t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;

    void OnEnable()
    {
        t = (Weapon)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("fireLocations"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();


        //DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("", DisplayFieldType);

        if (GUILayout.Button("Add New"))
        {
            t.fireLocations.Add(new Vector3());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);
            SerializedProperty MyVect3 = MyListRef.FindPropertyRelative("AnVector3");


            // Display the property fields in two ways.


                //Or

                //2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
                EditorGUILayout.LabelField("Customizable Field With GUI");
                MyVect3.vector3Value = EditorGUILayout.Vector3Field("My Custom Vector 3", MyVect3.vector3Value);


                // Array fields with remove at index
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Array Fields");

                if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    MyArray.InsertArrayElementAtIndex(MyArray.arraySize);
                    MyArray.GetArrayElementAtIndex(MyArray.arraySize - 1).intValue = 0;
                }

                for (int a = 0; a < MyArray.arraySize; a++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("My Custom Int (" + a.ToString() + ")", GUILayout.MaxWidth(120));
                    MyArray.GetArrayElementAtIndex(a).intValue = EditorGUILayout.IntField("", MyArray.GetArrayElementAtIndex(a).intValue, GUILayout.MaxWidth(100));
                    if (GUILayout.Button("-", GUILayout.MaxWidth(15), GUILayout.MaxHeight(15)))
                    {
                        MyArray.DeleteArrayElementAtIndex(a);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space();

            //Remove this index from the List
            EditorGUILayout.LabelField("Remove an index from the List<> with a button");
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}