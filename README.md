# codelab_microservices

Dans Visual Studio 2019 **Tools > Options > Container Tools > Docker Compose**, il est préférable de sélectionner ces options :

![disable run containers on startup](https://i.ibb.co/8DQsPkx/vs-config.png)

## 1 - Configuration via variables d'environnement

Surcharge du appsettings.json via la syntaxe spécifique ASP.NET.

```
curl -X GET 'http://localhost:37001/demo'
```

## 2 - Panier Stateless vs panier Stateful

Utilisation d'un cache Redis pour partager de la donnée éphémère.

```
curl -X POST 'http://localhost:37002/panier/stateless' -H 'Content-Type: application/json' --data-raw '{"userId":"fanboy","product":"PS5"}'
curl -X GET 'http://localhost:37003/panier/stateless/fanboy'
```

### Pour aller plus loin :
1. Ajouter une nouvelle classe de configuration dans **AppSettings.cs**.
2. "Piloter" le contenu de cette nouvelle classe à parir des variables d'environnements.

## 3 - Build, test automatique et depôt docker hub

dotnet build/test, docker build/push

```
docker run --rm --name catalogue -e AppSettings__Ttl=1337 -d -p 8085:80 odinnou/catalogue-api:0.0.5
curl -X GET 'http://localhost:8085/demo'
```

### Pour aller plus loin :
1. Faire un script de build pour "panier-api".
2. Créer une pull-request sur le Github pour ajouter une analyse Sonarcloud.

## 4 - Sécurisation des APIs et token JWT

Ajout du middleware autorisant les appels contenant un token provenant du serveur d'authentification donné.
Lecture du contenu du JWT pour récupérer certaines infos utilisateurs et bloquer quelques APIs en fonction du type d'utilisateur

```
curl -X POST 'http://localhost:37002/panier/stateless' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........' -H 'Content-Type: application/json' --data-raw '{"product":"PS5"}'
curl -X GET 'http://localhost:37003/panier/stateless' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'

curl -X GET 'http://localhost:{port}/check-access/panier-admin' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
curl -X GET 'http://localhost:{port}/check-access/client' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
curl -X GET 'http://localhost:{port}/check-access/tout-le-monde'
```

Les comptes utilisateurs sont les suivants : 

email : client_email_verifie / admin_email_non_verifie / client_email_non_verifie / panier_admin_email_verifie / non_panier_admin_email_verifie ..... @ineat.fr
mot de passe : test123

```
curl -L -X POST 'https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyCfZd9kOFERsYZBvk_30LeiUcsyEAzoyUw' \
-H 'Content-Type: application/json' \
--data-raw '{"email":"client_email_verifie@ineat.fr","password":"test123","returnSecureToken":true}'
```

### Pour aller plus loin :
1. Récupérer depuis les "claims" l'email de l'utilisateur courrant.
2. Regarder dans la doc Firebase comment ajouter un claims personnalisé, pour le récupérer au niveau du JWT.

## 5 - Chaque service, possède ses propres données

Création du volume externe pour garder la donnée après reboot

```
docker volume create --name=formation-sqldata
```

Ajout d'une interface Swagger via un "Building Blocks" (en condition réel, il devrait être déposé sur un nuget privé)
Accessible via : http://localhost:{port}/index.html sur toutes les APIs

Création d'une migration

```
PM> Add-Migration NomDeLaMigration -OutputDir "Infrastructure/Migrations"
```

Accès à l'API de test
```
curl -X GET 'http://localhost:37001/produit'
```

### Pour aller plus loin :
1. Ajouter deux types de famille "ECommerce" / "ClickAndCollect".
2. Créer la migration associé et vérifier le comportement via test d'intégration et F5.