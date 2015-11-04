# Image-Gallery

## Image Gallery WebApi

### Users

- Registers a new user

POST: api/account/register

HEADERs:

| Header Key | Header Value |
|---|---|
| Content-Type | application/json |    

BODY:
```js
{
    "email":"user1@user1.com",
    "password":"123",
    "confirmPassword":"123"
}
```
- Get authorization token

POST: token

BODY: x-www-form-urlencoded

| Header Key | Header Value |
|---|---|
| username | *{your emali}* |
| passwrod | *{your password}* |
| grant_type | password |

In order to authenticate to the WebApis that require authorization (have the [Authorize] attribute) you need to provide the received "access_token" as a header "Authorization" of the Http message:

| Header key | Header value |
| --- | --- |
| Authorization | bearer {*your access token here*} |

-

### Albums
    
- Create a new album - needs Authorization

POST: api/albums

HEADERs:

| Header Key | Header Value |
|---|---|
| Authorization | bearer {*your access token here*} |
| Content-Type | application/json |

BODY:
```js
{
    "Name":"{album name}",
    "Description":"{description}",
}
```
- Get all albums

GET: api/albums 

- Get specific album by album name

GET: api/albums/{album name}
ex. api/albums/snimki%20kupona

- Get all albums by page

GET: api/albums/all/{page number}?pageSize={items per page}
ex. api/albums/all/1?pageSize=2
