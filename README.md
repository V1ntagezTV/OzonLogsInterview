
# Тестовое задание в OZON.
### Сервис хранения логов с использованием
Entity Framework Core - used for data provider. <br/>
Swashbuckle (Swagger) - used for http requests.
____

#### Logging levels:
```
Info = 1,
Debug = 2,
Warning = 3,
Fatal = 4
```
## Requests.
### GET ​/Logs​/Add
```
Add new log to database.
Return new log id property.
(Datetime format ex: 2012-12-31T22:00:00.000Z)
```
```
Parameters
string content - Log text.
string action - Log action.
date date - Log init date.
integer level - Log level.
```
____
### GET ​/Logs​/Remove
```
Remove all logs that id equals given logId.
Return logs count that was deleted from database.
```
```
Parameters:
integer logId - Log id.
```
____
```
Return statistics about current database state.
```
### GET ​/Logs​/GetStats
____
```
Return all logs that suits to given filter parameters.
```
### GET ​/Logs​/SearchByFilter
```
All parameters are unneccessary.<br/> 
Choose parameters that you need to use.
```
```
Parameters:
DateTime dateFrom=default,
DateTime dateTo=default,
string inContent=default, 
string inAction=default,
LogLevel logLevel=default
```
____
### GET ​/Logs​/SearchByDate
```
Returns all logs that happens between given from and to dates.
```
```
Parameters:
DateTime dateFrom=default,
DateTime dateTo=default
```
____
### GET ​/Logs​/SearchInContent
```
Returns all logs that contains string in Content text.
```
```
Parameters:
string inContent
```
____
### GET ​/Logs​/SearchInAction
```
Returns all logs that contains string in Action text.
```
```
Parameters:
string inAction
```
____
### GET ​/Logs​/SearchByLevel
```
Returns all logs where log level equasl given integer.
```
```
Parameters:
integer level
```
____

