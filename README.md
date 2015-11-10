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
        "firstName":"John",
        "lastName":"Doe"
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
    | --- | --- |
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
    | --- | --- | --- |
    | Authorization | bearer {*your access token here*} | false |

    If authorization token is NOT provided then all the albums that are NOT private are returned. If such token is proived, then the result includes all the private album of the authorized user


- **Get album by id** (Optional authorization)
    
    Gets the album by id if it is not private, or if it is private gets it if it is owned by the authenitcated user.
    
    GET api/albums/{id}
    
    *ex. api/albums/1* will return the album with id=1 if it is not private or if it is private and owned by the requesting authorized user.
    
    HEADERS:

    | Header key | Header value | Required |
    | --- | --- | --- |
    | Authorization | bearer {*your access token here*} | false |

- **Get all albums by page** (Optional authorization)

    GET api/albums/all?page={*page number*}&pageSize={*items per page*}

    If no query string is provided defaults to page = 1 & pageSize = 10.
    If no authorization token is provided returns only the non private albums. If such is provided includes the authorized user`s albums also.

    *ex. api/albums/all?page=1&pageSize=2*

    *ex. api/albums/all* **will default to** *api/albums/all?page=1&pageSize=10*

    HEADERS:

    | Header key | Header value | Required |
    | --- | --- | --- |
    | Authorization | bearer {*your access token here*} | false |

- **Get all albums by user** (Optional authorization)
    
    GET api/albums/user?owner={*owner username*}

    Gets all albums that are owned by the provided owner username and are not private. If the authorized user is also the owner gets the private albums as well.

    *ex. api/albums/user?owner=BaiIvan@mail.com*

    HEADERS:

    | Header key | Header value | Required |
    | --- | --- | --- |
    | Authorization | bearer {*your access token here*} | false |

- **Delete album by album id** (Requires authorization)
    
    DELETE api/albums/{id}

    Deletes the album with the specified id if it belongs to the current authorized user.

    HEADERS:

    | Header key | Header value | Required |
    | --- | --- | --- |
    | Authorization | bearer {*your access token here*} | true |