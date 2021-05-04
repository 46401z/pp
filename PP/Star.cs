using System;

namespace PPProject
{

    public class Star : AbstractObject
    {
        string galaxy;
        string classs;
        decimal mass;
        decimal size;
        decimal lumen;
        int tempreture;

        public Star()
        {

        }

         public string Galaxy
        {
            get { return galaxy; }
            set { galaxy = value; }
        }

        public string Classs
        {
            get { return classs; }
            set { classs = value; }
        }

        public decimal Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        public decimal Size
        {
            get { return size; }
            set { size = value; }
        }

        public decimal Lumen
        {
            get { return lumen; }
            set { lumen = value; }
        }

        public int Tempreture
        {
            get { return tempreture; }
            set { tempreture = value; }
        }

        public void findClass()
        {
            if (
                tempreture >= (decimal)30000 &&
                lumen >= (decimal)30000 &&
                mass >= (decimal)16 &&
                size >= (decimal)6.6)
                this.Classs = "О";

            else if (
                (tempreture > (decimal)10000 && tempreture < (decimal)30000) &&
                (lumen > (decimal) 25 && lumen < (decimal)30000) &&
                (mass > (decimal)2.1 && mass < (decimal)16) &&
                (size > (decimal)1.8 && size < (decimal)6.6)
            )
                this.Classs = "B";

            else
            if (
                (tempreture > (decimal)7500 && tempreture < (decimal)10000) &&
                (lumen > (decimal) 5 && lumen < (decimal)25) &&
                (mass > (decimal)1.4 && mass < (decimal)2.1) &&
                (size > (decimal)1.4 && size < (decimal)1.8)
            )
                this.Classs = "A";

            else
            if (
                (tempreture > (decimal)6000 && tempreture < (decimal)7500) &&
                (lumen > (decimal)1.5 && lumen < (decimal)5) &&
                (mass > (decimal)1.04 && mass < (decimal)1.4) &&
                (size > (decimal)1.15 && size < (decimal)1.4)
            )
                this.Classs = "F";

            else
            if (
                (tempreture > (decimal)5200 && tempreture < (decimal)6000) &&
                (lumen > (decimal)0.6 && lumen < (decimal)1.5) &&
                (mass > (decimal)0.8 && mass < (decimal)1.04) &&
                (size > (decimal)0.96 && size < (decimal)1.15)
            )
                this.Classs = "G";

            else
            if (
                (tempreture > (decimal)3700 && tempreture < (decimal)5200) &&
                (lumen > (decimal)0.08 && lumen < (decimal)0.6) &&
                (mass > (decimal)0.45 && mass < (decimal)0.8) &&
                (size > (decimal)0.7 && size < (decimal)0.96)
            )
                this.Classs = "K";

            else
            if (
                (tempreture > (decimal)2400 && tempreture < (decimal)3700) &&
                (lumen <= (decimal)0.08) &&
                (mass > (decimal)0.08 && mass < (decimal)0.45) &&
                (size <= (decimal)0.7)
            )
                this.Classs = "M";

            else
                throw new Exception("Не може да се намери клас за звездата");
        }
    }
}