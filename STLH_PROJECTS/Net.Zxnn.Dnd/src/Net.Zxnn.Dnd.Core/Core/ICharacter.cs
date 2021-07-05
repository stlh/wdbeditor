namespace Net.Zxnn.Dnd.Core
{
    public interface ICharacter
    {
        void move();
        void attack(ICharacter target);

        int ArmorClass {
            get {
                return 5;
            }
        }
    }
}