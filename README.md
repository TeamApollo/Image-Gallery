# Image-Gallery

## Image Gallery WebApi

### Users

- **Register a new user**

    POST api/account/register

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
- **Get authorization token**

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
    
- **Create a new album** (needs Authorization)

    POST api/albums

    HEADERs

    | Header Key | Header Value |
    |---|---|
    | Authorization | bearer {*your access token here*} |
    | Content-Type | application/json |

    BODY
    ```js
    {
        "Name":"{album name}", // required
        "Description":"{description}",
        "Private": "{true | false}",
    }
    ```
- **Get all non-private albums**

    GET api/albums 

- **Get album by id** (Needs authorization)

    Gets the album if it is not private or if it is private - gets it if it is owned by the authenticated user
    
    GET api/albums/{id}
    
    _ex. api/albums/1_
    
    HEADERS:

    | Header key | Header value |
    | --- | --- |
    | Authorization | bearer {*your access token here*} |

- **Get all albums by page**

    GET api/albums/all?page={*page number*}&pageSize={*items per page*}

    If no query string is provided defaults to page = 1 & pageSize = 10

    _ex. api/albums/all?page=1&pageSize=2_

    _ex. api/albums/all_ will default to _api/albums/all?page=1&pageSize=10_



    
