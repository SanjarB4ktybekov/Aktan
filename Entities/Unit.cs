namespace Aktan.Entities
{
    public class Unit
    {
        public Unit()
        {
            UnitNumber = 1;
            isActive = false;
        }
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public bool isActive { get; set; }
    }
}