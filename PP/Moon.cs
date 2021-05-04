using System;

namespace PPProject
{
    public class Moon : AbstractObject
    {
        string planet;

        public Moon()
        {

        }

        public string Planet
        {
            get { return planet; }
            set { planet = value; }
        }
    }
}