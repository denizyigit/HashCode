using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace HashCode
{
    class Program
    {
        static string path = @"put\";
        //static string filename = "a_an_example.in.txt";
        static List<ingredient_info> myingredient_info_list = new List<ingredient_info>();
        static List<string> filenameList = new List<string>();

        static void Main(string[] args)
        {
            filenameList.Add("a_an_example.in.txt");
            filenameList.Add("b_basic.in.txt");
            filenameList.Add("c_coarse.in.txt");
            filenameList.Add("d_difficult.in.txt");
            filenameList.Add("e_elaborate.in.txt");


            foreach (var myfilename in filenameList)
            {
                picky_client picky_clients = new picky_client();

                System.Console.WriteLine("-Start Reading -" + myfilename);

                picky_clients = ReadMyInput(path + myfilename);
                Pizza mypizza = CreatePizza(picky_clients);
                WriteOutput(mypizza, path + myfilename);

                System.Console.WriteLine("-Finished Running -" + myfilename);
            }

        }

        static Pizza CreatePizza(picky_client mypicky_client)
        {
            Pizza mypizza = new Pizza();

            foreach (var ingredient in myingredient_info_list)
            {
                if (ingredient.is_loved)
                {

                }
            }

            // for (int i = 0; i < mypicky_client.n_potential_clients; i++)
            // {
            //     client_preference current_client_prefences = mypicky_client.client_preferences[i];
            //     mypizza.ingredient_name_list.AddRange(current_client_prefences.loved_ingredients);
            // }
            // mypizza.ingredient_name_list = mypizza.ingredient_name_list.Distinct().ToList();
            // mypizza.n_ingredients = mypizza.ingredient_name_list.Count();


            return mypizza;

        }



        static picky_client ReadMyInput(string full_path)
        {
            picky_client mypicky_client = new picky_client();


            var InputLine = File.ReadAllLines(full_path);
            mypicky_client.n_potential_clients = int.Parse(InputLine[0]);

            for (int i = 1; i <= mypicky_client.n_potential_clients * 2; i += 2)
            {
                client_preference myclient_preference = new client_preference();
                var loved_ing_line = InputLine[i].Split(' ');

                myclient_preference.n_loved_ingredients = int.Parse(loved_ing_line[0]);
                for (int j = 1; j <= myclient_preference.n_loved_ingredients; j++)
                {
                    myclient_preference.loved_ingredients.Add(loved_ing_line[j]);
                    myingredient_info_list.Add(new ingredient_info(loved_ing_line[j], true));


                }
                var hated_ing_line = InputLine[i + 1].Split(' ');

                myclient_preference.n_hated_ingredients = int.Parse(hated_ing_line[0]);
                for (int j = 1; j <= myclient_preference.n_hated_ingredients; j++)
                {
                    myclient_preference.hated_ingredients.Add(hated_ing_line[j]);
                    myingredient_info_list.Add(new ingredient_info(hated_ing_line[j], false));

                }
                mypicky_client.client_preferences.Add(myclient_preference);

            }
            myingredient_info_list = myingredient_info_list.Distinct().ToList();

            return mypicky_client;

        }

        static void WriteOutput(Pizza mypizza, string full_path)
        {
            string createText = mypizza.n_ingredients.ToString() + " ";

            foreach (var item in mypizza.ingredient_name_list)
            {
                createText += " " + item;
            }

            File.WriteAllText(full_path + "_output.txt", createText);

        }

    }



    //Input File Classes
    public class picky_client
    {
        public picky_client() { }

        public int n_potential_clients { get; set; }
        public List<client_preference> client_preferences { get; set; } = new List<client_preference>();
    }
    public class client_preference
    {
        public int n_loved_ingredients { get; set; }
        public List<string> loved_ingredients { get; set; } = new List<string>();

        public int n_hated_ingredients { get; set; }
        public List<string> hated_ingredients { get; set; } = new List<string>();

    }

    class ingredient_info
    {
        public ingredient_info(string name, bool is_loved)
        {
            ingredient_name = name;
            this.is_loved = is_loved;

        }
        public string ingredient_name { get; set; }

        public bool is_loved { get; set; }

    }



    //Output File Classes

    public class Pizza
    {
        public int n_ingredients { get; set; }
        public List<string> ingredient_name_list { get; set; } = new List<string>();
    }


}
