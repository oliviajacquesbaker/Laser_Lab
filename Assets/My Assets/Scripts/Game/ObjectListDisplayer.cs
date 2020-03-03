using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListDisplayer : MonoBehaviour
{
    public IconSet iconSet;
    public GameObject ObjectButtonPrefab;
    public GameManager manager;

    private void Start()
    {
    }

    public void ReloadButtons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < manager.UnplacedObjects.Count; i++)
        {
            GameObject button = Instantiate(ObjectButtonPrefab, transform);
            SelectorButton selector = button.GetComponent<SelectorButton>();

            if (selector == null)
            {
                Debug.LogError("No selector button on button prefab");
                Destroy(button);
                break;
            }
            button.name = "Selector button " + i;
            selector.icon.sprite = iconSet.FindIcon(manager.UnplacedObjects[i].GetType());
            selector.id = i;
            selector.displayer = this;
        }
    }

    public void OnClickButton(int id)
    {
        manager.selectedObjectIndex = id;
    }
}
