/*****************************************************
*                                                    *
* Travail fait par : Yannick Poirier, POIY04109403   * 
*                                                    *                              
*****************************************************/

using System;
using System.IO;
using Newtonsoft.Json;
namespace PIF1006_tp1
{

    /* Le but de cette classe est de permettre de charger un automate
        à partir d'un fichier Json par défaut pour pouvoir : 
        1- Valider des combinaisons
        2- Afficher la description de l'automate. */

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Automate automate = FichierDefautJson(); // Charge le fichier Json définit par défaut

            if (automate == null) // Boucle qui s'assure que le fichier Json existe et n'est pas null
            {
                return;
            }
            bool quit = false;

            while (!quit) // Si le fichier Json existe, ce menu est présenté
            {
                Console.WriteLine("Bienvenue dans le menu de l'automate! \n");
                Console.WriteLine("1. Essayer une combinaison avec l'automate");
                Console.WriteLine("2. Afficher la description de l'automate");
                Console.WriteLine("3. Quitter le menu\n");

                Console.Write("Veuillez inscrire le numéro (1, 2, 3) de votre choix : \n");
                string choice = Console.ReadLine(); // L'utilisateur choisis ce qu'il veut faire 

                switch (choice) // Selon le choix de l'utilisateur, l'option du menu correspondante est exécuter
                {
                    case "1": // L'utilisateur inscrit la combinaison qu'il veut input à l'automate
                        Console.Write("\nInscrivez la combinaison que vous souhaitez utilisé avec l'automate : \n");
                        string input = Console.ReadLine();
                        bool isValid = automate.Validate(input);
                        Console.WriteLine(isValid ? "Cette combinaison est valide! :)\n" : "Cette combinaison est invalide! :(\n");
                        break;

                    case "2": // Affiche la description de l'automate 
                        Console.WriteLine(automate.ToString());
                        break;

                    case "3": 
                        quit = true;
                        break;

                    default: 
                        Console.WriteLine("Les options possibles du menu sont 1, 2, 3\n");
                        break;
                }
            }
        }
        private static Automate FichierDefautJson()
        {
            string defautJsonFilePath = "AutomatesTxt.json"; // Le chemin pour atteindre le fichier Json. Il est dans le répértoire


            if (File.Exists(defautJsonFilePath)) // S'assure d'avoir accès au fichier Json 
            {
                string jsonContent = File.ReadAllText(defautJsonFilePath);
                return JsonConvert.DeserializeObject<Automate>(jsonContent); // Désérialise le Json en objet "Automate"

            }
            else // Si le chemin vers le fichier est invalide
            {
                Console.WriteLine("Erreur lors de la tentative de récupération du fichier Json."); 
                return null; 
            }
        }
    }
}
