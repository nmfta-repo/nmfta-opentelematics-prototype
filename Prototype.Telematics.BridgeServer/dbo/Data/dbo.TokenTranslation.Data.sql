USE [OpenTelematics]
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'CLEARTYPE_BYSYSTEMS', N'Vehicle Fault Code Event', N'cleared by systems')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'CLEARTYPE_MANUALLYBACKOFFICE', N'Vehicle Fault Code Event', N'cleared manually in backoffice')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_CERTIFICATION', N'Log Event, eventType', N'Driver certification event, can be multiple -- see `certificationCount`')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DEVICE_CONNECTED', N'Log Event, eventType', N'System log for device power connection.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DEVICE_DISCONNECTED', N'Log Event, eventType', N'System log for device power disconnection.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_DATATRANSFERDATA', N'Log Event, eventType', N'Data transfer data diagnostic event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_ENGINESYNCDATA', N'Log Event, eventType', N'Engine synchronization data diagnostic')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_MISSINGELEMENT', N'Log Event, eventType', N'Missing data elements.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_NONE', N'Log Event, eventType', N'Clear previous instance of Diagnostic')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_OTHER', N'Log Event, eventType', N'Other identified diagnostic event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_POWERDATA', N'Log Event, eventType', N'Power data diagnostic event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DIAGNOSTIC_UNIDENTIFIEDDRIVINGRECORDS', N'Log Event, eventType', N'More than 30 minutes of driving with unidentified driving.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DRIVER_LOGIN', N'Log Event, eventType', N'User login record.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DRIVER_LOGOFF', N'Log Event, eventType', N'User logout record.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DUTY_D', N'Log Event, eventType', N'Drive status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DUTY_OFF', N'Log Event, eventType', N'Off-duty status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DUTY_OFF_WT', N'Log Event, eventType', N'Wait time oil well driver status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DUTY_ON', N'Log Event, eventType', N'On-duty status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_DUTY_SB', N'Log Event, eventType', N'Sleeper berth status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_ENGINE_POWERUP', N'Log Event, eventType', N'Engine power up record.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_ENGINE_SHUTDOWN', N'Log Event, eventType', N'Engine shutdown record.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_EXEMPTION_16H', N'Log Event, eventType', N'Exemption 16 hour.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_EXEMPTION_ADVERSEWEATHER', N'Log Event, eventType', N'Adverse weather and driving conditions exemption.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_EXEMPTION_OFFDUTYDEFERRAL', N'Log Event, eventType', N'Exemption off duty deferral.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_INDICATION_NONE', N'Log Event, eventType', N'Cleared indication (e.g. no YM or PC or any other indication)')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_INDICATION_PC', N'Log Event, eventType', N'Personal conveyance driver status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_INDICATION_YM', N'Log Event, eventType', N'Yard move driver status.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_INTERMEDIATE', N'Log Event, eventType', N'Intermediate Log Event.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_DATARECORDINGCOMPLIANCE', N'Log Event, eventType', N'Storage capacity is reached, or missing data elements exist.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_DATATRANSFERCOMPLIANCE', N'Log Event, eventType', N'Transfer of data fails to complete.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_ENGINESYNCCOMPLIANCE', N'Log Event, eventType', N'Occurs when engine information (power, motion, km, and hours) cannot be obtained by ELD.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_NONE', N'Log Event, eventType', N'Clear previous instances of Malfunction.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_OTHERCOMPLIANCE', N'Log Event, eventType', N'Other instances of Malfunction.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_POSITIONINGCOMPLIANCE', N'Log Event, eventType', N'ELD continually fails to acquire valid position measurement.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_POWERCOMPLIANCE', N'Log Event, eventType', N'Engine power status engages ELD within 1 minute.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EVENTTYPE_MALFUNCTION_TIMINGCOMPLIANCE', N'Log Event, eventType', N'When ELD date and time exceeds 10 minute offset from UTC.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EXTTRIG_STATUS_OFF', N'Externally Trigger Duty Status Change, status', N'The driver has changed status to off-duty')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'EXTTRIG_STATUS_ON', N'Externally Trigger Duty Status Change, status', N'The driver has changed status to on-duty')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FILTERSTATUS_ACTIVE_REGEN_END', N'Vehicle Performance Event, particulateFilterStatus', N'Filter active regen ended')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FILTERSTATUS_ACTIVE_REGEN_START', N'Vehicle Performance Event, particulateFilterStatus', N'Filter active regen started')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FILTERSTATUS_FORCED_REGEN_WILL_HAPPEN', N'Vehicle Performance Event, particulateFilterStatus', N'Filter forced regen will happen')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FILTERSTATUS_PASSIVE_REGEN_END', N'Vehicle Performance Event, particulateFilterStatus', N'Filter passive regen ended')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FILTERSTATUS_PASSIVE_REGEN_START', N'Vehicle Performance Event, particulateFilterStatus', N'Filter passive regen started')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FILTERSTATUS_REGEN_NEEDED', N'Vehicle Performance Event, particulateFilterStatus', N'Filter regen needed')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FLAGGEDTYPE_COLLISION', N'Vehicle Flagged Event, trigger', N'Collision flagged event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FLAGGEDTYPE_ONBOARD_RECORDING', N'Vehicle Flagged Event, trigger', N'onboard recording flagged event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FLAGGEDTYPE_ROLL_STABILITY', N'Vehicle Flagged Event, trigger', N'Roll stability flagged event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FLAGGEDTYPE_SUDDEN_START', N'Vehicle Flagged Event, trigger', N'Sudden start flagged event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'FLAGGEDTYPE_SUDDEN_STOP', N'Vehicle Flagged Event, trigger', N'Sudden stop flagged event')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'GPSQUALITY_FINELOCK', N'Vehicle Flagged Event, gpsQuality', N'gps receiver had fine lock')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'GPSQUALITY_OTHER', N'Vehicle Flagged Event, gpsQuality', N'gps receiver had fix other than fine lock')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'ORIGIN_AUTOMATIC', N'Log Event, origin', N'Automatic recorded by device')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'ORIGIN_MANUAL', N'Log Event, origin', N'Manual entry by driver')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'ORIGIN_OTHERUSER', N'Log Event, origin', N'Other authenticated user')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'ORIGIN_UNASSIGNED', N'Log Event, origin', N'Unassigned driver')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'PIDORSID_PID', N'Vehicle Fault Code Event, parameterOrSubsystemIdType', N'This is a PID')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'PIDORSID_SID', N'Vehicle Fault Code Event, parameterOrSubsystemIdType', N'This is a SID')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'SERVICESTATUS_DEGRADED_PERFORMANCE', N'State of Health, status', N'the service is operating, but with limitations')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'SERVICESTATUS_MAJOR_OUTAGE', N'State of Health, status', N'the service is unavailab')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'SERVICESTATUS_OPERATIONAL', N'State of Health, status', N'the service is operational')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'SERVICESTATUS_PARTIAL_OUTAGE', N'State of Health, status', N'there are aspects of the service which are not presently available')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'STATE_ACTIVE', N'Log Event, state', N'The log is active and has been accepted by the driver.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'STATE_INACTIVE', N'Log Event, state', N'The log is inactive. It has been removed or it is the modification history of a log.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'STATE_REJECTED', N'Log Event, state', N'The log is a rejected edit request from another user.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'STATE_REQUESTED', N'Log Event, state', N'The log is a pending edit request from another user.')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'TIMERESOLUTION_MAX', N'Vehicle Locatoin Time History', N'This is at the highest available time resolution')
GO
INSERT [dbo].[TokenTranslation] ([msgId], [origin], [msgstr]) VALUES (N'TIMERESOLUTION_NOT_MAX', N'Vehicle Locatoin Time History', N'This is not at the highest available time resolution')
GO
