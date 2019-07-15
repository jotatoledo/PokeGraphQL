# PokeGraphQL.Net

All the Pokémon data you'll ever need in one place, easily accessible through a modern GraphQL API.

> The GraphQL API is possible thanks to the awesome [HotChocolate project](https://github.com/ChilliCream/hotchocolate).

> The RESTful API provided by the awesome [PokeAPI project](https://github.com/PokeAPI/pokeapi) is consumed through the [PokeApiNet project](https://github.com/mtrdp642/PokeApiNet).

[![Deploy](https://www.herokucdn.com/deploy/button.svg)](https://heroku.com/deploy)

## Docker

The application is dockerized. To build and run the image execute the following in the solution folder:

```bash
docker build -t <image_name>
docker run -d -p 8080:80 --name <container_name> <image_name>
```

After that the application should be reachable at http://localhost:8080

## Cloud deployment

There are a couple of requirements in the case that the application runs behind a proxy or load balancer
as explained in the [.net core docs](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-2.2#forward-the-scheme-for-linux-and-non-iis-reverse-proxies).

In concrete, the following **env. variables** are relevant:

- (**Required**) `ASPNETCORE_FORWARDEDHEADERS_ENABLED=true`: Turns the forward-header-middleware on. Needed to forward the scheme for Linux and non-IIS reverse proxies.

- (**Optional**) `ASPNETCORE_WEBHOST=<application_domain>`: If the forward-header-middleware is active, it improves the security of the pipeline by restricting the
possible values of the [`X-Forwarded-Host` header](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-2.2#forwarded-headers).

- (**Optional**) `ASPNETCORE_HSTS_ENABLED)=true`: Turns the HSTS-middleware on. The middleware options
are set to the minimal requirements described in https://hstspreload.org/ 


### Heroku

For the following sections its recommended to install the heroku cli in your local machine.
Most of this steps can be performed through the heroku dashboard, but for the sake of simplicity
the CLI is prefered.

First, you will need to login into heroku:

```bash
heroku login
```

After loging in create a new app by running:

```bash
heroku create --name <heroku_app>
```

After the application is created, you need to set some [config vars](https://devcenter.heroku.com/articles/config-vars) 
and add a [buildpack](https://devcenter.heroku.com/articles/buildpacks) compatible with `dotnetcore`. 

The buildpack used in this deployment can be found [here](https://github.com/jincod/dotnetcore-buildpack).
You can add it as follows:

```bash
heroku buildpacks:set jincod/dotnetcore -a <heroku_app>
```

Through the use of config vars we will set some of the env. variables mentioned in the [cloud deployment](#cloud-deployment) section.
This can be done as follows:

```bash
heroku config:set ASPNETCORE_FORWARDEDHEADERS_ENABLED=true -a <heroku_app>
heroku config:set ASPNETCORE_HSTS_ENABLED=true -a <heroku_app>
```