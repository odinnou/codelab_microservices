# codelab_microservices

Dans Visual Studio 2019 **Tools > Options > Container Tools > Docker Compose**, il est préférable de sélectionner ces options :

![disable run containers on startup](https://i.ibb.co/8DQsPkx/vs-config.png)

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

## 3 - build, test automatique et depôt docker hub

dotnet build/test, docker build/push

```
docker run --rm --name catalogue -e AppSettings__Ttl=1337 -d -p 8085:80 odinnou/catalogue-api:0.0.5
```