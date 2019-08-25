# BtcExchanger
project created for recruitment process  
# Abstract:
OOP, Functional programming, C#, JS, Python, Docker, Unit Tests, E2E Tests
### backend:
OOP, ASP.Net, C#, webapi, Swagger
### frontend:
Functional programming, JS, React, Redux, React Router, Material UI, Thunk
### Tests:
C#, Xunit, Python, Selenium

# Needs:  
- Download chrome driver for your enviroment and put it in global directory for project as 'chromedriver'.  
```
https://sites.google.com/a/chromium.org/chromedriver/downloads
```
- If You don't use Chrome Web browser beside downloading driver you also need to edit .env and every selenium tests in line:
```
browser = webdriver.Chrome(driver)
```
- python Selenium 
```
pip install selenium --user
```

# Simple commands:
Create .env for selenium tests and docker
```
cp .env.example .env  
```
Create .env with api path for Frontend  
```
cp ./BtcExchanger.Frontend/.env.example ./BtcExchanger.Frontend/.env  
```
Build all docker images:  
```
docker-compose build  
```
Run all in docker:  
```
docker-compose up  
```
Run backend native:  
```
dotnet run --project BtcExchanger  
```
Run backend tests:  
```
dotnet test  
```
Run frontend tests:  
```
cd BtcExchanger.Frontend.Tests  
python Tests_Transaction.py  
python Tests_Verification.py  
```
Run E2E tests:  
```
cd E2E.Tests  
python Tests.py   
```
Run only backend in docker:  
```
docker build -t aspnetapp BtcExchanger  
docker run -d -p 8080:80 --name myapp aspnetapp  
```
Run only frontend:  
```
cd BtcExchanger.Frontend  
npm install  
npm run  
```
Run only frontend in docker:  
```
docker build BtcExchanger.Frontend -t react-docker  
docker run -p 8000:80 react-docker  
```


# Back-end do aplikacji, która sprzedaje BTC.  

# Przebieg aplikacji wygląda następująco:  

  Użytkownik wybiera ile BTC chce sprzedać oraz na jakie konto chce otrzymać pieniądze  
  Użytkownik rejestruje się podając numer telefonu lub adres e-mail, na który otrzymuje wiadomość z kodem do wpisania  
  Po wpisaniu kodu użytkownik otrzymuje informacje o adresie portfela na jakie ma przelać środki  
    
# Podstawowe wymagania:  

  Kod powinien być testowalny oraz powinny zostać wykorzystane dobre praktyki z dziedziny wytwarza oprogramowania  
  Użyj dowolnych technologii do stworzenia aplikacji.  
  Aplikacja to RESTOWE API, z który będzie się komunikowała aplikacja frontowa  
  Aplikacja może w dowolny sposób przetrzymywać dane  
  Aplikacja powinna składać się z co najmniej 2 mikro-serwisów  
  Kod powinien posiadać przykładowe testy jednostkowe oraz E2E (nie trzeba pokrywać aplikacji w 100%)  
  Pomyśl o dodatkowych endpointach, które nie są wymienione w wymaganiach  

# Dodatkowe wymagania:  

  Konteneryzacja rozwiązania  
  Uruchamianie oraz deploy za pomocą jednego polecenia  
  Swagger  