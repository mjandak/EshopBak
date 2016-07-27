CREATE TABLE [dbo].[Categories] (
    [id]        INT          NOT NULL,
    [title]     VARCHAR (50) NOT NULL,
    [parent_id] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Goods]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [title] NVARCHAR(50) NOT NULL, 
    [price] DECIMAL(9, 2) NOT NULL, 
    [small_image_url] NVARCHAR(50) NULL, 
    [large_image_url] NVARCHAR(50) NULL, 
    [description] NTEXT NULL, 
    [producer] NVARCHAR(50) NOT NULL, 
    [special] BIT NOT NULL
)

CREATE TABLE [dbo].[GoodsCategories]
(
	[product_id] INT NOT NULL , 
    [category_id] INT NOT NULL, 
    PRIMARY KEY ([category_id], [product_id]), 
    CONSTRAINT [FK_GoodsCategories_Goods] FOREIGN KEY ([product_id]) REFERENCES [Goods]([id]), 
    CONSTRAINT [FK_GoodsCategories_Categories] FOREIGN KEY ([category_id]) REFERENCES [Categories]([id])
)

CREATE TABLE [dbo].[ShoppingCart]
(
	[Id]     INT           NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserId] NVARCHAR(128) NOT NULL,
	CONSTRAINT [AK_dbo.ShoppingCart] UNIQUE(UserId) --uživatel má jen jeden košík, nejsou dva košíky ukazující na stejného uživatele
)

CREATE TABLE [dbo].[CartItem] (
    [ProductId] INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Title]     NVARCHAR (100) NOT NULL,
    [Price]     DECIMAL (18)   NOT NULL,
    [Quantity]  INT            NOT NULL,
    [CartId]    INT            NOT NULL,
    CONSTRAINT [FK_CartItem_ShoppingCart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[ShoppingCart] ([Id])
);

CREATE TABLE [dbo].[UsersDetails]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Street] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [ZipCode] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_UsersDetails_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [aspnet_Users]([UserId])
)

CREATE TABLE [dbo].[Orders]
(
	[OrderId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [State] NVARCHAR(13) NOT NULL, 
    CONSTRAINT [FK_Orders_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [aspnet_Users]([UserId])
)

CREATE TABLE [dbo].[OrdersGoods]
(
	[OrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [ProductPrice] DECIMAL(9, 2) NOT NULL, 
    [ProductQuantity] INT NOT NULL, 
    CONSTRAINT [FK_OrdersGoods_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([OrderId])
)
GO


CREATE PROCEDURE CreateOrder
	@UserId uniqueidentifier,
	@NewOrderId int OUT
AS
	INSERT INTO Orders (UserId, CreateDate, State) VALUES (@UserId, GETDATE(), 'zpracovává se')
	SET @NewOrderId = @@IDENTITY
GO

CREATE PROC DeleteOrder
	@OrderId int,
	@UserId uniqueidentifier,
	@Result bit OUT
AS
BEGIN
	DECLARE @IsCarriedOut nvarchar(13)
	SET @IsCarriedOut = (SELECT State FROM Orders WHERE OrderId = @OrderId AND UserId = @UserId)
	IF (@IsCarriedOut = 'zpracovává se')
	BEGIN
		DELETE OrdersGoods WHERE OrderId = @OrderId
		DELETE Orders WHERE OrderId = @OrderId AND UserId = @UserId
		SET @Result = 1
	END
	ELSE
	BEGIN
		SET @Result = 0
	END
END
GO
