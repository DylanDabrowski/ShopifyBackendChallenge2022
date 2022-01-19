run project:
cd API
dotnet run

consider using 'Postman' to run commands

Add one item:
POST http://localhost:5000/api/item/add
(item pulled from body as json)

Add list of items:
POST http://localhost:5000/api/item/addmany
(items pulled from body as json)

Returns all items unsorted:
GET http://localhost:5000/api/item/unsorted

Returns all items sorted by their Guid:
GET http://localhost:5000/api/item/byid

Returns 1 item by Guid from query:
GET http://localhost:5000/api/item/{id}

Returns all items by name from query:
GET http://localhost:5000/api/item/byname/{name}

Returns all items by type from query:
GET http://localhost:5000/api/item/bytype/{type}

Delete an item by id:
DELETE http://localhost:5000/api/item/{id}

Delete all items in db:
DELETE http://localhost:5000/api/item/deleteall