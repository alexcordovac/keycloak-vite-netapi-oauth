
## Vite + Vue

* `setup API base path in file src/http/api.js`  
* `setup Keycloak base path in file src/keycloak.js`  

1.- `npm install`  
2.- `npm run dev`

## .NET API  
* `setup CORS in Program.cs if needed`

1.- `Run solution using Docker or IIS Express`

## Keycloak + Docker Compose
* `copy keycloak_server_keycloak-data in your volumes of docker`
* `ie. in docker desktop for Windows \\wsl.localhost\docker-desktop\mnt\docker-desktop-disk\data\docker\volumes`

* `change client weather-ui origins if needed`

1- `run command "docker compose up -d" where Compose.yaml file is located`


## Users  
| realm | username | password |
| ------------- | ------------- | ------------- |
| master | admin | admin |
| weather | user_presenter | X |
| weather | user_manager | X |
| weather | user_gold | X |



