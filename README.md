# codelab_microservices

Dans Visual Studio 2019 **Tools > Options > Container Tools > Docker Compose**, il est préférable de sélectionner ces options :

![disable run containers on startup](https://affix-test-api.phoceis.com/img/vs_config.png)

## 1 - Configuration via variables d'environnement

Surcharge du appsettings.json via la syntaxe spécifique ASP.NET.

```
curl -X GET 'http://localhost:37001/demo'
```

### Pour aller plus loin :
1. Ajouter une nouvelle classe de configuration dans **AppSettings.cs**.
2. "Piloter" le contenu de cette nouvelle classe à parir des variables d'environnements.