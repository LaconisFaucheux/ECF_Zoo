# Application Zoo Arcadia - Partie Back

La partie Back (API) du projet Arcadia a été réalisé avec [ASP.NET 8](https://github.com/dotnet/aspnetcore).

## IDE

Ce projet a été réalisé avec les IDE VS Code, Visual Studio 2022 et Rider (JetBrains).

## Déploiement local

### PRE REQUIS

#### Logiciels

Installation de [Git](https://github.com/LaconisFaucheux/ECF_Zoo)
Installation de [Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/) et plus particulièrement de la charge de travail `ASP.NET`.
Installation de [SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads)

### ETAPES

1. Sous Visual Studio 2022, ouvrez la `Console du Gestionnaire de Package`
2. Exécutez la commande `update-database -Context AuthDbContext` pour exécuter les migrations correspondant à la partie utilisateurs
3. Exécutez la commande `update-database -Context ContextArcadia` pour exécuter les migrations correspondant à la partie zoo
4. Ouvrez le fichier `Migrations\Dataset.sql`
5. Cliquez sur l'icône `Connexion` en haut à gauche, puis sélectionnez la base de données `Arcadia`
6. Exécutez le script SQL (triangle vert en haut à gauche de l'onglet). Le jeu de données est créé et inséré dans la BDD.
7. Pressez `Alt + F5` pour démarrer l'API.
8. Testez vos requêtes avec Swagger. N'oubliez pas de récupérer le token JWT pour les endpoints protégés.
