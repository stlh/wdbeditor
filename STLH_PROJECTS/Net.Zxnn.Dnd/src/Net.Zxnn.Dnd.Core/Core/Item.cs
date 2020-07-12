namespace Net.Zxnn.Dnd.Core
{
    public abstract class Item
    {
        public QualityLevel Quality { get; set; }
        public Item() : this(QualityLevel.Normal)
        {
        }

        public Item(QualityLevel quality)
        {
            this.Quality = quality;
        }
    }
}