# LightCRM

Simple Customer Relationship Management API

---

# 🫙How to run using Docker

1. Copy `.env.example`

2. Rename it to `.env`

3. Fill the file

4. `docker compose up -d`

5. API will be available at http://localhost:1001/

---

# 🌐Endpoints

---

## 🔐 AuthController (`/api/auth`)

### POST `/api/auth/register`

**Description:** Register a new user  
**Request body:**

```json
{
  "email": "string (email)",
  "password": "string"
}
```

**Responses:**

- `200 OK` — registration successful

- `400 Bad Request` — registration failed (e.g., user already exists)

---

### POST `/api/auth/login`

**Description:** Authenticate a user and receive tokens  
**Request body:**

```json
{
  "email": "string (email)",
  "password": "string"
}
```

**Response:**

```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```

**Errors:** `400 Bad Request` — invalid credentials

---

### POST `/api/auth/refreshTokens`

**Description:** Refresh JWT tokens  
**Request body:**

```json
{
  "oldRefreshToken": "string"
}
```

**Response:**

```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```

**Errors:** `400 Bad Request` — invalid or expired refresh token

---

## 👤 ClientsController (`/api/clients`)

> ⚠️ Requires `ADMIN` role

### GET `/api/clients/{id}`

**Description:** Get client by ID  
**Path parameter:**

- `id` (GUID)  
  **Response:**

```json
{
  "id": "guid",
  "name": "string",
  "email": "string",
  "phone": "string",
  "orders": [ ... ]
}
```

**Errors:** `400 Bad Request` — client not found

---

### GET `/api/clients`

**Description:** Get all clients  
**Response:**

```json
[
  {
    "id": "guid",
    "name": "string",
    "email": "string",
    "phone": "string",
    "orders": [ ... ]
  }
]
```

---

### POST `/api/clients`

**Description:** Create a new client  
**Request body:**

```json
{
  "name": "string",
  "email": "string",
  "phone": "string"
}
```

**Responses:**

- `200 OK` — client created

- `400 Bad Request` — invalid data or creation failed

---

### PUT `/api/clients/{id}`

**Description:** Update an existing client  
**Path parameter:**

- `id` (GUID)  
  **Request body:**

```json
{
  "name": "string",
  "email": "string",
  "phone": "string"
}
```

**Responses:**

- `200 OK` — client updated

- `400 Bad Request` — update failed

---

### DELETE `/api/clients/{id}`

**Description:** Delete a client  
**Path parameter:**

- `id` (GUID)  
  **Responses:**

- `204 No Content` — client deleted

- `400 Bad Request` — deletion failed

---

## 📦 OrdersController (`/api/orders`)

> ⚠️ Requires `ADMIN` role

### POST `/api/orders`

**Description:** Create a new order  
**Request body:**

```json
{
  "clientId": "guid",
  "items": [
    {
      "productId": "guid",
      "quantity": int
    }
  ]
}
```

**Response:**

```json
{
  "id": "guid",
  "clientId": "guid",
  "createdAt": "datetime",
  "items": [ ... ]
}
```

**Errors:** `400 Bad Request`

---

### GET `/api/orders`

**Description:** Get all orders  
**Response:**

```json
[
  {
    "id": "guid",
    "clientId": "guid",
    "createdAt": "datetime",
    "items": [ ... ]
  }
]
```

---

### GET `/api/orders/{id}`

**Description:** Get a specific order by ID  
**Path parameter:**

- `id` (GUID)  
  **Response:**

```json
{
  "id": "guid",
  "clientId": "guid",
  "createdAt": "datetime",
  "items": [ ... ]
}
```

**Errors:** `400 Bad Request`

---

## 🗂️ Contracts

### `UserRequest`

```json
{
  "email": "string (email)",
  "password": "string"
}
```

### `RefreshTokensRequest`

```json
{
  "oldRefreshToken": "string"
}
```

### `ClientRequest`

```json
{
  "name": "string",
  "email": "string (email)",
  "phone": "string"
}
```

### `OrderRequest`

```json
{
  "clientId": "guid",
  "items": [
    {
      "productId": "guid",
      "quantity": int
    }
  ]
}
```

---

## 🔐`Secrets.json` example

```json
{
    "ConnectionStrings:Default": "Host=localhost;Port=5432;Database=lightcrm;Username=postgres;Password=postgres;",

    "Jwt": {
        "SigningKey": "38abcc5679969df6550f92308102f5b4f8a866b3d4c5865955149303ff6eefd1",
        "Issuer": "caf833b59860f9bed49a0c0561cf6c5cb10470432f8448ac1d9c9f8f54a1070b",
        "Audience": "caf833b59860f9bed49a0c0561cf6c5cb10470432f8448ac1d9c9f8f54a1070b",
        "RefreshTokenExpiresIn": "10080",
        "AccessTokenExpiresIn": "15"
    },

    "Api": {
        "AdminEmail": "admin@admin.com",
        "AdminPassword": "admin"
    }
}
```