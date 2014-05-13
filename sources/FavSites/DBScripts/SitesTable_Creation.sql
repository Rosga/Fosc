USE [FavouriteSites]
GO

/****** Object:  Table [dbo].[Sites]    Script Date: 21.10.2013 16:23:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sites](
	[userName] [varchar](50) NOT NULL,
	[SiteName] [varchar](50) NULL,
	[SiteLink] [varchar](50) NULL,
	[SiteImage] [varbinary](max) NULL,
	[SiteId] [int] IDENTITY(1,1) NOT NULL,
	[Position] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OF
GO


