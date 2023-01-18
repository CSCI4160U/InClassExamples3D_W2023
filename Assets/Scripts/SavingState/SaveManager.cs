using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Armour armour = null;

    private string savePath;

    private void Start() {
        savePath = Application.persistentDataPath + "/saveData/";

        Debug.Log("Saving to path: " + savePath);

        this.armour = new Armour();
    }

    [ContextMenu("Save Armour")]
    public void SaveArmour() {
        // save the data
        JSONLoaderSaver.SaveArmourAsJSON(savePath, "armour.json", this.armour);
    }

    [ContextMenu("Load Armour")]
    public void LoadArmour() {
        // load the data
        this.armour = JSONLoaderSaver.LoadArmourFromJSON(savePath, "armour.json");
    }
}
