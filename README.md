# ASP.NET Core IdentityServer4 and Identity
Authorization management using ASP.NET Core Identity and IdentityServer4. This codebase will give an idea how Identity and IdentityServer works side by side using EfCore Sqlite. For simplicity, I have used Sqlite. But also added support for SqlServer.

### Features: 
- EfCore with Sqlite. Configurable to use SqlServer
- ASP.NET Core Identity with EfCore Sqlite
- IdentityServer4 with EfCore Sqlite. Configurable to use InMemory store
- Apply Migration and Seeds for Identity and IdentityServer4 

### How to run the project: 
Just clone the repo and press F5. It will automatically create AuthorizationManagement.db Sqlite database and populate with some dummy data related to Identity and IdentityServer.

### References : 
- [AccessToken Vs ID Token Vs Refresh Token](https://www.c-sharpcorner.com/article/accesstoken-vs-id-token-vs-refresh-token-what-whywhen/)



