# ASP.NET Core IdentityServer4 and Identity
Authorization management using ASP.NET Core Identity and IdentityServer4. This codebase will give an idea how Identity and IdentityServer works side by side using EfCore Sqlite. For simplicity, I have used Sqlite. But also added support for SqlServer.

### Features: 
- EfCore with Sqlite. Configurable to use SqlServer
- ASP.NET Core Identity with EfCore Sqlite
- IdentityServer4 with EfCore Sqlite.
- Apply Migration and Seeds for Identity and IdentityServer4 

### How to run the project: 
Just clone the repo and press F5. It will automatically create AuthorizationManagement.db Sqlite database and populate with some dummy data related to Identity and IdentityServer.

### Get AccessToken from IdentityServer
- Run the app using F5
- Send a request to IdentityServer using POSTMAN. You will receive an AccessToken. By defauly, AccessToken expiration time is 1hour.
![Postman request for AccessToken from IdentityServer](https://user-images.githubusercontent.com/8789577/104222221-31954780-546c-11eb-9089-d7ba8a2c65c7.JPG)

### Issues
- Cannot support both InMemory and DbStore with IdentityServer4

### References : 
- [AccessToken Vs ID Token Vs Refresh Token](https://www.c-sharpcorner.com/article/accesstoken-vs-id-token-vs-refresh-token-what-whywhen/)
- [Set Token timeout in IdentityServer4](https://github.com/IdentityServer/IdentityServer4/issues/857)



