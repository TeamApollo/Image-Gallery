# Image-Gallery

## Image Gallery WebApi

### Users

- Registers a new user

    __POST__: api/account/register

    __HEADERs__:

    | Header Key | Header Value |
    |---|---|
    | Content-Type | application/json |    

    __BODY__:
    ```js
    {
        "email":"user1@user1.com",
        "password":"123",
        "confirmPassword":"123"
    }
    ```
- Get authorization token

    __POST__: token

    __BODY__: x-www-form-urlencoded

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
    
- Create a new album (needs Authorization)

    __POST__: api/albums

    __HEADERs__:

    | Header Key | Header Value |
    |---|---|
    | Authorization | bearer {*your access token here*} |
    | Content-Type | application/json |

    __BODY__:
    ```js
    {
        "Name":"{album name}",
        "Description":"{description}",
    }
    ```
- Get all albums

    __GET__: api/albums 

- Get specific album by album name

    __GET__: api/albums/{album name}

    _ex. api/albums/snimki%20kupona_

- Get all albums by page

    __GET__: api/albums/all/{page number}?pageSize={items per page}

    _ex. api/albums/all/1?pageSize=2_
