# API Products
API responsible for returning products

## About

This project contains the source code of the API responsible for returning the products that are marketed by Prudential according to the partner / customer.

This source code follows good practices and clean architecture


## Environment Variables

The following environment variables are needed to run the application:

* `DATABASE_CONNECTION_STRING` - Connection string with relational database.
* `REDIS_CONNECTION_STRING` - Connection string to redis database.
* `REDIS_INSTANCE_NAME` - Redis database instance name.
* `JWT_TOKEN` - Security key for JWT token signing.


## Running the service

### Requirements

- Setup Envirolment Variables via `export` command or via `appsettings.json` file on the SRC/InterfaceAdapters path of the repo
- DotNet cli (Core SDK)
- DotNet 3.1+
- Node.js v8+
- Npm v6+
- Yarn v1.21+

### Install dependencies
```
  npm install && npm run restore
```

### Start Server
```
  npm start
```

### Unit Tests
```
  npm test
```
You can check the results on the prompt or open the coverage folder, inside it, open index.html in any browser.
