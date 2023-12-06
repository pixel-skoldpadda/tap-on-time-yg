namespace Generator
{
    public class Quarter
    {
        private readonly int _minAngle;
        private readonly int _maxAngle;

        public Quarter(int minAngle, int maxAngle)
        {
            _minAngle = minAngle;
            _maxAngle = maxAngle;
        }

        public int MinAngle => _minAngle;
        public int MaxAngle => _maxAngle;
    }
}