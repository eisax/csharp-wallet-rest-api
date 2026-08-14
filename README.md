# csharp-wallet-rest-api


dotnet ef migrations add AddFirstNameToUser 

dotnet ef database update  

dotnet build

dotnet run  

curl -X POST \
  "http://localhost:5141/api/wallets/a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546/deposit" \
  -H "Content-Type: application/json" \
  -d '{
    "amount": 500,
    "reference": "a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546"
  }'


curl -X POST \
  "http://localhost:5141/api/wallets/a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546/withdrawal" \
  -H "Content-Type: application/json" \
  -d '{
    "amount": 50,
    "reference": "a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546"
  }'



curl -X POST \
  "http://localhost:5141/api/wallets" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Josphat",
    "lastName": "Ndhlovu",
    "phoneNumber": "0770000000"
  }'


a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546