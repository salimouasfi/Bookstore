# Bookstore

**Bookstore** est une application web dédiée à la gestion et à la présentation de livres, développée avec ASP.NET Core, JavaScript, SCSS et HTML. Ce projet permet aux utilisateurs de consulter, rechercher et organiser des livres en ligne via une interface moderne et réactive.

## Fonctionnalités

- **Catalogue de livres** : Affichage d'une liste de livres avec détails (titre, auteur, résumé, etc.).
- **Recherche et filtrage** : Outils de recherche et filtres avancés pour trouver facilement un livre.
- **Ajout, modification et suppression** : Interface d'administration pour gérer le catalogue.
- **Interface utilisateur moderne** : Utilisation de SCSS pour le style et JavaScript pour l'interactivité.
- **Sécurité et authentification** : Authentification des utilisateurs pour les actions sensibles (admin).

## Technologies utilisées

- **ASP.NET Core** (C#) : Backend & API
- **JavaScript** : Interactivité côté client
- **SCSS** : Styles avancés et responsive design
- **HTML** : Structure des pages

## Prérequis

- [.NET 6+ SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/) (pour la gestion des dépendances JS/SCSS)
- Un SGBD compatible (ex : SQL Server, SQLite)

## Installation

1. **Cloner le dépôt :**
   ```bash
   git clone https://github.com/salimouasfi/Bookstore.git
   cd Bookstore
   ```

2. **Installer les dépendances frontend :**
   ```bash
   npm install
   ```

3. **Configurer la base de données :**
   - Modifier `appsettings.json` pour vos paramètres de connexion.

4. **Appliquer les migrations et démarrer le backend :**
   ```bash
   dotnet ef database update
   dotnet run
   ```

5. **Lancer le serveur de développement frontend (si séparé) :**
   ```bash
   npm start
   ```

6. **Accéder à l'application :**
   - Ouvrez votre navigateur sur [http://localhost:5000](http://localhost:5000) ou l’URL indiquée dans la console.

## Structure du projet

- `Controllers/` : Contrôleurs ASP.NET Core
- `Models/` : Modèles de données
- `Views/` : Vues Razor/HTML
- `wwwroot/` : Ressources statiques (JS, SCSS, images)
- `appsettings.json` : Configuration de l’application

## Contribution

Les contributions sont les bienvenues !  
Pour proposer une fonctionnalité ou corriger un bug :
1. Forkez le projet
2. Créez une branche (`feature/ma-feature`)
3. Soumettez une pull request

## Licence

Ce projet est sous licence MIT.

## Auteur

[Salim Ouasfi](https://github.com/salimouasfi)
