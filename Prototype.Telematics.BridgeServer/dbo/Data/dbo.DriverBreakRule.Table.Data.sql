USE [OpenTelematics]
GO
INSERT [dbo].[DriverBreakRule] ([Id], [driverId], [activeFrom], [activeTo], [country], [region]) VALUES (N'ce5c3903-2d5c-4272-8393-94dcccbe87c1', N'1ccf4966-862b-42f5-8e26-5733dd08a27b', CAST(N'2019-05-01T00:00:00.0000000-05:00' AS DateTimeOffset), CAST(N'2019-05-02T10:00:00.0000000-05:00' AS DateTimeOffset), N'US', N'TX')
