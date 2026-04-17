🍽️ RestaurantOrderAPI
API REST desenvolvida em ASP.NET Core para gerenciamento de pedidos de restaurante, com autenticação JWT, banco de dados MongoDB na nuvem e deploy em container Docker.

🚀 Acesse a API

🔗 https://restaurantorderapi.onrender.com/swagger

📸 Preview
⚙️ Funcionalidades

✔️ Cadastro e gerenciamento de produtos
✔️ Criação de pedidos com múltiplos itens
✔️ Cálculo automático do total do pedido
✔️ Atualização de status (Pending, Completed, etc.)
✔️ Autenticação e autorização com JWT
✔️ Persistência com MongoDB Atlas

🛠️ Tecnologias
ASP.NET Core (.NET 8)
MongoDB Atlas
MongoDB.Driver
JWT Authentication
Docker
Render (deploy em nuvem)
Swagger
🧠 Arquitetura
Controllers → Services → MongoDbService → MongoDB
             ↓
            DTOs
🔌 Endpoints
🛒 Produtos
POST /products → Criar produto
GET /products → Listar produtos
DELETE /products/{id} → Remover produto
📦 Pedidos
POST /orders → Criar pedido
GET /orders → Listar pedidos
PUT /orders/{id}/status → Atualizar status
🔐 Autenticação

A API utiliza JWT (JSON Web Token).

No Swagger, utilize:

Bearer SEU_TOKEN_AQUI
🐳 Rodando com Docker
Build da imagem:
docker build -t restaurant-api .
Executar container:
docker run -d -p 8080:10000 \
-e MongoSettings__ConnectionString="SUA_STRING" \
-e MongoSettings__DatabaseName="RestaurantOrderDB" \
-e JwtSettings__Secret="SUA_CHAVE" \
-e JwtSettings__Issuer="RestaurantAPI" \
-e JwtSettings__Audience="RestaurantAPI" \
--name restaurant-container restaurant-api

Acesse:

http://localhost:8080/swagger
⚙️ Configuração

Exemplo de appsettings.json:

"MongoDbSettings": {
  "ConnectionString": "SUA_STRING_DO_MONGO",
  "DatabaseName": "RestaurantOrderDB"
},
"JwtSettings": {
  "Secret": "SUA_CHAVE_SECRETA",
  "Issuer": "RestaurantAPI",
  "Audience": "RestaurantAPI"
}
☁️ Deploy

A API está hospedada no Render, utilizando container Docker.

📌 Melhorias futuras

Testes automatizados

Paginação de resultados

Logs estruturados

Cache com Redis

CI/CD pipeline

👩‍💻 Autora

Naime Lima

⭐ Contribuição

Sinta-se à vontade para abrir issues ou enviar pull requests.
