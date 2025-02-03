USE [Milkstore]
GO

/****** Object:  Table [dbo].[CardDetails]    Script Date: 29-01-2025 13:23:24 ******/

IF OBJECT_ID('dbo.CardDetails', 'U') IS NOT NULL
DROP TABLE [dbo].[CardDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CardDetails](
	[cardId] [int] IDENTITY(1,1) NOT NULL,
	[User_emailId] [varchar](30) NOT NULL,
	[cardNumber] [varchar](55) NOT NULL,
	[cvv] [varchar](55) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CardDetails]  WITH CHECK ADD FOREIGN KEY([User_emailId])
REFERENCES [dbo].[Users] ([emailId])
GO