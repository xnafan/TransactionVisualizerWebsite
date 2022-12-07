USE [master]
GO
CREATE DATABASE [TransactionSample]
 Go
USE [TransactionSample]
GO

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [Name], [Stock]) VALUES (1, N'Ticket', 10)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Stock]  DEFAULT ((10)) FOR [Stock]
GO
USE [master]
GO
ALTER DATABASE [TransactionSample] SET  READ_WRITE 
GO
