using System;
namespace PPProject
{
    public abstract class AbstractObject
    {
        public string name;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
