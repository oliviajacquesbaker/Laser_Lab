using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListDisplayer : MonoBehaviour
{
    public IconSet iconSet;
    public GameObject ObjectButtonPrefab;
    List<SelectorButton> buttons;
    public GameManager manager;

    private void Start()
    {
        ReloadButtons();
    }

    public void ReloadButtons()
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < manager.UnplacedObjects.Count; i++)
        {
            GameObject button = Instantiate(ObjectButtonPrefab);
            SelectorButton selector = button.GetComponent<SelectorButton>();

            if (selector == null)
            {
                Debug.LogError("No selector button on button prefab");
                Destroy(button);
                break;
            }
            button.transform.SetParent(transform);
            button.name = "Selector button " + i;
            selector.icon.sprite = iconSet.FindIcon(manager.UnplacedObjects[i].GetType());
            selector.id = i;
            selector.displayer = this;
            buttons.Add(selector);
        }
    }

    public void OnClickButton(int id)
    {
        manager.selectedObjectIndex = id;
    }
}
