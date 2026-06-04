
# Simulateur d'automate fini — PIF1006

Application console en C# qui charge un automate fini déterministe depuis un fichier JSON et valide des chaînes de caractères selon ses transitions.

## Fonctionnalités

- Chargement d'un automate depuis un fichier `AutomatesTxt.json`
- Validation interactive de combinaisons avec affichage de chaque transition
- Affichage de la table de transitions et des états de l'automate
- Détection des états finaux pour déterminer la validité d'une entrée

## Concepts démontrés

- Automate fini déterministe (AFD)
- Désérialisation JSON (Newtonsoft.Json)
- Programmation orientée objet en C#

## Technologies

- C# / .NET 6+
- Newtonsoft.Json

## Prérequis

- .NET 6+
- Package NuGet : `Newtonsoft.Json`

## Lancer le projet

```bash
dotnet run
```

Le fichier `AutomatesTxt.json` doit être présent dans le répertoire d'exécution.

## Structure

```
Program.cs          — point d'entrée et menu interactif
Automate.cs         — logique de validation et chargement JSON
State.cs            — modèle d'un état
Transition.cs       — modèle d'une transition
AutomatesTxt.json   — définition de l'automate par défaut
```

---

Projet universitaire solo — cours PIF1006, UQTR.
