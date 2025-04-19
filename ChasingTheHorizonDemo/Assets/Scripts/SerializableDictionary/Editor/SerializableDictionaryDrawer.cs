using System;
using System.Globalization;
using UnityEditor.UIElements;
using System.Text;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
public class SerializableDictionaryDrawer : PropertyDrawer
{
    SerializedProperty linkedProperty;
    SerializedProperty linkedKeys;
    SerializedProperty linkedValues;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        linkedProperty = property;
        // these have to match the names of the keys and values lists from SerializableDictionary
        linkedKeys = property.FindPropertyRelative("keys");
        linkedValues = property.FindPropertyRelative("values");

        var containerUI = new Foldout()
        {
            text = property.displayName,
            viewDataKey = $"{property.serializedObject.targetObject.GetInstanceID()}.{property.name}"
        };

        var ContentsUI = new ListView()
        {
            showAddRemoveFooter = true,
            showBorder = true,
            showAlternatingRowBackgrounds = AlternatingRowBackground.All,
            showFoldoutHeader = false,
            showBoundCollectionSize = false,
            reorderable = false,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            headerTitle = property.displayName,
            bindingPath = linkedKeys.propertyPath,
            bindItem = BindListItem,
            overridingAddButtonBehavior = OnAddButton,
            onRemove = OnRemove
        };

        containerUI.Add(ContentsUI);

        var removeDuplicatesButton = new Button() { text = "Remove Duplicates" };
        removeDuplicatesButton.clicked += OnRemoveDuplicates;

        containerUI.Add(removeDuplicatesButton);

        return containerUI;
    }

    bool AreDuplicateKeysPresent(SerializedProperty keyProperty, int keyIndex)
    {
        for (int i = 0; i < linkedKeys.arraySize; i++)
        {
            if (i == keyIndex) continue;

            SerializedProperty otherKey = linkedKeys.GetArrayElementAtIndex(i);
            if (otherKey.boxedValue.Equals(keyProperty.boxedValue)) return true;
        }
        return false;
    }

    void BindListItem(VisualElement itemUI, int itemIndex)
    {
        itemUI.Clear();
        itemUI.Unbind();

        var keyProperty = linkedKeys.GetArrayElementAtIndex(itemIndex);
        var valueProperty = linkedValues.GetArrayElementAtIndex(itemIndex);

        var keyUI = new PropertyField(keyProperty) { label = "Key" };
        var valueUI = new PropertyField(valueProperty) { label = "Value" };

        itemUI.Add(keyUI);
        itemUI.Add(valueUI);

        var WarningUI = new Label("<b>Error: Duplicate Key Detected</b>");
        itemUI.Add(WarningUI);

        WarningUI.visible = AreDuplicateKeysPresent(keyProperty, itemIndex);

        itemUI.TrackPropertyValue(keyProperty, (SerializedProperty keyProp) =>
        {
            WarningUI.visible = AreDuplicateKeysPresent(keyProperty, itemIndex);
        });

        itemUI.Bind(linkedProperty.serializedObject);
    }

    void OnAddButton(BaseListView listView, Button button)
    {
        linkedKeys.InsertArrayElementAtIndex(linkedKeys.arraySize);
        linkedValues.InsertArrayElementAtIndex(linkedValues.arraySize);
        linkedProperty.serializedObject.ApplyModifiedProperties();
    }

    void OnRemoveDuplicates()
    {
        List<int> indicesToRemove = new();
        for (int i = 0; i < linkedKeys.arraySize; i++)
        {
            SerializedProperty firstKey = linkedKeys.GetArrayElementAtIndex(i);
            for (int j = i + 1; j < linkedKeys.arraySize; j++)
            {
                SerializedProperty secondKey = linkedKeys.GetArrayElementAtIndex(j);
                if (firstKey.boxedValue.Equals(secondKey.boxedValue) &&
                    !indicesToRemove.Contains(j))
                {
                    indicesToRemove.Add(j);
                }
            }
        }

        for (int i = indicesToRemove.Count - 1; i >= 0; i--)
        {
            int indexToRemove = indicesToRemove[i];
            linkedKeys.DeleteArrayElementAtIndex(indexToRemove);
            linkedValues.DeleteArrayElementAtIndex(indexToRemove);
        }
        linkedProperty.serializedObject.ApplyModifiedProperties();
    }

    void OnRemove(BaseListView listView)
    {
        if (linkedKeys.arraySize > 0 && listView.selectedIndex >= 0 &&
            listView.selectedIndex < linkedKeys.arraySize)
        {
            int indexToRemove = listView.selectedIndex;
            linkedKeys.DeleteArrayElementAtIndex(indexToRemove);
            linkedValues.DeleteArrayElementAtIndex(indexToRemove);
            linkedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}

public class SerializableDictionaryConverter<TKey, TValue> : UxmlAttributeConverter<SerializableDictionary<TKey, TValue>>
{
    static string ValueToString(object value) => Convert.ToString(value, CultureInfo.InvariantCulture);

    public override string ToString(SerializableDictionary<TKey, TValue> source)
    {
        var dataBuilder = new StringBuilder();

        foreach (var keyValuePair in source)
        {
            dataBuilder.Append($"{ValueToString(keyValuePair.Key)}|{ValueToString(keyValuePair.Value)}");
        }

        return dataBuilder.ToString();
    }

    public override SerializableDictionary<TKey, TValue> FromString(string stringToConvert)
    {
        var outputDict = new SerializableDictionary<TKey, TValue>();

        var keyValuePairs = stringToConvert.Split(',');
        foreach (var keyValuePair in keyValuePairs)
        {
            var Fields = keyValuePair.Split('|');
            TKey key = (TKey)Convert.ChangeType(Fields[0], typeof(TKey));
            TValue value = (TValue)Convert.ChangeType(Fields[1], typeof(TValue));

            outputDict.EditorOnlyAdd(key, value);
        }
        outputDict.SynchronizeToSerializedData();

        return outputDict;
    }
}