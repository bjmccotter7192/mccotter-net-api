# mccotter-net-api

## Welcome to the mccotter.net API

### Pre-Reqs
1. Make sure you have dotnet 5 installed on your machine
2. clone project ```git clone <git-repo-link>```

### How to run locally through CLI

1. Navigate to mccotter-net-api folder you cloned
2. ```dotnet restore```
3. ```dotnet build```
4. ```dotnet run```

### How to run using Visual Studio Code
1. Open mccotter-net-api folder in VS Code
2. Say "yes" to the prompt asking to add files to your project
    - This will help when launching the project through VS Code (added .vscode)
3. Open debug console on left side (Play button with bug on it)
4. Make sure ".Net Core Launch (web)" is selected
5. Press run and enjoy!

Access your running API on https://localhost:5001/ over HTTPS or http://localhost:5000 over HTTP

<hr>

## Run With Docker
1. ```docker build -t <image-name> .```
2. ```docker run -dit -p 5000:5000 -p 44319:44319 -name <container-name> <image-name>```

## Run with Docker-Compose
1. ```docker-compose up -d```

Access your running API on http://localhost:44319/ for the swagger UI or use http://localhost:5000/ for all routes without swagger UI.