# Image-Gallery

## Image Gallery WebApi

### Users

- __Registers a new user__

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
- __Get authorization token__

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
    
- __Create a new album__ (needs Authorization)

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
- __Get all albums__

    GET: api/albums 

- __Get specific album by album name__

    GET: api/albums/{album name}

    _ex. api/albums/snimki%20kupona_

- __Get all albums by page__

    GET: api/albums/all/{page number}?pageSize={items per page}

    _ex. api/albums/all/1?pageSize=2_
