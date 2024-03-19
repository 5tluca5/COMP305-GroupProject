using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager
{
    private static UIManager instance;
    private Transform uiRoot;

    // Dictionary for path
    public Dictionary<string, string> pathDict;

    // Dictionary for prefabs
    public Dictionary<string, GameObject> prefabDict;

    // Dictionary for opened panel
    public Dictionary<string, BasePanel> panelDict;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                if (GameObject.Find("Canvas"))
                {
                    uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    uiRoot = new GameObject("Canvas").transform;
                }
            }
            return uiRoot;
        }
    }

    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        pathDict = new Dictionary<string, string>()
        {
            { UIConst.PackagePanel, "Package/PackagePanel" },
            // Add more panels here if needed
        };
    }

    public BasePanel GetPanel(string name)
    {
        if (panelDict.TryGetValue(name, out BasePanel panel))
        {
            return panel;
        }
        return null;
    }

    public BasePanel OpenPanel(string name)
    {
        if (panelDict.ContainsKey(name))
        {
            Debug.Log("Panel already opened: " + name);
            return panelDict[name];
        }

        if (!pathDict.TryGetValue(name, out string path))
        {
            Debug.LogError("Panel name error, or no path has been set: " + name);
            return null;
        }

        if (!prefabDict.TryGetValue(path, out GameObject panelPrefab))
        {
            string realPath = "Prefabs/Panels/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(path, panelPrefab);
        }

    
        GameObject gameObject = UnityEngine.Object.Instantiate(panelPrefab, uiRoot, false);
        BasePanel panel = gameObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        if (!panelDict.TryGetValue(name, out BasePanel panel))
        {
            Debug.LogError("Panel not open yet: " + name);
            return false;
        }

        panel.ClosePanel();
        panelDict.Remove(name);
        return true;
    }

    public class UIConst
    {
        // Define constant panel names here
        public const string PackagePanel = "PackagePanel";
        // Add more panel names if needed
    }
}
