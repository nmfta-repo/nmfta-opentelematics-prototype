USE [OpenTelematics]
GO
INSERT [dbo].[Route] ([Id], [routeName], [startName], [startAddress], [startLatitude], [startLongitude], [comment]) VALUES (N'98cf449c-9483-4b45-a3bc-f14fecca0d2e', NULL, N'start place 202', N'11 Elm Street', CAST(37.42247640 AS Decimal(18, 8)), CAST(-122.08424990 AS Decimal(18, 8)), N'take trailers XXX and YYY')
