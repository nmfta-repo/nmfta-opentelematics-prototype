USE [OpenTelematics]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2040930e-c2d9-4761-9d3c-973230c9e8e4', N'Admin', N'ADMIN', N'97d4c49a-edee-44b4-9471-c88a1d7afe8c')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'388ca1de-7a43-47d6-ae7c-a0333d04fddb', N'Driver Duty', N'DRIVER DUTY', N'17993223-c096-41a9-940a-3b18ec699ac3')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'61602e87-0af1-4317-b751-849c3c831361', N'Vehicle Query', N'VEHICLE QUERY', N'029dc992-b7fd-4484-9f5f-12c858a94abe')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'7c9f7ad5-3b22-4257-b0dc-d84edaad6366', N'Driver Dispatch', N'DRIVER DISPATCH', N'1bc044e9-16ff-40e8-9518-2d6f684c43de')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a1f53d01-e11c-4f5c-9b98-fa6a71eac26b', N'HR', N'HR', N'aa41bd7c-0887-4228-a264-e7d7fa9e1777')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a46add3b-f02e-49ab-8c7a-3264a9553992', N'Driver Follow', N'DRIVER FOLLOW', N'c5e9160b-4cc1-4159-adc4-d4c2eda1d7e0')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c20756d3-ea4e-48ce-bc7a-1e4019e477f5', N'Vehicle Follow', N'VEHICLE FOLLOW', N'd62dd4d2-3396-4c22-81b3-27cdeb697325')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c75dde46-c118-467f-a0eb-341d8d5602e7', N'Driver Query', N'DRIVER QUERY', N'b8ac10b1-7c28-41b8-9ed5-cc0aa6ad328c')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8c5c62fc-25f2-4c99-8df1-e48cba383c97', N'otapiclient', N'OTAPICLIENT', N'otapiclient@test.com', N'OTAPICLIENT@TEST.COM', 0, N'AQAAAAEAACcQAAAAECKBIiBMaaJ5SBhYQKb5QSskAPXDYJ5WsVgNfybQYTCSEj10UJK/LwYW227YKYqYiw==', N'D7CBT7JRZI4PLMIXBGPJX6HXEWYS54UB', N'10e85c06-d178-4e57-99d7-4e01b22e8978', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8c5c62fc-25f2-4c99-8df1-e48cba383c97', N'2040930e-c2d9-4761-9d3c-973230c9e8e4')
GO
INSERT [dbo].[ClientMaster] ([ClientId], [ClientName], [Status]) VALUES (N'CED4868A-B64C-4C40-9D17-A4D58D0A1C5A', N'Carrier Association', N'Active')
GO
INSERT [dbo].[ClientUserXRef] ([ClientUserXRefId], [ClientId], [UserId]) VALUES (N'c98cab5d-f0a4-4b48-a440-9592ea8d8b8b', N'CED4868A-B64C-4C40-9D17-A4D58D0A1C5A', N'8c5c62fc-25f2-4c99-8df1-e48cba383c97')
GO
