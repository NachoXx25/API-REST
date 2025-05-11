# StreamFlow - Modular monolith
This is the repository of a modular monolith for the fictitious application "StreamFlow" from the subject "Arquitectura de Sistemas" at the Universidad Católica del Norte. All the necessary tools and commands to run the project are described below.

## Pre-requisites
- [.NET SDK](https://dotnet.microsoft.com/es-es/download) (version 9.0.4)
- [MySQL](https://www.mysql.com/) (version 8.0.29) 
- [Git](https://git-scm.com/) (version 2.49.0)

## Installation and configuration

1. **Clone the repository**
```bash
git clone https://github.com/NachoXx25/API-REST.git
```

2. **Navigate to the project directory**
```bash
cd API-REST
```

3. **Restore project dependencies**
```bash
dotnet restore
```

4. **Create a ```.env``` file on the root of the project and fill the environment variables**
```bash
cp .env.example .env
```

In the ```.env``` file, replace:

- ```JWT_SECRET``` with your JWT secret key
- ```MYSQL_CONNECTION``` with the connection string of MySQL
- 
Once you have replaced everything, save the changes and move on to the next step.


5. **Run the project**
```bash
dotnet run
```

The server will start on port **5161** and the **seeders** will be created automatically. Access the API via http://localhost:5161.

## Table of contents
As mentioned in the previous section, the seeders are loaded automatically with the ```dotnet run``` command.

The seeder contains:
- 100 products 

## Authors
- [@Ignacio Valenzuela](https://github.com/NachoXx25)
