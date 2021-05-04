using System;
using System.Collections.Generic;

namespace PPProject
{
	public class Galaxy : AbstractObject
	{
		string type;
		decimal age;
		string age_meter;

		enum Types {
			elliptical, lenticular, spiral, irregular
		}

		enum AgeMeters
		{
			B, M
		}


		public string Type
		{
			get { return type; }
			set {
				if (Enum.IsDefined(typeof(Types), value))
					type = value;
				else
					throw new Exception("Няма такъв тип галактика");

			}
		}

		public decimal Age
		{
			get { return age; }
			set { age = value; }
		}

		public string AgeMeter
		{
			get { return age_meter; }
			set {
				if (Enum.IsDefined(typeof(AgeMeters), value))
					age_meter = value;
				else
					throw new Exception("Няма такъв разред "+ value + " за възраст на галактика ");
			}
		}

	}
}