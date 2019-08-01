namespace birgersbolcher.database
{
    public class Item
    {
        public string Name;
        public int Value;

        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public override string ToString() {
            // Generates the text shown in the combo box
            return Name;
        }
    }
    
}