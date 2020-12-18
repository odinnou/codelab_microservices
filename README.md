# codelab_microservices

Dans Visual Studio 2019 **Tools > Options > Container Tools > Docker Compose**, il est préférable de sélectionner ces options :

![disable run containers on startup](https://affix-test-api.phoceis.com/img/vs_config.png)

## 1 - Configuration via variables d'environnement

Surcharge du appsettings.json via la syntaxe spécifique ASP.NET.

```
curl -X GET 'http://localhost:37001/demo'
```

## 2 - panier Stateless vs panier Stateful

Utilisation d'un cache Redis pour partager de la donnée éphémère.

```
curl -X POST 'http://localhost:37002/panier/stateless' -H 'Content-Type: application/json' --data-raw '{"userId":"fanboy","product":"PS5"}'
curl -X GET 'http://localhost:37003/panier/stateless/fanboy'
```