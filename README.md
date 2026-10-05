# BookApp

En responsiv CRUD-applikation för böcker och favoritcitat med JWT-inloggning.
Frontend i Angular 20, backend i .NET 9 Web API.

**Live:** https://gabriel-bookapp.netlify.app

**API:** https://bookapp-ecd6hbfcchfffsat.swedencentral-01.azurewebsites.net/api

> Första anropet kan ta 10–30 sekunder. API:t och databasen ligger på Azures gratisnivå och "sover" när de inte används.
>
> Alla bok- och citatanrop kräver inloggning, så `/api/Book` direkt i webbläsaren ger 401. Swagger är bara aktiverat lokalt.

---

## Funktioner

- Registrera konto och logga in (JWT)
- Boklista med lägg till, redigera och radera
- **Mina citat** – personliga citat med lägg till, redigera och radera
- Nya användare får fem startcitat vid registrering
- Responsiv layout med mobilmeny (Bootstrap)
- Ikoner med Font Awesome
- Växling mellan ljust och mörkt läge

## Teknik

| Del | Teknik |
|---|---|
| Frontend | Angular 20, Bootstrap 5.3, Font Awesome |
| Backend | .NET 9 Web API, Entity Framework Core |
| Databas | SQL Server (lokalt), Azure SQL Database (produktion) |
| Auth | JWT, BCrypt |
| Hosting | Netlify (frontend), Azure App Service (API) |

## Designval

- **Böcker är gemensamma, citat är personliga.** Uppgiften beskriver "en lista över alla böcker" men "*sina* favoritcitat". Därför har `Quote` ett `UserId` och `Book` inte.
- **Startcitat vid registrering.** Så att en ny användare direkt ser fem citat, men kan ändra och ta bort dem fritt.
- **Interface + service + controller** i backend. Controllern väljer statuskod, servicen gör jobbet.
- **DTO:er** för allt som går in och ut ur API:t. Modellerna, till exempel `User` med lösenordshash, skickas aldrig direkt.
- **Interceptor i Angular** lägger till token i alla anrop. Vid 401 (utgången token) loggas användaren ut automatiskt.

## Säkerhet

- Lösenord hashas med **BCrypt**, inget sparas i klartext.
- JWT valideras på issuer, audience, utgångstid och signatur.
- Alla CRUD-endpoints kräver inloggning (`[Authorize]`).
- Citat filtreras på `userId` **från token**. Försöker man ändra någon annans citat blir svaret 404.
- Login-fel säger inte om det var användarnamnet eller lösenordet som var fel.
- Hemligheter ligger aldrig i repot: lokalt i *user secrets*, i produktion som miljövariabler i Azure.
- CORS släpper bara in frontendens adress.
- Route guards i Angular är bara för användarupplevelsen. Det riktiga skyddet sker i API:t.

## Projektstruktur

```
BookAPI/            .NET 9 Web API
  Controllers/      AuthController, BookController, QuoteController
  Service/          AuthService, TokenService, BookService, QuoteService (+ Interface/)
  DTO/              Request- och response-DTO:er
  Models/           User, Book, Quote
  Data/             AppDbContext
  Migrations/
frontend/           Angular 20
  src/app/pages/          login, register, book-list, book-form, quotes
  src/app/services/       auth, book, quote
  src/app/guards/         auth guard
  src/app/interceptors/   auth interceptor
```

## Köra lokalt

**Krav:** .NET 9 SDK, Node.js 20.19+, SQL Server (eller LocalDB)

### 1. Backend

`appsettings.json` finns inte i repot. Skapa `BookAPI/appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookAppDb;Trusted_Connection=True;TrustServerCertificate=true"
  },
  "Jwt": {
    "Issuer": "BookAPI",
    "Audience": "BookApp",
    "ExpireMinutes": 60
  },
  "AllowedOrigins": [ "http://localhost:4200" ],
  "AllowedHosts": "*"
}
```

Lägg JWT-nyckeln i user secrets (minst 32 tecken):

```
cd BookAPI
dotnet user-secrets set "Jwt:Key" "din-långa-slumpmässiga-nyckel-minst-32-tecken"
```

Starta API:t. Databasen och tabellerna skapas automatiskt vid start:

```
dotnet run --launch-profile https
```

API:t körs på `https://localhost:7142`, med Swagger på `/swagger`.

### 2. Frontend

```
cd frontend
npm install
ng serve
```

Öppna `http://localhost:4200`.

## API

| Metod | Endpoint | Kräver inloggning |
|---|---|---|
| POST | `/api/Auth/register` | Nej |
| POST | `/api/Auth/login` | Nej |
| GET, POST | `/api/Book` | Ja |
| GET, PUT, DELETE | `/api/Book/{id}` | Ja |
| GET, POST | `/api/Quote` | Ja |
| PUT, DELETE | `/api/Quote/{id}` | Ja |
