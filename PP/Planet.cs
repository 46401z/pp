using System;

namespace PPProject
{
    public class Planet : AbstractObject
    {
        string star;
        string type;
        bool life;

        string[] types = new string[] {
                 "terrestrial",
                 "giant planet",
                 "ice giant",
                 "mesoplanet",
                 "mini-neptune",
                 "planetar",
                 "super-earth",
                 "super-jupiter",
                 "sub-earth"
             };
        string[] moons;

        public Planet()
        {

        }

        public string Star
        {
            get { return star; }
            set { star = value; }
        }

        public override string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set {
                if(Array.IndexOf(types, value) >= 0)
                    type = value;
                else
                    throw new Exception("Няма такъв тип планета");

            }
        }

        public bool Life
        {
            get { return life; }
            set { life = value; }
        }

        public string[] Moons
        {
            get { return moons; }
            set { moons = value; }
        }

    }
}