using UnityEngine;

public class CanvasPanel{

    private GameObject canvasPrefab;
    private GameObject panelPrefab;

    private const string strCanvasPrefab = "Prefabs/Canvas";
    private const string strPanelPrefab = "Prefabs/Panel";

    /// <summary>
    /// Canvas GameObject
    /// </summary>
    public GameObject MenuCanvas { get; set; }
    /// <summary>
    /// Panel GameObject
    /// </summary>
    public GameObject MenuPanel { get; set; }
    

    /// <summary>
    /// Constructor. Loads the Canvas and Panel prefabs necessary to display UI elements. Sets up the panel to be a child of the canvas.
    /// This was done separately so different panels could be attached should the need arise (instead of the prefab having the panel in it already).
    /// </summary>
    public CanvasPanel()
    {
        canvasPrefab = Resources.Load<GameObject>(strCanvasPrefab);
        panelPrefab = Resources.Load<GameObject>(strPanelPrefab);
        ResetCanvasPanel();
    }
    
    
    /// <summary>
    /// Cleans then loads a new canvas and panel for use with menus.
    /// </summary>
    public void ResetCanvasPanel()
    {
        DestroyCanvasPanel();
        MenuCanvas = Object.Instantiate(canvasPrefab);
        MenuPanel = Object.Instantiate(panelPrefab);
        MenuPanel.transform.SetParent(MenuCanvas.transform);
    }


    /// <summary>
    /// Destroys the Canvas and Panel when no longer wanted/needed.
    /// </summary>
    public void DestroyCanvasPanel()
    {
        Object.Destroy(MenuPanel);
        Object.Destroy(MenuCanvas);
    }


    /// <summary>
    /// Simplifies showing/hiding this canvas.
    /// </summary>
    /// <param name="value">true to show, false to hide</param>
    public void ShowCanvas(bool value)
    {
        MenuCanvas.SetActive(value);
    }


}
