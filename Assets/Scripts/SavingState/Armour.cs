using System.IO;

[System.Serializable]
public class Armour
{
    public string head;
    public string chest;
    public string legs;

    public Armour() {
        this.head = "";
        this.chest = "";
        this.legs = "";
    }

    public Armour(string head, string chest, string legs) {
        this.head = head;
        this.chest = chest;
        this.legs = legs;
    }
}
