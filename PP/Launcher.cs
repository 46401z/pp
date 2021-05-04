using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

namespace PPProject
{
    class Launcher
    {
        static List<Galaxy> galaxies = new List<Galaxy>();
        static List<Star> stars = new List<Star>();
        static List<Planet> planets = new List<Planet>();
        static List<Moon> moons = new List<Moon>();
        static string command_patern = @"(?<=\()[^[\]]+(?=\))|(?<=\[)[^[\]]+(?=\])|[^[\]\s]+";
        static Common common = new Common();

        static void test()
        {

            execCommand("add galaxy [млечен път] spiral 12.5B");

            //Add stars
            execCommand("add star [млечен път] слънце1 12 17 31000 31000");
            execCommand("add star [млечен път] слънце2 6.5 6.5 29000 29000");
            execCommand("add star [млечен път] слънце3 1.5 1.5 24 7600");
            execCommand("add star [млечен път] слънце4 1.2 1.3 4 6500");
            execCommand("add star [млечен път] слънце5 1.1 1.01 0.7 5500");
            execCommand("add star [млечен път] слънце5 0.8 0.5 0.5 3800");
            execCommand("add star [млечен път] слънце7 0.5 0.4 0.07 2500");

            //Add planets
            execCommand("add planet слънце1 planeta1 terrestrial true");
            execCommand("add planet слънце1 planeta2 [giant planet] true");
            execCommand("add planet слънце1 planeta3 [ice giant] true");
            execCommand("add planet слънце1 planeta4 mesoplanet true");
            execCommand("add planet слънце1 planeta5 mini-neptune true");
            execCommand("add planet слънце1 planeta6 planetar false");
            execCommand("add planet слънце1 planeta7 super-earth false");
            execCommand("add planet слънце1 planeta8 super-jupiter false");
            execCommand("add planet слънце1 planeta9 sub-earth false");

            //Add moons
            execCommand("add moon planeta1 луна");

            execCommand("print galaxy [млечен път]");

            execCommand("print star слънце1");
            execCommand("print planet planeta1");

        }

        static string[] parseCommand(string str)
        {
            List<string> el = new List<string>();

            Regex regex = new Regex(command_patern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection results = regex.Matches(str);
            foreach (Match match in results)
                el.Add(match.Value.Trim());

            return el.ToArray();
        }

        static string ReadCommand()
        {
            return Console.ReadLine();
        }

        static void execCommand(string command)
        {

            //Console.WriteLine("Изпълни команда:\n{0}", command);
            string[] aCommand = parseCommand(command);

            string main_command = aCommand[0].Trim();
            string sub_command = null;
            if (aCommand.Length > 1)
                sub_command = aCommand[1].Trim();

            if (main_command == "exit")
            {
                return;
            }
            else if (main_command == "help")
            {
                Help();
            }
            else if (main_command == "stats")
            {
                ShowStats();
            }
            else if (main_command == "add")
            {
                if (ValidateAddCommandParams(aCommand, sub_command))
                {
                    switch (sub_command)
                    {
                        case "galaxy":
                            addGalaxy(aCommand);
                            break;
                        case "star":
                            addStar(aCommand);
                            break;
                        case "planet":
                            addPlanet(aCommand);
                            break;
                        case "moon":
                            addMoon(aCommand);
                            break;
                        default:
                            Console.WriteLine("Невалидна команда");
                            break;
                    }
                }
            }
            else if (main_command == "list")
            {
                switch (sub_command)
                {
                    case "galaxies":
                        listGalaxies();
                        break;
                    case "stars":
                        listStars();
                        break;
                    case "planets":
                        listPlanets();
                        break;
                    case "moons":
                        listMoons();
                        break;

                    default:
                        Console.WriteLine("Невалидна команда");
                        break;
                }
            }
            else if (main_command == "print")
            {
                //if (ValidatePrintCommandParams(aCommand, sub_command))
                {
                    switch (sub_command)
                    {
                        case "galaxy":
                            printGalaxy(aCommand[2]);
                            break;
                        case "star":
                            printStar(aCommand[2]);
                            break;
                        case "planet":
                            printPlanet(aCommand[2]);
                            break;
                        default:
                            Console.WriteLine("Невалидна команда");
                            break;
                    }
                }
            }
            else
                Console.WriteLine("Невалидна команда");
        }

        static bool ValidateAddCommandParams(string[] aCommand, string command)
        {
            bool result = false;
            switch (command)
            {
                case "galaxy":
                    if (aCommand.Length == 5)
                        result = true;
                    else
                        Console.WriteLine("Невалиден брой параметри за команда " + command);

                    break;
                case "star":
                    if (aCommand.Length == 8)
                        result = true;
                    else
                        Console.WriteLine("Невалиден брой параметри за команда " + command);

                    break;
                case "planet":
                    if (aCommand.Length == 6)
                        result = true;
                    else
                        Console.WriteLine("Невалиден брой параметри за команда " + command);

                    break;
                case "moon":
                    if (aCommand.Length == 4)
                        result = true;
                    else
                        Console.WriteLine("Невалиден брой параметри за команда " + command);
                    break;

                default:
                    Console.WriteLine("Невалидна команда " + command);
                    break;
            }

            return result;
        }

        static void addGalaxy(string[] data)
        {
            try
            {
                Galaxy galaxy = new Galaxy();
                galaxy.Name = (string)data[2];
                galaxy.Type = (string)data[3];
                galaxy.Age = common.ParseDecimal(Regex.Match((string)data[4], @"\d+.+\d").Value);
                galaxy.AgeMeter = Regex.Match((string)data[4], @"[A-Z]+").Value;
                galaxies.Add(galaxy);

                Console.WriteLine("Успешно добавяне на галактика:\nИме {0} Тип: {1} Земни години: {2}{3}",
                    galaxy.Name, galaxy.Type, galaxy.Age, galaxy.AgeMeter);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void addStar(string[] data)
        {
            //[galaxy] [name] [size] [mass] [luminance] [tempreture]
            try
            {
                Star star = new Star();
                star.Galaxy = (string)data[2];
                star.Name = (string)data[3];
                star.Size = common.ParseDecimal((string)data[4]);
                star.Mass = common.ParseDecimal((string)data[5]);
                star.Lumen = common.ParseDecimal((string)data[6]);
                star.Tempreture = common.ParseInteger((string)data[7]);
                star.findClass();
                stars.Add(star);
                Console.WriteLine("Успешно добавяне на звезда:\nИме: {0} Галактика: {1} Размер: {2} Маса: {3} Светимост: {4} Температура: {5} Клас: {6}",
                    star.Name, star.Galaxy, star.Size, star.Mass, star.Lumen, star.Tempreture, star.Classs);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void addPlanet(string[] data)
        {
            try
            {
                //[star] [name] [type] [support life]
                Planet planet = new Planet();
                planet.Star = (string)data[2];
                planet.Name = (string)data[3];
                planet.Type = (string)data[4];
                planet.Life = common.ParseBool((string)data[5]);

                planets.Add(planet);
                Console.WriteLine("Успешно добавяне на планета:\nИме: {0} Звезда: {1} Тип: {2} Живот: {3}",
                    planet.Name, planet.Star, planet.Type, planet.Life ? "Да" : "Не");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void addMoon(string[] data)
        {
            try
            {
                //[planet] [name]
                Moon moon = new Moon();
                moon.Planet = (string)data[2];
                moon.Name = (string)data[3];
                moons.Add(moon);

                Console.WriteLine("Успешно добавяне на луна:\nИме: {0} Планета: {1}", moon.Name, moon.Planet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void printGalaxy(string str)
        {

            Console.WriteLine("--- Data for {0} galaxy ---", str);
            try
            {
                Galaxy galaxy = galaxies.Find(galaxy => galaxy.Name == str);
                if (galaxy != null)
                {
                    Console.WriteLine("Име на галактика: {0}", galaxy.Name);
                    Console.WriteLine("Тип на галактика: {0}", galaxy.Type);
                    Console.WriteLine("Възраст на галактика: {0}", galaxy.Age);

                    List<Star> aStars = stars.FindAll(star => star.Galaxy == galaxy.Name).OrderBy(star => star.Classs).ToList();

                    if (aStars != null && aStars.Count > 0)
                    {
                        Console.WriteLine("Звезди:");
                        foreach (Star star in aStars)
                        {

                            Console.WriteLine("\tИме: {0}", star.Name);
                            Console.WriteLine("\tКлас: {0} ({1}, {2}, {3}, {4})", star.Classs, star.Size, star.Mass, star.Lumen, star.Tempreture);

                            List<Planet> aPlanets = planets.FindAll(planet => planet.Star == star.Name);
                            if (aPlanets != null && aPlanets.Count > 0)
                            {
                                Console.WriteLine("\tПланети:");

                                foreach (Planet planet in aPlanets)
                                {
                                    Console.WriteLine("\t\tИме: {0}", planet.Name);
                                    Console.WriteLine("\t\tТип: {0}", planet.Type);
                                    Console.WriteLine("\t\tНаличие на живот: {0}", planet.Life ? "Да" : "Не");

                                    List<Moon> aMoons = moons.FindAll(moon => moon.Planet == planet.Name);
                                    if (aMoons != null && aMoons.Count > 0)
                                    {
                                        Console.WriteLine("\t\tЛуни:");
                                        foreach (Moon moon in aMoons)
                                            Console.WriteLine("\t\t\tИме: {0}", moon.Name);

                                    }
                                    else
                                        Console.WriteLine("\t\tЛуни: none");
                                }
                            }
                            else
                                Console.WriteLine("\tПланети: none");
                        }
                        }
                        else
                            Console.WriteLine(" Звезди: none");
                }
                else
                    Console.WriteLine("Галактики: none");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("---End of data for {0} galaxy---", str);

        }

        static void printStar(string str)
        {
            Star star = stars.Find(star => star.Name == str);
            if ( star != null )
            {
                Console.WriteLine("Име: {0}", star.Name);
                Console.WriteLine("Клас: {0} ({1}, {2}, {3}, {4})", star.Classs, star.Size, star.Mass, star.Lumen, star.Tempreture);

                List<Planet> aPlanets = planets.FindAll(planet => planet.Star == star.Name);
                if (aPlanets != null && aPlanets.Count > 0)
                {
                    Console.WriteLine("Планети:");

                    foreach (Planet planet in aPlanets)
                    {
                        Console.WriteLine("\tИме: {0}", planet.Name);
                        Console.WriteLine("\tТип: {0}", planet.Type);
                        Console.WriteLine("\tНаличие на живот: {0}", planet.Life ? "Да" : "Не");

                        List<Moon> aMoons = moons.FindAll(moon => moon.Planet == planet.Name);
                        if (aMoons != null && aMoons.Count > 0)
                        {
                            Console.WriteLine("\tЛуни:");
                            foreach (Moon moon in aMoons)
                                Console.WriteLine("\t\tИме: {0}", moon.Name);

                        }
                        else
                            Console.WriteLine("\tЛуни: none");
                    }
                }
                else
                    Console.WriteLine("Планети: none");
            }
        }
        
        static void printPlanet(string str)
        {
            Planet planet = planets.Find(planet => planet.Name == str);
            if( planet != null)
            {
                Console.WriteLine("Име: {0}", planet.Name);
                Console.WriteLine("Тип: {0}", planet.Type);
                Console.WriteLine("Наличие на живот: {0}", planet.Life ? "Да" : "Не");

                List<Moon> aMoons = moons.FindAll(moon => moon.Planet == planet.Name);
                if (aMoons != null && aMoons.Count > 0)
                {
                    Console.WriteLine("\tЛуни:");
                    foreach (Moon moon in aMoons)
                        Console.WriteLine("\t\tИме: {0}", moon.Name);

                }
                else
                    Console.WriteLine("\tЛуни: none");

            }
        }
        
        static void listGalaxies()
        {
            Console.WriteLine("--- List of galaxies ---");

            foreach (Galaxy galaxy in galaxies)
                Console.WriteLine(galaxy.Name + " " + galaxy.Type + " " + galaxy.Age);

            Console.WriteLine("--- End list of galaxies ---");
        }

        static void listStars()
        {
            Console.WriteLine("--- List of stars ---");
            foreach (Star star in stars)
                Console.WriteLine(star.Name);

            Console.WriteLine("--- End list of stars ---");
        }

        static void listPlanets()
        {
            Console.WriteLine("--- List of planets ---");

            foreach (Planet planet in planets)
                Console.WriteLine(planet.Name);

            Console.WriteLine("--- End list of planets ---");
        }


        static void listMoons()
        {
            Console.WriteLine("--- List of moons ---");

            foreach (Moon moon in moons)
                Console.WriteLine(moon.Name);

            Console.WriteLine("--- End list of moons ---");
        }

        static void ShowStats()
        {

            Console.WriteLine("--- Stats ---");
            Console.WriteLine("Галактики:" + galaxies.Count);
            Console.WriteLine("Звезди:" + stars.Count);
            Console.WriteLine("Планети:" + planets.Count);
            Console.WriteLine("Луни:" + moons.Count);
            Console.WriteLine("--- End of Stats ---");
        }



        static void Help()
        {
            string help = "add galaxy [name] [type] [age]\n";
            help += "add star [galaxy] [name] [size] [mass] [luminance] [tempreture]\n";
            help += "add planet [star] [name] [type] [support life]\n";
            help += "add moon [planet] [name]\n";
            help += "list galaxies\n";
            help += "list stars\n";
            help += "list planets\n";
            help += "list moons \n";
            help += "print galaxy [galaxy] \n";
            help += "print star [star] \n";
            help += "print planet [planet] \n";
            help += "stats\n";
            help += "exit\n";

            Console.WriteLine("Списък с наличните команди и техните параметри\n\n{0}", help);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string name = "Илко Кръстев";
            string fNumber = "46401z";
            Console.WriteLine("Програмният код е подготвен от {0}, факултетен номер {1}", name, fNumber);
            Console.WriteLine("Моля въведете команда или help за списък от команди: ");
            test();

            string command = "";
            while (true && command != "exit")
            {
                command = ReadCommand();
                if (command.Trim() != "")
                    execCommand(command);
            }
        }
    }
}
