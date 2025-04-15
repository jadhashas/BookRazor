# Bibliothèque en ligne - Application Blazor WASM

Cette application web permet de gérer une bibliothèque en ligne. Elle est construite avec Blazor WebAssembly et une API .NET Core.

## 🚀 Fonctionnalités

- ✨ Interface utilisateur moderne et responsive
- 📚 Gestion complète des livres (CRUD)
- 🖼️ Upload et gestion des couvertures de livres
- 🔍 Recherche et filtrage des livres
- 👤 Authentification des utilisateurs
- 🎨 Design moderne avec animations et transitions

## 🛠️ Technologies utilisées

- **Frontend**
  - Blazor WebAssembly
  - Bootstrap 5
  - HTML5/CSS3
  - JavaScript (interop)

- **Backend**
  - ASP.NET Core API
  - Entity Framework Core
  - SQL Server
  - JWT pour l'authentification

## 📋 Prérequis

- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022 ou VS Code

## 🚀 Installation

1. Cloner le repository :
```bash
git clone [URL_DU_REPO]
```

2. Restaurer les packages NuGet :
```bash
dotnet restore
```

3. Configurer la base de données :
```bash
cd BlazorAppWASM.API
dotnet ef database update
```

4. Lancer l'application :
```bash
dotnet run --project BlazorAppWASM.API
dotnet run --project BlazorAppWASM
```

## 🏗️ Structure du projet

```
BlazorAppWASM/
├── BlazorAppWASM/           # Application Blazor WASM
│   ├── Components/          # Composants réutilisables
│   ├── Pages/              # Pages de l'application
│   ├── Services/           # Services et interfaces
│   └── Shared/            # Composants partagés
│
├── BlazorAppWASM.API/      # API Backend
│   ├── Controllers/        # Contrôleurs API
│   ├── Data/              # Contexte et migrations EF
│   └── Models/            # Modèles de données
│
└── BlazorAppWASM.Models/   # Modèles partagés
```

## 📚 Gestion des livres

L'application permet de :
- Ajouter un nouveau livre avec sa couverture
- Modifier les informations d'un livre existant
- Supprimer un livre
- Visualiser la liste des livres avec pagination
- Rechercher des livres par titre, auteur ou ISBN

## 🔐 Authentification

- Système d'authentification basé sur JWT
- Gestion des rôles utilisateurs
- Protection des routes et des fonctionnalités

## 🎨 Interface utilisateur

- Design responsive
- Thème moderne et élégant
- Animations fluides
- Messages de feedback utilisateur
- Gestion des erreurs avec messages explicites

## 📱 Responsive Design

L'application est entièrement responsive et s'adapte à tous les appareils :
- Ordinateurs de bureau
- Tablettes
- Smartphones

## 🤝 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à :
1. Fork le projet
2. Créer une branche pour votre fonctionnalité
3. Commiter vos changements
4. Pousser vers la branche
5. Ouvrir une Pull Request

## 📝 License

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## 📧 Contact

Pour toute question ou suggestion, n'hésitez pas à ouvrir une issue ou à nous contacter.

---
Développé avec ❤️ par [Votre nom/équipe] 
