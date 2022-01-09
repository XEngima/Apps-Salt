using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Salt.Cypher
{
    public class TrueRandomGenerator
    {
        private Random _random;
        private Random _plusMinusRandom;
        private Random _spiceRandom;
        private int _mouseChangeCount = 0;
        private string _lastMousePos = "";

        public TrueRandomGenerator()
        {
            int seed = DateTime.Now.Year * DateTime.Now.Month * DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

            Point mousePos = MouseData.GetCursorPosition();

            int seed2 = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Millisecond + (mousePos.X + 1) * (mousePos.Y + 1);
            int spiceSeed = seed - seed2;

            _random = new Random(seed);
            _plusMinusRandom = new Random(seed2);
            _spiceRandom = new Random(spiceSeed);
        }

        public int MouseChangeCount
        {
            get { return _mouseChangeCount; }
        }

        public int GetNextNumber(int maxValue)
        {
            bool goPlus = _plusMinusRandom.Next(100) >= 50;

            int nextValue = _random.Next(maxValue);

            var mousePos = MouseData.GetCursorPosition();

            if (mousePos.ToString() != _lastMousePos)
            {
                _mouseChangeCount++;
                _lastMousePos = mousePos.ToString();
            }

            int spiceNumber = (mousePos.X + 1) * (mousePos.Y + 1) * (DateTime.Now.Millisecond + 1) * _spiceRandom.Next(1000);

            if (goPlus)
            {
                nextValue = nextValue + spiceNumber;
            }
            else
            {
                nextValue = nextValue - spiceNumber;
            }

            if (nextValue < 0)
            {
                int factor = -nextValue / maxValue;
                nextValue += maxValue * factor;

                while (nextValue < 0)
                {
                    nextValue += maxValue;
                }
            }

            if (nextValue > 0)
            {
                int factor = nextValue / maxValue;
                nextValue -= maxValue * factor;

                while (nextValue > maxValue)
                {
                    nextValue -= maxValue;
                }
            }

            return nextValue;
        }
    }
}
