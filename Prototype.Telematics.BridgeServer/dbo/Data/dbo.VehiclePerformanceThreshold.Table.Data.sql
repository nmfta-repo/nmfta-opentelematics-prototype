USE [OpenTelematics]
GO
INSERT [dbo].[VehiclePerformanceThreshold] ([Id], [activeFrom], [activeTo], [rpmOverValue], [overSpeedValue], [excessSpeedValue], [longIdleValue], [hiThrottleValue]) VALUES (N'540c3af2-bee7-4ff8-9459-021625b916a8', CAST(N'2019-04-02T12:00:00.0000000-05:00' AS DateTimeOffset), CAST(N'2019-04-02T14:00:00.0000000-05:00' AS DateTimeOffset), 5, 12, 3, 5, 14)
INSERT [dbo].[VehiclePerformanceThreshold] ([Id], [activeFrom], [activeTo], [rpmOverValue], [overSpeedValue], [excessSpeedValue], [longIdleValue], [hiThrottleValue]) VALUES (N'abe0a687-b8f1-4fa0-9c03-93aa35f52370', CAST(N'2019-04-05T08:00:00.0000000-05:00' AS DateTimeOffset), CAST(N'2019-04-07T10:00:00.0000000-05:00' AS DateTimeOffset), 6, 14, 9, 12, 210)
