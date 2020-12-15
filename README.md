# ЛР 4. Корзун Адам 953505

### Выполнения условий:
1) Выбрана и восстановлена бд AventureWorks2019 ✔️
2) Выбраны 5 таблиц - Person.Person, Person.PersonPhone, Production.Product, Production.ProductInventory, Sales.CreditCard. Первые 2 свзяны c помоью BusinessEntityID, следующие 2 связаны с помощью ProductID ✔️
3) Разработаны 5 хранимых процедур; пример процедуры: ✔️
```
USE [AdventureWorks2019]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetPhoneByBEIDF]
	@beid int
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT PhoneNumber, BusinessEntityID
	FROM AdventureWorks2019.Person.PersonPhone
	WHERE BusinessEntityID = @beid
```
4) Разработан слой AccessLayer, в котором находятся 5 интерфейсов, которые реализованы в моделях ✔️
5) Разработан слой ServiceLayer(ServiceModel) в котором находится строка подключения к базе данных, которая берется из ServiceModel.json с помощью ConfigManager из 3 лабы ✔️
6) Все находится а разных проектах ✔️
7) Все модели находятся в отдельном проекте ✔️
8) Разработан XmlGenerator, который создает XML документ модели FileModel, вкоторой находятся все модели; создается XSD схема по XML документу ✔️
9) В XmlGenerator указывается путь создания файла и схемы, который может вести к SourceDirectory из 2 лабы,  так что не совсем понятно зачем нам сервис  (❌✔️)?
10) Конфиг берется из ServiceModel.json ✔️
11) Конфиг берется из ServiceModel.json ✔️
12) В решении находится 7 проектов, которые относятсся к 4 лабе ✔️
13) Создана база данных LogDB, в которой есть таблица Errors  с полями ErrorText, Timestamp, хранимая процедура AddError ✔️
```
USE [LogDB]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AddError] 
	-- Add the parameters for the stored procedure here
	@errorText nvarchar(100),
	@timest time(7)
AS
BEGIN

	INSERT INTO Errors(ErrorMessage, Timestamp)
	VALUES (@errorText, @timest)
END
GO

```
14) Создан слой LogAccessLayer, в котором есть функция добавления ошибок в LogDB, не совсем понятно, что нужно сделать в ServiceLayer, так что его не сделал (❌✔️)?
15) Везде используются хранимые процедуры, но не в TransactionScope (❌✔️)?
16) Все разбито на разные проекты с разными namespace'ами ✔️
17) Статику нигде не использовал, она осталась во 2 лабе :)  ✔️<br />
ORM-библиотеки не использовались, маппперы на использовались, готовые библиотеки тоже ✔️

### Примеры сгенерированных файла и схемы находятся в FileExamples
