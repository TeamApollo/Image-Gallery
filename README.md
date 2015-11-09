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
    
- **Create a new album** (Required authorization)

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
- **Get all albums** (Optional authorization)

    GET api/albums 

    HEADERS:

    | Header key | Header value | Required |
    | --- | --- |
    | Authorization | bearer {*your access token here*} | false |

    If authorization token is NOT provided then all the albums that are NOT private are returned. If such token is proived, then the result includes all the private album of the authenticated user


- **Get album by id** (Optional authorization)
    
    Gets the album by id if it is not private, or if it is private gets it if it is owned by the authenitcated user.
    
    GET api/albums/{id}
    
    _ex. api/albums/1_
    
    HEADERS:

    | Header key | Header value | Required |
    | --- | --- |
    | Authorization | bearer {*your access token here*} | false |

- **Get all albums by page**

    GET api/albums/all?page={*page number*}&pageSize={*items per page*}

    If no query string is provided defaults to page = 1 & pageSize = 10

    _ex. api/albums/all?page=1&pageSize=2_

    _ex. api/albums/all_ will default to _api/albums/all?page=1&pageSize=10_



    
