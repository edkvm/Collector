USE [EddyScoresDB]
GO

/****** Object:  Table [dbo].[tblMatches]    Script Date: 06/24/2013 15:27:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMatches](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[League] [nvarchar](50) NOT NULL,
	[HomeTeam] [nvarchar](50) NOT NULL,
	[AwayTeam] [nvarchar](50) NOT NULL,
	[MatchDate] [datetime] NOT NULL,
	[HomeScore] [int] NULL,
	[AwayScore] [int] NULL,
	[Field] [nvarchar](50) NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblMatches] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


