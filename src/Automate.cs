using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace PIF1006_tp1
{
    public class Automate
    {
        public State InitialState { get; set; } // État initial de l'automate
        public State CurrentState { get; set; } // État actuel de l'automate

        public Automate(State initialState)
        {
            InitialState = initialState;
            CurrentState = initialState; // Initialise CurrentState à l'état initial

            Reset(); // utilisé pour réinitialiser l'automate à son état initial
        }

        // Cette méthode LoadFromFile permet d'utiliser un automate depuis un fichier Json
        public static Automate LoadFromFile(string filePath)
        {
            string jsonFilePath = "AutomatesTxt.json"; // Le chemin pour accéder au fichier Json (il est dans le répertoire)
            if (File.Exists(jsonFilePath)) // S'assure que le fichier existe
            {
                using (StreamReader liseuse = new(jsonFilePath))
                {
                    string json = liseuse.ReadToEnd(); // Lire tout le contenu du fichier Json contenant les caractéristique de l'automate
                    Automate automate = JsonConvert.DeserializeObject<Automate>(json); // Désérialisable 
                    return automate;
                }
            }
            else // Si le fichier est introuvable
            {
                Console.WriteLine("\nImpossible de charger le fichier demandé,veuillez réessayer");
                return null; 
            }
        }

        // Cette méthode permet de valider une entrée saisie par l'utilisateur par rapport à l'automate
        public bool Validate(string input)
        {
            Reset();
            bool accepter = true;

            // Parcours tous les caractères qui ont été saisie en entrée (input de l'utilisateur)
            foreach (char Caractere in input)
            {
                // Recherche la transition correspondante à l'entrée actuelle (Caractere) dans l'état actuel (CurrentState)
                Transition transition = CurrentState.Transitions.Find(t => t.Input == Caractere);

                if (transition != null) // Affiche l'état actuel (input lue et transition effectué)
                {
                    CurrentState = transition.TransiteTo;
                    Console.WriteLine($"État actuel: {CurrentState.Name}, Entrée lue: {Caractere}, État transité: {transition.TransiteTo.Name}");
                }
                else // Si aucune transition possible est trouvé, l'input est invalide 
                {
                    Console.WriteLine($"Aucune transition trouvée pour l'entrée: {Caractere} dans l'état: {CurrentState.Name}");
                    accepter = false; 
                    break;
                }
            }

            // Après avoir parcouru toutes les entrées saisie, on vérifie si l'état actuel est final ou non
            if (!CurrentState.IsFinal)
            {
                Console.WriteLine("L'automate ne s'est pas arrêté sur un état final. Malheureusement, l'entrée est invalide.");
                accepter = false; // Si l'automate ne s'arrête pas sur un état final (donc invalide), retourne faux
            }

            return accepter; // Si l'automate s'arrête sur un état final (donc valide), retourne vrai
        }

        // Méthode pour générer une représentation en chaîne de caractères de l'automate
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Liste des états
            stringBuilder.AppendLine("États de l'automate :");
            foreach (State state in GetStates())
            {
                // Affiche le nom de l'état et son statut final (accepté ou non)
                stringBuilder.AppendLine($"- Nom : {state.Name}, Final : {state.IsFinal}");
            }

            // Table de transitions
            stringBuilder.AppendLine("\nTable de transitions :");
            foreach (State state in GetStates())
            {
                // Parcours les transitions de l'état actuel
                foreach (Transition transition in state.Transitions)
                {
                    // Affiche l'état source, l'état cible et puis aussi l'entrée associée à la transition
                    stringBuilder.AppendLine($"- De l'état {state.Name} à l'état {transition.TransiteTo.Name} avec l'entrée '{transition.Input}'");
                }
            }

            return stringBuilder.ToString();
        }

        // Méthode pour obtenir tous les états accessibles à partir de l'état initial
        private List<State> GetStates()
        {
            List<State> states = new();
            Stack<State> stack = new();
            stack.Push(InitialState);

            while (stack.Count > 0)
            {
                State currentState = stack.Pop();
                states.Add(currentState);

                // Parcours des transitions de l'état actuel
                foreach (Transition transition in currentState.Transitions)
                {
                    if (!states.Contains(transition.TransiteTo))
                    {
                        stack.Push(transition.TransiteTo);
                    }
                }
            }

            return states;
        }

        // Réinitialise l'automate à l'état initial
        public void Reset() => CurrentState = InitialState; 
    }
}
