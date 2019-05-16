USE [OpenTelematics]
GO
INSERT [dbo].[DriverPerformanceSummary] ([Id], [driverId], [eventStart], [eventEnd], [distance], [fuel], [cruiseTime], [engineLoadPercent], [overRpmTime], [brakeEvents]) VALUES (N'df3391ca-1ea9-4f60-9510-16f2293d14c0', N'1ccf4966-862b-42f5-8e26-5733dd08a27b', CAST(N'2019-05-12T00:00:00.0000000-05:00' AS DateTimeOffset), CAST(N'2019-05-12T10:00:00.0000000-05:00' AS DateTimeOffset), CAST(165.80000 AS Numeric(18, 5)), CAST(28.40000 AS Numeric(18, 5)), CAST(4.34000 AS Numeric(18, 5)), CAST(73.40000 AS Numeric(18, 5)), CAST(23.50000 AS Numeric(18, 5)), 0)
