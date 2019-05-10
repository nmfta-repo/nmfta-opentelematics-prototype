USE [OpenTelematics]
GO
SET IDENTITY_INSERT [dbo].[SimulatedData_LogEventAnnotation] ON 

INSERT [dbo].[SimulatedData_LogEventAnnotation] ([Id], [logEventId], [driverId], [comment], [dateTime]) VALUES (1000, 1037, NULL, N'test annotation', NULL)
INSERT [dbo].[SimulatedData_LogEventAnnotation] ([Id], [logEventId], [driverId], [comment], [dateTime]) VALUES (1001, 1033, NULL, N'Difficulty with device connection this morning.', NULL)
INSERT [dbo].[SimulatedData_LogEventAnnotation] ([Id], [logEventId], [driverId], [comment], [dateTime]) VALUES (1002, 1019, NULL, N'something noteworthy', NULL)
SET IDENTITY_INSERT [dbo].[SimulatedData_LogEventAnnotation] OFF
