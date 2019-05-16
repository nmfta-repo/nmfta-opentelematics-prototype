# Getting started

A project to enable business resiliency for motor freight carriers with tight integrations into Telematics Service Providers (TSPs).

![NMFTA Logo](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/image1.png)

This document is written using [api-blueprint](https://apiblueprint.org/) and available online at both
[opentelematicsapi.docs.apiary.io](https://opentelematicsapi.docs.apiary.io/#) (for browsable document) and
https://github.com/nmfta-repo/nmfta-opentelematics-api for the API blueprint source. Please use https://github.com
/nmfta-repo/nmfta-opentelematics-api to raise issues and suggest changes to the API.

If a telematics system provider (TSP) suddenly goes out of business
(have had two examples of this in 2018) any commercial fleet relying on
their service will need to find a new provider. Due to the lack of a
standardized telematics data format, a commercial fleet manager will
have to reintegrate an alternate telematics provider's data format into
their existing system reporting.

The Open Telematics API (OTAPI) is intended to make the TSP-Carrier interface the same across multiple TSPs. It is not
intended to specify any aspects of the TSP's connections to their telematics devices. Neither does it imply any changes
to the location where data is stored or access controls on the data -- the data will still live at the Motor Freight
Carrier as-sourced from their accounts at the TSP.

![NMFTA Logo](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/overview.png)

This is a standardized API for retrieving telematics logs & data.
Each participating TSP would be individually responsible for the necessary
translations from their existing formats to this Open Telematics
API. Each TSP would continue to be responsible for managing their
own cloud infrastructure housing customer data. The Open Telematics API, as an additional interface,
will be made available by TSPs to allow their customers ready
access to pull data in the standardized format, especially in examples
of mixed TSP fleets.

# Contributors

This Open Telematics API was made possible through the generous contributions of thought leadership and technical expertise
of many collaborators across the heavy vehicle cyber security community, working to push the industry forward and make it
more resilient. Though some of our contributors wish to remain anonymous, we are deeply grateful to everyone who has given
their time and energy to make this a reality.

| **Fleet Managers** | **Telematics Providers** | **Independents** |
|:------------------:|:------------------------:|:----------------:|
|Bill Brown, SEFL | Geotab | Altaz Valani, Security Compass |
| | Samsara Networks, Inc.| Stephen Raio, U.S. Army Combat Capabilities Development Command |
| | | Andrew Smith, ISE Inc. |
| | | Dr. Jeremy Daily, UTulsa |

![SEFL](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/SFL2c_300dpi-resized.jpg) ![Geotab](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/geotab-logo_full-colour-rgb_resized.png) ![Samsara Networks Inc.](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/samsara_horizontal_logo_black-resized.jpg) ![Security Compass](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/securitycompass-logo-resized.jpg) ![ISE Inc.](https://raw.githubusercontent.com/nmfta-repo/nmfta-opentelematics-api/master/media/ISE_A_Trimble_Company_RGB.png)

# Authentication

**All requests** to Open Telematics API endpoints **require authentication**.

For this version of the API, `v1`, all authentication must be performed using HTTP Basic. e.g.

```http
Authorization: Basic YWRtaW46YWRtaW4=
```

This authentication method relies entirely on the security protections provided by the TLS layer; therefore HTTPS is
mandatory on all connections and implementors must follow adhere to the security requirements detailed in the *Security
Requirements for Implementors* section below.

TSPs implementing the Open Telematics API must provide a means to create username-password pair credentials and these
must be associated with roles (see section *Authorization* below).

# Authorization

TSPs implementing the Open Telematics API must include access controls for requests against the following roles.

* *Vehicle Query*

* *Vehicle Follow*

* *Driver Query*

* *Driver Follow*

* *Driver Dispatch*

* *Driver Duty*

* *HR*

* *Admin*

It must be possible for clients to usernames for Authentication (see above) which are assigned to **one role and no more
than one role**.

TSPs implementing the Open Telematics API must restrict authorization of requests to only those roles that are assigned
in the **Access Controls** tables throughout this API specification. The tables will look like the following example:

|Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
|-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
|Access:|`DENY/ALLOW` |`DENY/ALLOW`  |`DENY/ALLOW`|`DENY/ALLOW` |`DENY/ALLOW`   |`DENY/ALLOW`|`DENY/ALLOW`|`DENY/ALLOW`|

The intent of these access controls is so that carriers can limit which clients they deploy have access to:

* streaming *feeds* of objects as they are added to the TSP

* any Personally Identifiable Information (PII)

As can be seen in the **Access Controls** tables in the requests subsections of *References* that follow, the roles
are assigned `DENY/ALLOW` such that:

* the *X Query* roles have access only to collections queries

* the *X Follow* roles have access to streaming feeds and queries

* the *Vehicle X* roles cannot access any PII

* the *Driver X* roles can access both vehicle info and PII

* the *Driver Dispatch* role is allowed to update route info and send messages to drivers

* the *Driver Duty* role is allowed to externaly trigger driver duty status (e.g. *Send Duty Status Changes to the TSP*)

* the *HR* role is allowed to update Driver information and TSP portal user accounts

# Security Requirements for Implementors

All TSPs that implement an Open Telematics API instance are expected to
provide a secure service by default. In what follows we outline some
security requirements that are expected in addition to the authentication
and access control that is detailed in the sections above.

## General Security Requirements

Vendors must maintain a vulnerability response and disclosure program in
accordance with established standards such as International Organization
of Standards (ISO)/International Electrotechnical Commission (IEC)
29147:2014 (Information technology \-- Security techniques \--
Vulnerability Disclosure) and ISO/IEC 30111:2013 (Information technology
\-- Security techniques \-- Vulnerability Handling Processes).

Vendors should ensure their vulnerability response and disclosure
program conforms with the ['Legal bug bounty' safe-harbor requirements](https://github.com/EdOverflow/legal-bug-bounty)
to protect researchers and encourage the highest-quality participation.

## Open Telematics API Server Security Requirements

**TLS Configuration**

The TLS security for Open Telematics API servers is of paramount importance. All of the confidentiality and integrity
protections are relying on this layer. For this reason, Open Telematics API servers must ensure that their HTTPS / TLS
configurations are of the highest quality. Following the [Qualys SSL Labs
Guide](https://github.com/ssllabs/research/wiki/SSL-and-TLS-Deployment-Best-Practices) is reccomended.  The automated
tool, also by Qualys SSL Labs, at [https://www.ssllabs.com/ssltest/](https://www.ssllabs.com/ssltest/) will report a
'letter grade'; it is expected that TSPs will have letter grades of 'A' or higher according to that tool.

**Whitelist HTTP Methods**

This API specification (in *APIBlueprint*) has a complete list of all allowable *HTTP methods* for each resource/'end
point' of the Open Telematics API. Open Telematics server implementations should use the list of methods as a whitelist
to filter incoming requests before any additional processing.

**Rate-limit API Requests**

Since every request to the Open Telematics API must be authenticated it is possible to rate-limit Open Telematics API
requests per authenticated user. Vendors should implement rate limits on overall API requests per authenticated user,
e.g. to avoid one user exhausting server resources of others; however, vendor must not rate-limit any Open Telematics
API implementations to rates below what is offered through their other API services for telematics data. Rate-limiting
response must be implemented using response 429 and headers as detailed in this document.

**Prevent Brute-Forcing**

Authentication (for this version of the API) is tied exclusively to HTTP Basic in each request. This means that each and
every API endpoint is an opportunity to brute force any of the user credentials. Open Telematics server implementors
must enforce a global rate-limit on authentication attempts for each user. Implementors may also want to consider other
mitigations against brute-force attacks on credentials; c.f. [the OWA page on Blocking Brute Force
Attacks](https://www.owasp.org/index.php/Blocking_Brute_Force_Attacks).

**Prevent Server-Error Stacktraces**

Open Telematics server implementors must ensure that their software is deployed such that it does not include stack
traces in any response; e.g. no stacktraces in a 500 server error response.

**Prevent Resource Exhaustion by Slow-posting**

Open Telematics server implementors must ensure that, when receiving data from clients, the server implements short-
enough timeouts such that exhaustion of the server resources through malicious client 'slow-posting' is not possible.
For more details, consult this [Qualys blog post](https://blog.qualys.com/securitylabs/2011/11/02/how-to-protect-
against-slow-http-attacks).

**Input Sanitization**

Open Telematics server implementors must employ input sanitization when ingesting any data, i.e. for the `POST`, `PUT`,
`PATCH` methods. This API uses exclusively JSON for data serialization; therefore, at a minimum all input data should be
verified as valid JSON before being further processed. Furthermore, this specification includes generated JSON schema
for the expected inputs and so additional verification that the input JSON conforms to the expected schema can also be
performed before further processing is performed.

## Open Telematics API Client Security Requirements

**Certificate Pinning**

Because the confidentiality of credentials is only protected by TLS in this version, it is
very important that all Open Telematics API clients must be configured such that substitution of
any TLS certificates results in a failure to establish any connections
to the Open Telematics API server.

All clients must implement certificate pinning. i.e. All client implementations will pass the tests, 5.4-5.6, for
certificate pinning in the [OWASP MASVS](https://www.owasp.org/index.php/OWASP_Mobile_Security_Testing_Guide).

# Working With Dates

When exchanging dates as parameters to API methods or in responses from the API, you must ensure that
they are formatted properly as an ISO 8601 string (format
`yyyy-MM-ddTHH:mm:ss.fffZ`). In addition, all dates will have to first be
converted to UTC in order to ensure time zone information and daylight
savings times are accounted for correctly.

# Working with Time-Based Queries

There are many API endpoints which support searches of data based on a time range. In all cases, the time range specified
by a pair of `start` and `stop` parameters represents an inclusive-exclusive time range. i.e. a time period which
includes the moment in time given by `start` and all times up-to but not including the moment in time given by `stop.`

The matching performed in all of these searches will be a time-intersection. i.e. the objects will be returned in search
results if their associated time periods (however defined) have any intersection with the inclusive-exclusive time range
given by the pair of `start` and `stop` parameters.

Objects may have multiple time periods associated with them; e.g. events recorded and transmitted by TSPs may have the
times at which the event was created, when it was recorded and/or when it was made available by the TSP to the motor
freight carrier. Unless otherwise specified, the time period of objects against which matching is performed will be the
*creation times*. Furthermore, implementors are required to ensure that the creation times of any objects are preserved
through all data transfers, i.e. that the creation times are not modified after initial recording at the telematics
devices in the field.

In the special case of matching against an object with a single time field, *instantaneous events* the object will be
returned if its instantaneous time value is within the inclusive-exclusive time range given by the pair of `start` and
`stop` parameters.

# Working With Locations

When exchanging locations as parameters to API methods or in responses from the API, you must ensure that they are
formatted as a latitude+longitude pair (format `[-]aaa.aaaaaaa [-]ooo.ooooooo`). Positive in the first component,
Latitude, indicates North (N) and East (E) in the second component, Longitude.

Note that all locations assigned to objects by the TSP in the OTAPI are done so on a best-effort basis. Different TSPs
have different rates of location update, different accuracies and different means of mapping a previously known location
into events after the known location (e.g. prediction vs. interpolation). This Open Telematics API draft does not make
any specifications about how often, how accurate or any other statements about how geographic locations must be assigned
to objects by the TSP.

# Working with *Feed Follow* Queries

There are endpoints of the API which are intended to be polled by clients so that new data (data newer than the previous
query) is returned. Theses *Feed Follow* endpoints are intended to assist in real-time client applications.

In some cases, the clients require that no data is ever missed by following (polling the endpoint); however, due to the
nature of telematics systems there can be a high latency between time of creation and time of delivery to the TSP. Hence
there is a non-trivial cost associated with providing endpoints that can ensure that no data is missed in following.
e.g. the *Follow a Feed of Log Events* endpoint below.

Unless stated otherwise, all *Feed Follow* endpoints will be such that no data is missed by following; including cases
of very high latency between data creation and delivery. Special exceptions will be noted on the endpoints for the cases
where the clients require that they have the newest information and are not concerned with ensuring that no data is
missed.

# Error States

The common [HTTP Response Status Codes](https://github.com/for-GET/know-your-http-well/blob/master/status-codes.md) are used.

# Pagination

Where requests may return large collections and where the collections to be returned are presumed to be static the API
will include support for pagination of requests. The scheme for pagination closely follows [the pagination scheme of github](https://developer.github.com/v3/#pagination).

Requests that are paginated will return 50 items by default. The `page` parameter can be used to access later pages;
it defaults to the first page, page *1*. A custom number of items can be requested with the `count` parameter.

The total number of elements available is returned in the `X-Total-Count` response header.

# Server Performance

This API is intended to be integrated into motor freight carriers back end operations systems; therefore, there are
performance requirements to be considered as well. The API has been modeled in terms of use cases by the motor freight
carriers (see below). Each of these use cases can be considered to be executing concurrently in the motor freight
carrier. This has important design implications. For example, if a motor freight carrier has 10,000 trucks and makes
requests regarding their location once a minute this would imply 10,000 method invocations per minute with a relatively
small return data point. Other methods such as vehicle histories will have larger return data sets and require more
server side processing but would not be expected to be called with a high frequency. Given that it is not possible to
understand how these methods might be used by motor freight carriers in a final implementation it is difficult to
provide concrete performance metric requirements at this stage. Instead, implementors should consider this per-truck
scaling and design accordingly. i.e. consider that fleets can have more than 100,000 trucks and that some API endpoints
in this specification will be called rapidly (1 request per minute) whereas others will be called infrequently (1
request per day).

This specification does not go into implementation specific design decisions which are up to the individual TSPs since
such decisions are heavily dependent on their architecture. It is, however, strongly recommended that the TSPs consider
building-in performance features such as request queueing and request rate limiters to provide a stable and robust
solution. It is also recommended that the TSPs provide information on the frequency of updates for data feed
subscriptions such as the vehicle feed so the consumers can make appropriate design decisions.

# Response Size Limits

Considering the volume of data which will be generated by large fleets, there will be cases where pagination is not
sufficient to limit the load on the TSP's OTAPI servers.

The TSPs implementing OTAPI may elect to return an error `413 Request Entity Too Large` when clients make queries which
would yield a response which is deemed 'too large.' The definition of 'too large' is intentionally left open so that
TSPs can configure different limits for the various endpoints and on a per-customer basis (based on e.g. tiers of
service).

Client software must be prepare to receive error `413` and to react by reducing the size of the query or ceasing the
query; client may not retry a request when receiving a `413` error. This draft lists the endpoints where clients must be
prepared for `413` responses.

# Unknown Drivers

For the elements of Open Telematics API which are concerned with Driver log events, or other driver-associated data,
there needs to be a concept of an 'unknown driver'. Indeed this concept is part of the ELD mandate. In Open Telematics
API objects: `driverID` references to the unknown driver is represented by `null`.

# Provider Identifiers

An important use case of the Open Telematics API by motor freight carriers is to run 'mixed fleets' where there are more
than one TSP's service integrated concurrently. In such a seployment we can imagine possible issues with duplicated data
or conflicts.

To prepare a solution to these problems, this specification includes provisions to identify the source of all data by a
'Provider ID'. Implementors are required to choose a unique identifier and assign it to all 'Provider ID' fields in all
data structures where it is included in this specification. We reccommend that TSPs use their domain (e.g.
`api.provider.com`) which should be sufficiently unique; however, TSPs can elect to use whatever identifier they like
but it should be 1) recognizable as associated with that TSP and 2) remain constant throughout the lifetime of the TSP's
service.

# Extending This API

The Open Telematics API is intended to enable motor freight carriers to be able to substitute TSPs (either concurently
or as fail-overs). This, of course, means that OTAPI must include only the most common elements of all the TSPs (so-
called Lowest Common Denominator).

However, each TSP has their own special value-add and, in some cases, the motor freight carriers would rather have
access to this special value-add via OTAPI rather than include parallel integrations with SDKs in their operations.
Unique vehicle performance data is a good example of this, where it is more useful embedded with the other events in the
TSP stream.

Thus, extensions to the API will be necessary and in preparation for this the data models specified in the current API
have been left 'open'. i.e. the addition of data fields to the definitions of the objects which are returned by OTAPI
according to this specification will not cause validation errors by clients which are following the JSON schema in this
specification.

Addition of fields to the objects must be done such that it is not possible for the new extension fields to collide with
other extensions or with future versions of the API; therefore, all fields added as extensions must be prefixed with
`x_providerid_` where `providerid` is the provider ID (see *Provider Identifiers* above for more details).

* Adding addional field/members to the data objects, when the data is needed by a motor freight carrier IS a valid extension to the OTAPI

* Adding additional possible values to enumerations IS NOT a valid extension to the OTAPI

* Adding additional endpoints/methods IS NOT a valid extension to the OTAPI

In cases where enumerations need to be changed and additional endpoints need to be added, this should be approached by
changing the OTAPI upstream. Also, ideally, adding fields should also be done by suggesting changes upstream; in this
case, an example roadmap of the process might look like: starting with a prefixed field in the TSP extensions then
suggesting the useful fields for inclusion and review leading to a new field without a prefix.

# Localization

The Open Telematics API is ready to be used in locales other than the United States (English-speakers). To enable
display and interpretation of data in languages other English (US, `en_US`) implementors may provide translations of the
descriptions of the enumerated constants in the API.

The localization does not apply to units of measure. With the exception of velocities where units of `km/h` are used,
the Open Telematics API will use SI units. Nor does the localization apply to arbitrary strings in data objects of the
API such as messages for display in vehicles or comment fields.

Requests to the Translation `/i18n` endpoint (see below for more details) shall include the request header `Accept-
Language: `. If no request header is present then `Accept-Language: en` will be assumed.

Open Telematics API implementors choose which languages they will support. If the `Accept-Language: ` request header
specifies a language which is unsupported by the Open Telematics API instance, then a `406` (Not Acceptable) response
will returned. Implementors must support `Accept-Language: en` and return a complete mapping -- a sample of which is
provided below for convenience in the sample response to `GET /v1.0/i18n`.

# Telematics API Use Cases by Motor Freight Carriers

The Open Telematics API is envisioned to be complete enough that motor freight carriers can use these APIs instead of
the proprietary TSP APIs -- while still connecting-to TSP-hosted servers and hence still using the same provider. In the
interest of ensuring that the APIs are _useful_ to their intended users (motor freight carriers) we will capture -- and
organize -- the API by these use cases. See the sections below for the API endpoints in those categories. For ease of
reference, we summarize the use cases here:

***Check Provider's State of Health***

Users of telematics that is highly integrated into their operations need assurances of the state of health of the
Provider's services.

***Data Export***

Motor freight carriers want to export all data from a TSP for a given time period. Where 'all data' for the purposes of
this specification is all the data specified here and does not include any other proprietary data structures designed
and employed by the TSP. These data exports could be used by carriers to complete mandatory processes during times of
TSP outage or for research purposes offline or to restore data -- although this last potential use is _not_ a use case
we aim to support here please see below on 'data import.'

***Generate Records of Duty Status for Compliance***

Motor freight carriers use telematics systems to maintain Record of Duty Status (RODS) compliance. The TSP to the
carrier will supply compliant records directly, without the need for the carrier to use the OTAPI. The carrier may use
OTAPI for RODS in the event that compliant records need to be produced when a TSP is no longer available. i.e. as a
backup process only; however, to do so will require processing and it is not expected that OTAPI data be ready for
compliance as-is. A couple examples; the `line data check` and `file data check` values will need to be calculated after
the creation of line and files in the process of creating RODS files for compliance; OTAPI will not include these
values. Also note that preparing compliance reports is a value added by TSPs; e.g. in at least one case, the ordering
used by regulators used truncated timestamps and the higher-resolution available in the TSP RODS information led to
events being out-of-order in the view of the regulators without special handling by the TSP. This is an example of the
kinds of nuances that are handled by TPSs in their service offering and OTAPI is not meant to replace that service.

***Driver Availability***

Motor freight carriers need to understand the availability of their drivers -- within the current regulatory context of
those drivers -- so that the carrier can ensure regulatory compliance and, more importantly, driver safety.

***Driver Route & Directions Communication***

Motor freight carriers update their Driver's destination and route during their trip. This allows them to react to
changing conditions in weather, the needs of regulatory restrictions on hours, optimizing follow-on activities after a
trip, etc.

This feature is heavily used but also is commonly offered as an add-on to the TSP service from another party.

***Driver Route & Directions Start***

Motor freight carriers plan destinations and routes for their drivers and inform the drivers of the plan via the in-cab
components of a telematics system.

This feature is heavily used but also is commonly offered as an add-on to the TSP service from another party.

***Driver Route and Directions Done***

Motor freight carriers receive notifications when Drivers have completed their trip.

This feature is heavily used but also is commonly offered as an add-on to the TSP service from another party.

***Driver Messaging by Geo-Location***

Motor freight carriers use telematics systems to message their drivers for various reasons, not the least of which is to
notify the drivers of dangerous weather conditions in their area.

***Driver Messaging by Vehicle***

Motor freight carriers also message their drivers by sending messages directly to a vehicle.

***Vehicle Location Time History Tracking***

Motor freight carriers use telematics fleet Location Time History for multiple 'fleet dynamics modeling' use cases such
as: Fuel Purchase Prediction, Fuel Consumption Performance Tracking, and Fleet Maintenance Planning.

***Human Resources Process: Payroll***

Motor freight carriers use telematics systems to assist in payroll processing. This enables efficiencies at scale that
are important to modern motor freight carrier operations.

***Human Resources Process: Accident Report***

Motor freight carriers use telematics systems to monitor their fleets for accidents.

***Carrier Custom Business Intelligence***

Motor freight carriers use telematics systems for multiple, custom, business intelligence purposes where the overall
health of their fleet is considered and fed into their own proprietary models.

***Compliance and Safety Monitoring***

Motor freight carriers use telematics systems to monitor compliance and safety for their operations.

***In-Field Maintenance & Repair***

Motor freight carriers use telematics systems to plan (and react to) maintenance needs of their fleets.

## How to Build


You must have Python ```2 >=2.7.9``` or Python ```3 >=3.4``` installed on your system to install and run this SDK. This SDK package depends on other Python packages like nose, jsonpickle etc. 
These dependencies are defined in the ```requirements.txt``` file that comes with the SDK.
To resolve these dependencies, you can use the PIP Dependency manager. Install it by following steps at [https://pip.pypa.io/en/stable/installing/](https://pip.pypa.io/en/stable/installing/).

Python and PIP executables should be defined in your PATH. Open command prompt and type ```pip --version```.
This should display the version of the PIP Dependency Manager installed if your installation was successful and the paths are properly defined.

* Using command line, navigate to the directory containing the generated files (including ```requirements.txt```) for the SDK.
* Run the command ```pip install -r requirements.txt```. This should install all the required dependencies.

![Building SDK - Step 1](https://apidocs.io/illustration/python?step=installDependencies&workspaceFolder=Open%20Telematics%20API-Python)


## How to Use

The following section explains how to use the Opentelematicsapi SDK package in a new project.

### 1. Open Project in an IDE

Open up a Python IDE like PyCharm. The basic workflow presented here is also applicable if you prefer using a different editor or IDE.

![Open project in PyCharm - Step 1](https://apidocs.io/illustration/python?step=pyCharm)

Click on ```Open``` in PyCharm to browse to your generated SDK directory and then click ```OK```.

![Open project in PyCharm - Step 2](https://apidocs.io/illustration/python?step=openProject0&workspaceFolder=Open%20Telematics%20API-Python)     

The project files will be displayed in the side bar as follows:

![Open project in PyCharm - Step 3](https://apidocs.io/illustration/python?step=openProject1&workspaceFolder=Open%20Telematics%20API-Python&projectName=opentelematicsapi)     

### 2. Add a new Test Project

Create a new directory by right clicking on the solution name as shown below:

![Add a new project in PyCharm - Step 1](https://apidocs.io/illustration/python?step=createDirectory&workspaceFolder=Open%20Telematics%20API-Python&projectName=opentelematicsapi)

Name the directory as "test"

![Add a new project in PyCharm - Step 2](https://apidocs.io/illustration/python?step=nameDirectory)
   
Add a python file to this project with the name "testsdk"

![Add a new project in PyCharm - Step 3](https://apidocs.io/illustration/python?step=createFile&workspaceFolder=Open%20Telematics%20API-Python&projectName=opentelematicsapi)

Name it "testsdk"

![Add a new project in PyCharm - Step 4](https://apidocs.io/illustration/python?step=nameFile)

In your python file you will be required to import the generated python library using the following code lines

```Python
from opentelematicsapi.opentelematicsapi_client import OpentelematicsapiClient
```

![Add a new project in PyCharm - Step 4](https://apidocs.io/illustration/python?step=projectFiles&workspaceFolder=Open%20Telematics%20API-Python&libraryName=opentelematicsapi.opentelematicsapi_client&projectName=opentelematicsapi&className=OpentelematicsapiClient)

After this you can write code to instantiate an API client object, get a controller object and  make API calls. Sample code is given in the subsequent sections.

### 3. Run the Test Project

To run the file within your test project, right click on your Python file inside your Test project and click on ```Run```

![Run Test Project - Step 1](https://apidocs.io/illustration/python?step=runProject&workspaceFolder=Open%20Telematics%20API-Python&libraryName=opentelematicsapi.opentelematicsapi_client&projectName=opentelematicsapi&className=OpentelematicsapiClient)


## How to Test

You can test the generated SDK and the server with automatically generated test
cases. unittest is used as the testing framework and nose is used as the test
runner. You can run the tests as follows:

  1. From terminal/cmd navigate to the root directory of the SDK.
  2. Invoke ```pip install -r test-requirements.txt```
  3. Invoke ```nosetests```

## Initialization

### Authentication
In order to setup authentication and initialization of the API client, you need the following information.

| Parameter | Description |
|-----------|-------------|
| basic_auth_user_name | The username to use with basic authentication |
| basic_auth_password | The password to use with basic authentication |



API client can be initialized as following.

```python
# Configuration parameters and credentials
basic_auth_user_name = 'basic_auth_user_name' # The username to use with basic authentication
basic_auth_password = 'basic_auth_password' # The password to use with basic authentication

client = OpentelematicsapiClient(basic_auth_user_name, basic_auth_password)
```



# Class Reference

## <a name="list_of_controllers"></a>List of Controllers

* [OpenTelematicsDataModelController](#open_telematics_data_model_controller)
* [UseCaseCheckProviderStateOfHealthController](#use_case_check_provider_state_of_health_controller)
* [UseCaseDataExportController](#use_case_data_export_controller)
* [UseCaseDriverAvailabilityController](#use_case_driver_availability_controller)
* [UseCaseDriverRouteDirectionsCommunicationController](#use_case_driver_route_directions_communication_controller)
* [UseCaseDriverRouteDirectionsStartController](#use_case_driver_route_directions_start_controller)
* [UseCaseDriverRouteAndDirectionsDoneController](#use_case_driver_route_and_directions_done_controller)
* [UseCaseDriverMessagingByGeoLocationController](#use_case_driver_messaging_by_geo_location_controller)
* [UseCaseVehicleLocationTimeHistoryTrackingController](#use_case_vehicle_location_time_history_tracking_controller)
* [UseCaseHumanResourcesProcessPayrollController](#use_case_human_resources_process_payroll_controller)
* [UseCaseCarrierCustomBusinessIntelligenceController](#use_case_carrier_custom_business_intelligence_controller)
* [UseCaseComplianceAndSafetyMonitoringController](#use_case_compliance_and_safety_monitoring_controller)
* [UseCaseInFieldMaintenanceRepairController](#use_case_in_field_maintenance_repair_controller)
* [LocalizationController](#localization_controller)

## <a name="open_telematics_data_model_controller"></a>![Class: ](https://apidocs.io/img/class.png ".OpenTelematicsDataModelController") OpenTelematicsDataModelController

### Get controller instance

An instance of the ``` OpenTelematicsDataModelController ``` class can be accessed from the API Client.

```python
 open_telematics_data_model_controller = client.open_telematics_data_model
```

### <a name="get_a_vehicle_object_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_vehicle_object_by_its_id") get_a_vehicle_object_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_vehicle_object_by_its_id(self,
                                       id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of a Vehicle object |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_vehicle_object_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_driver_object_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_driver_object_by_its_id") get_a_driver_object_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_driver_object_by_its_id(self,
                                      id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of a Driver object |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_driver_object_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_vehicle_location_time_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_vehicle_location_time_by_its_id") get_a_vehicle_location_time_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_vehicle_location_time_by_its_id(self,
                                              id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Vehicle Location Time of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_vehicle_location_time_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_log_event_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_log_event_by_its_id") get_a_log_event_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_log_event_by_its_id(self,
                                  id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Log Event of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_log_event_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_region_specific_break_rules_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_region_specific_break_rules_by_its_id") get_a_region_specific_break_rules_by_its_id

> Rules governing driver brakes for the specific region of governance of the driver in question. The rules are defined
> only by the region that is dictating the rules, clients are expected to interpret the region to realize specific break
> rules.**Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_region_specific_break_rules_by_its_id(self,
                                                    id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Region Specific Break Rules of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_region_specific_break_rules_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_region_specific_waivers_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_region_specific_waivers_by_its_id") get_a_region_specific_waivers_by_its_id

> Waivers and exceptions for the specific region of governance of the driver in question. One entity per day of a waiver
> available**Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_region_specific_waivers_by_its_id(self,
                                                id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Region Specific Waivers of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_region_specific_waivers_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_stop_geographic_details_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_stop_geographic_details_by_its_id") get_a_stop_geographic_details_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_stop_geographic_details_by_its_id(self,
                                                id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Stop Geographic Details of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_stop_geographic_details_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_vehicle_flagged_event_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_vehicle_flagged_event_by_its_id") get_a_vehicle_flagged_event_by_its_id

> The purpose of the flagged events is to flag potential saftey issues for motor freight carrier staff to validate**Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_vehicle_flagged_event_by_its_id(self,
                                              id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Vehicle Flagged Event of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_vehicle_flagged_event_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_vehicle_fault_code_event_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_vehicle_fault_code_event_by_its_id") get_a_vehicle_fault_code_event_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_vehicle_fault_code_event_by_its_id(self,
                                                 id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Vehicle Fault Code Event of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_vehicle_fault_code_event_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_flagged_vehicle_fault_code_event_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_flagged_vehicle_fault_code_event_by_its_id") get_a_flagged_vehicle_fault_code_event_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_flagged_vehicle_fault_code_event_by_its_id(self,
                                                         id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Flagged Vehicle Fault Code Event of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_flagged_vehicle_fault_code_event_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_performance_thresholds_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_performance_thresholds_by_its_id") get_a_performance_thresholds_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_performance_thresholds_by_its_id(self,
                                               id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Performance Thresholds of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_performance_thresholds_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_vehicle_performance_event_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_vehicle_performance_event_by_its_id") get_a_vehicle_performance_event_by_its_id

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_a_vehicle_performance_event_by_its_id(self,
                                                  id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Vehicle Performance Event of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_vehicle_performance_event_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_a_driver_performance_summary_by_its_id"></a>![Method: ](https://apidocs.io/img/method.png ".OpenTelematicsDataModelController.get_a_driver_performance_summary_by_its_id") get_a_driver_performance_summary_by_its_id

> Summary statistics on performance of drivers.**Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | ALLOW      | ALLOW      |

```python
def get_a_driver_performance_summary_by_its_id(self,
                                                   id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| id |  ``` Required ```  | ID of the Driver Performance Summary of interest |



#### Example Usage

```python
id = 'id'

result = open_telematics_data_model_controller.get_a_driver_performance_summary_by_its_id(id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_check_provider_state_of_health_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseCheckProviderStateOfHealthController") UseCaseCheckProviderStateOfHealthController

### Get controller instance

An instance of the ``` UseCaseCheckProviderStateOfHealthController ``` class can be accessed from the API Client.

```python
 use_case_check_provider_state_of_health_controller = client.use_case_check_provider_state_of_health
```

### <a name="check_past_30_d_state_of_health"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseCheckProviderStateOfHealthController.check_past_30_d_state_of_health") check_past_30_d_state_of_health

> Clients can request the recent history of all service statuses. The response to this query will return all service
> status records (i.e. those returned via *Check Current State of Health*) from over the past 30 days.
> 
> Clients must treat any response other than code 200, code 401, or code 429 as equivalent to `SERVICESTATUS_MAJOR_OUTAGE`.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | ALLOW         | ALLOW      | ALLOW      | ALLOW      |

```python
def check_past_30_d_state_of_health(self)
```

#### Example Usage

```python

result = use_case_check_provider_state_of_health_controller.check_past_30_d_state_of_health()

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="check_current_state_of_health"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseCheckProviderStateOfHealthController.check_current_state_of_health") check_current_state_of_health

> Clients can request the current service state of health. The response to this query will be a data structure indicating
> everything is good or showing some details as to why the service is not presently at 100%.
> 
> Clients must treat any response other than code 200, code 401, or code 429 as equivalent to `SERVICESTATUS_MAJOR_OUTAGE`.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | ALLOW         | ALLOW      | ALLOW      | ALLOW      |

```python
def check_current_state_of_health(self)
```

#### Example Usage

```python

result = use_case_check_provider_state_of_health_controller.check_current_state_of_health()

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_data_export_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseDataExportController") UseCaseDataExportController

### Get controller instance

An instance of the ``` UseCaseDataExportController ``` class can be accessed from the API Client.

```python
 use_case_data_export_controller = client.use_case_data_export
```

### <a name="test_if_complete_export_ready"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDataExportController.test_if_complete_export_ready") test_if_complete_export_ready

> If the file is ready the response will include a URL where the complete file can be fetched; if the file is not yet
> ready then a `202` return code will be returned.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | **DENY**    | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def test_if_complete_export_ready(self,
                                      day_of)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| dayOf |  ``` Required ```  | the day of interest, specified by any timestamp within that day, including 0000h |



#### Example Usage

```python
day_of = '2019-04-05T02:04:16Z'

result = use_case_data_export_controller.test_if_complete_export_ready(day_of)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: dayOf parameter invalid |
| 401 | TODO: Add an error description |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="test_if_vehicle_only_export_ready"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDataExportController.test_if_vehicle_only_export_ready") test_if_vehicle_only_export_ready

> If the file is ready the response will include a URL where the complete file can be fetched; if the file is not yet
> ready then a `202` return code will be returned.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def test_if_vehicle_only_export_ready(self,
                                          day_of)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| dayOf |  ``` Required ```  | the day of interest, specified by any timestamp within that day, including 0000h |



#### Example Usage

```python
day_of = '2019-04-05T02:04:16Z'

result = use_case_data_export_controller.test_if_vehicle_only_export_ready(day_of)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: dayOf parameter invalid |
| 401 | TODO: Add an error description |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_driver_availability_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseDriverAvailabilityController") UseCaseDriverAvailabilityController

### Get controller instance

An instance of the ``` UseCaseDriverAvailabilityController ``` class can be accessed from the API Client.

```python
 use_case_driver_availability_controller = client.use_case_driver_availability
```

### <a name="get_driver_availability_factors"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverAvailabilityController.get_driver_availability_factors") get_driver_availability_factors

> Clients can request all the factors contributing to driver availability for a given driver, over a given time period.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_driver_availability_factors(self,
                                        driver_id,
                                        start_time,
                                        stop_time)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| driverId |  ``` Required ```  | The id of the driver who created this status change. |
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |



#### Example Usage

```python
driver_id = '63A9F0EA7BB98050796B649E85481845'
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'

result = use_case_driver_availability_controller.get_driver_availability_factors(driver_id, start_time, stop_time)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime or stopTime parameters invalid |
| 401 | TODO: Add an error description |
| 404 | Error: driverId Not Found |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_driver_breaks_and_waivers"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverAvailabilityController.get_driver_breaks_and_waivers") get_driver_breaks_and_waivers

> Clients can request any region-specific waivers and break-rules for a given driver that are applicable within a given
> time period.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | ALLOW      | ALLOW      |

```python
def get_driver_breaks_and_waivers(self,
                                      driver_id,
                                      start_time,
                                      stop_time)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| driverId |  ``` Required ```  | The id of the driver who created this status change. |
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |



#### Example Usage

```python
driver_id = '63A9F0EA7BB98050796B649E85481845'
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'

result = use_case_driver_availability_controller.get_driver_breaks_and_waivers(driver_id, start_time, stop_time)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime or stopTime parameters invalid |
| 401 | TODO: Add an error description |
| 404 | Error: driverId Not Found |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="update_driver_duty_status"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverAvailabilityController.update_driver_duty_status") update_driver_duty_status

> Clients can send custom-integrated duty status changes to the TSP to trigger duty status changes for a given driver by pushing data to this endpoint.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | **DENY**      | ALLOW      | **DENY**   | ALLOW      |

```python
def update_driver_duty_status(self,
                                  driver_id,
                                  body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| driverId |  ``` Required ```  | The id of the driver who created this status change. |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
driver_id = 'driverId'
body = ExternallyTriggeredDutyStatusChange()

use_case_driver_availability_controller.update_driver_duty_status(driver_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: driverId Not Found |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_driver_route_directions_communication_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseDriverRouteDirectionsCommunicationController") UseCaseDriverRouteDirectionsCommunicationController

### Get controller instance

An instance of the ``` UseCaseDriverRouteDirectionsCommunicationController ``` class can be accessed from the API Client.

```python
 use_case_driver_route_directions_communication_controller = client.use_case_driver_route_directions_communication
```

### <a name="update_driver_route_stop"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverRouteDirectionsCommunicationController.update_driver_route_stop") update_driver_route_stop

> Clients can update a Driver's destination; sending data to this endpoint, using a previously obtained `routeId` will
> change the destination of the route, hence also changing the stopId associated with the route.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def update_driver_route_stop(self,
                                 vehicle_id,
                                 route_id,
                                 body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| vehicleId |  ``` Required ```  | The vehicle id to associate this route to |
| routeId |  ``` Required ```  | the id of the route created, to be used for later updates to the route |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
vehicle_id = 'vehicleId'
route_id = 'routeId'
body = ExternallySourcedRouteStopDetails()

result = use_case_driver_route_directions_communication_controller.update_driver_route_stop(vehicle_id, route_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: vehicleId or routeId Not Found |
| 429 | TODO: Add an error description |




### <a name="get_stop_geographic_details"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverRouteDirectionsCommunicationController.get_stop_geographic_details") get_stop_geographic_details

> Clients can retrieve the _geographic details_ of a stop; the *Stop Geographic Details* are the specific location for the
> truck and trailer to park and a polygon of geographic points indicating the entryway onto a facility (i.e. where the
> truck should drive on approach).
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def get_stop_geographic_details(self,
                                    stop_id)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| stopId |  ``` Required ```  | The stop id to update |



#### Example Usage

```python
stop_id = 'b4655ce13cb3e137013d852bd7d687ae'

result = use_case_driver_route_directions_communication_controller.get_stop_geographic_details(stop_id)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: stopId Not Found |
| 429 | TODO: Add an error description |




### <a name="update_stop_geographic_details"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverRouteDirectionsCommunicationController.update_stop_geographic_details") update_stop_geographic_details

> Clients can update the _geographic details_ of a stop; the *Stop Geographic Details* are the specific location for the
> truck and trailer to park and a polygon of geographic points indicating the entryway onto a facility (i.e. where the
> truck should drive on approach).
> 
> Sending data to this endpoint, using a previously returned `stopId` will update the geograhic details of the stop and
> any other routes using this stop will also be updated.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def update_stop_geographic_details(self,
                                       stop_id,
                                       body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| stopId |  ``` Required ```  | The stop id to update |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
stop_id = 'stopId'
body = ExternallySourcedStopGeographicDetails()

result = use_case_driver_route_directions_communication_controller.update_stop_geographic_details(stop_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: stopId Not Found |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_driver_route_directions_start_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseDriverRouteDirectionsStartController") UseCaseDriverRouteDirectionsStartController

### Get controller instance

An instance of the ``` UseCaseDriverRouteDirectionsStartController ``` class can be accessed from the API Client.

```python
 use_case_driver_route_directions_start_controller = client.use_case_driver_route_directions_start
```

### <a name="get_vehicle_flagged_events"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverRouteDirectionsStartController.get_vehicle_flagged_events") get_vehicle_flagged_events

> Clients can retrieve all the flagged vehicle events of a given vehicle over a given period of time.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_vehicle_flagged_events(self,
                                   vehicle_id,
                                   start_time,
                                   stop_time)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| vehicleId |  ``` Required ```  | The vehicle id to associate this route to |
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |



#### Example Usage

```python
vehicle_id = '21232F297A57A5A743894A0E4A801FC3'
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'

result = use_case_driver_route_directions_start_controller.get_vehicle_flagged_events(vehicle_id, start_time, stop_time)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime or stopTime parameters invalid |
| 401 | TODO: Add an error description |
| 404 | Error: vehicleId Not Found |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="create_a_vehicle_route"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverRouteDirectionsStartController.create_a_vehicle_route") create_a_vehicle_route

> Clients can request the creation of a new route for a given vehicle, providing start & stop location along with
> additional instructions in a *Externally Sourced Route Start Stop Details* object.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def create_a_vehicle_route(self,
                               vehicle_id,
                               body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| vehicleId |  ``` Required ```  | The vehicle id to associate this route to |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
vehicle_id = 'vehicleId'
body = ExternallySourcedRouteStartStopDetails()

result = use_case_driver_route_directions_start_controller.create_a_vehicle_route(vehicle_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: vehicleId Not Found |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_driver_route_and_directions_done_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseDriverRouteAndDirectionsDoneController") UseCaseDriverRouteAndDirectionsDoneController

### Get controller instance

An instance of the ``` UseCaseDriverRouteAndDirectionsDoneController ``` class can be accessed from the API Client.

```python
 use_case_driver_route_and_directions_done_controller = client.use_case_driver_route_and_directions_done
```

### <a name="follow_fleet_log_events"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverRouteAndDirectionsDoneController.follow_fleet_log_events") follow_fleet_log_events

> Clients can follow a feed of Log Event entries as they are added to the TSP system; following is accomplished via
> polling an endpoint and providing a 'token' which evolves the window of new entries with each query in the polling.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | ALLOW       | ALLOW         | **DENY**   |    ALLOW   | ALLOW      |

```python
def follow_fleet_log_events(self,
                                token=None)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| token |  ``` Optional ```  | a since-token, pass-in the token previously returned to 'follow' new Log Events; pass in a `null` or omit this token to start with a new token set to 'now'. |



#### Example Usage

```python
token = '37A6259CC0C1DAE299A7866489DFF0BD'

result = use_case_driver_route_and_directions_done_controller.follow_fleet_log_events(token)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: token parameter invalid |
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_driver_messaging_by_geo_location_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseDriverMessagingByGeoLocationController") UseCaseDriverMessagingByGeoLocationController

### Get controller instance

An instance of the ``` UseCaseDriverMessagingByGeoLocationController ``` class can be accessed from the API Client.

```python
 use_case_driver_messaging_by_geo_location_controller = client.use_case_driver_messaging_by_geo_location
```

### <a name="get_fleet_latest_locations"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverMessagingByGeoLocationController.get_fleet_latest_locations") get_fleet_latest_locations

> Clients can retrieve the (coarse) vehicle locations (of all vehicles) over a given time period.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def get_fleet_latest_locations(self,
                                   page=None,
                                   count=None)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| page |  ``` Optional ```  | the page to select for paginated response |
| count |  ``` Optional ```  | the number of items to return |



#### Example Usage

```python
page = 127.685769241157
count = 127.685769241157

result = use_case_driver_messaging_by_geo_location_controller.get_fleet_latest_locations(page, count)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: page or count parameters invalid |
| 401 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="send_message_to_a_vehicle"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseDriverMessagingByGeoLocationController.send_message_to_a_vehicle") send_message_to_a_vehicle

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |

```python
def send_message_to_a_vehicle(self,
                                  vehicle_id,
                                  body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| vehicleId |  ``` Required ```  | The vehicle id to send the message to |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
vehicle_id = 'vehicleId'
body = ExternallySourcedVehicleDisplayMessages()

use_case_driver_messaging_by_geo_location_controller.send_message_to_a_vehicle(vehicle_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: vehicleId not found |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_vehicle_location_time_history_tracking_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseVehicleLocationTimeHistoryTrackingController") UseCaseVehicleLocationTimeHistoryTrackingController

### Get controller instance

An instance of the ``` UseCaseVehicleLocationTimeHistoryTrackingController ``` class can be accessed from the API Client.

```python
 use_case_vehicle_location_time_history_tracking_controller = client.use_case_vehicle_location_time_history_tracking
```

### <a name="get_fleet_location_history"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseVehicleLocationTimeHistoryTrackingController.get_fleet_location_history") get_fleet_location_history

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_fleet_location_history(self,
                                   start_time,
                                   stop_time,
                                   page=None,
                                   count=None)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |
| page |  ``` Optional ```  | the page to select for paginated response |
| count |  ``` Optional ```  | the number of items to return |



#### Example Usage

```python
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'
page = 127.685769241157
count = 127.685769241157

result = use_case_vehicle_location_time_history_tracking_controller.get_fleet_location_history(start_time, stop_time, page, count)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime, stopTime, page or count parameters invalid |
| 401 | TODO: Add an error description |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_human_resources_process_payroll_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseHumanResourcesProcessPayrollController") UseCaseHumanResourcesProcessPayrollController

### Get controller instance

An instance of the ``` UseCaseHumanResourcesProcessPayrollController ``` class can be accessed from the API Client.

```python
 use_case_human_resources_process_payroll_controller = client.use_case_human_resources_process_payroll
```

### <a name="update_driver_info"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseHumanResourcesProcessPayrollController.update_driver_info") update_driver_info

> Clients can request updates to the TSP's managed user accounts for drivers by sending data to this endpoint.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | **DENY**      | **DENY**   | ALLOW      | ALLOW      |

```python
def update_driver_info(self,
                           driver_id,
                           body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| driverId |  ``` Required ```  | The id of the driver who created this status change. |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
driver_id = 'driverId'
body = ExternallySourcedDriverInfo()

use_case_human_resources_process_payroll_controller.update_driver_info(driver_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: driverId Not Found |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_carrier_custom_business_intelligence_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseCarrierCustomBusinessIntelligenceController") UseCaseCarrierCustomBusinessIntelligenceController

### Get controller instance

An instance of the ``` UseCaseCarrierCustomBusinessIntelligenceController ``` class can be accessed from the API Client.

```python
 use_case_carrier_custom_business_intelligence_controller = client.use_case_carrier_custom_business_intelligence
```

### <a name="get_fleet_vehicle_info"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseCarrierCustomBusinessIntelligenceController.get_fleet_vehicle_info") get_fleet_vehicle_info

> Clients can retrieve a combination of all vehicle information for all vehicles over a given time period.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_fleet_vehicle_info(self,
                               start_time,
                               stop_time)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |



#### Example Usage

```python
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'

result = use_case_carrier_custom_business_intelligence_controller.get_fleet_vehicle_info(start_time, stop_time)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime or stopTime parameters invalid |
| 401 | TODO: Add an error description |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="follow_fleet_vehicle_info"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseCarrierCustomBusinessIntelligenceController.follow_fleet_vehicle_info") follow_fleet_vehicle_info

> Clients can follow a feed of a combination of all vehicle information for all vehicles as they are added to the TSP
> system; following is accomplished via polling an endpoint and providing a 'token' which evolves the window of new
> entries with each query in the polling.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | ALLOW        | **DENY**   | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def follow_fleet_vehicle_info(self,
                                  token=None)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| token |  ``` Optional ```  | a since-token, pass-in the token previously returned to 'follow' new Log Events; pass in a `null` or omit this token to start with a new token set to 'now'. |



#### Example Usage

```python
token = '37A6259CC0C1DAE299A7866489DFF0BD'

result = use_case_carrier_custom_business_intelligence_controller.follow_fleet_vehicle_info(token)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: token parameter invalid |
| 401 | TODO: Add an error description |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_compliance_and_safety_monitoring_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseComplianceAndSafetyMonitoringController") UseCaseComplianceAndSafetyMonitoringController

### Get controller instance

An instance of the ``` UseCaseComplianceAndSafetyMonitoringController ``` class can be accessed from the API Client.

```python
 use_case_compliance_and_safety_monitoring_controller = client.use_case_compliance_and_safety_monitoring
```

### <a name="get_driver_performance_summaries"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseComplianceAndSafetyMonitoringController.get_driver_performance_summaries") get_driver_performance_summaries

> Clients can request all driver performance summaries for a specific driver within a given period of time.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | **DENY**      | **DENY**   | ALLOW      | ALLOW      |

```python
def get_driver_performance_summaries(self,
                                         driver_id,
                                         start_time,
                                         stop_time)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| driverId |  ``` Required ```  | The id of the driver for performance summmaries |
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |



#### Example Usage

```python
driver_id = '63A9F0EA7BB98050796B649E85481845'
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'

result = use_case_compliance_and_safety_monitoring_controller.get_driver_performance_summaries(driver_id, start_time, stop_time)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime or stopTime parameter invalid |
| 401 | TODO: Add an error description |
| 404 | Error: driverId Not Found |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="update_driver_tsp_portal_account"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseComplianceAndSafetyMonitoringController.update_driver_tsp_portal_account") update_driver_tsp_portal_account

> Clients can request updates to the TSP's portal user accounts for drivers by sending data to this endpoint.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    | **DENY**      | **DENY**   | ALLOW      | ALLOW      |

```python
def update_driver_tsp_portal_account(self,
                                         driver_id,
                                         body)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| driverId |  ``` Required ```  | The id of the driver who created this status change. |
| body |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
driver_id = 'driverId'
body = ExternalTSPPortalUserManagement()

use_case_compliance_and_safety_monitoring_controller.update_driver_tsp_portal_account(driver_id, body)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 404 | Error: driverId Not Found |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="use_case_in_field_maintenance_repair_controller"></a>![Class: ](https://apidocs.io/img/class.png ".UseCaseInFieldMaintenanceRepairController") UseCaseInFieldMaintenanceRepairController

### Get controller instance

An instance of the ``` UseCaseInFieldMaintenanceRepairController ``` class can be accessed from the API Client.

```python
 use_case_in_field_maintenance_repair_controller = client.use_case_in_field_maintenance_repair
```

### <a name="follow_fleet_fault_code_events"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseInFieldMaintenanceRepairController.follow_fleet_fault_code_events") follow_fleet_fault_code_events

> Clients can follow a feed of Vehicle Fault Code Events as they are added to the TSP system; following is accomplished
> bia polling an endpoint and providing a 'token' which evolves the window of new entries with each query in the polling.
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| **DENY**    | ALLOW        | **DENY**   | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def follow_fleet_fault_code_events(self,
                                       token=None)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| token |  ``` Optional ```  | a since-token, pass-in the token previously returned to 'follow' new Log Events; pass in a `null` or omit this token to start with a new token set to 'now'. |



#### Example Usage

```python
token = '37A6259CC0C1DAE299A7866489DFF0BD'

result = use_case_in_field_maintenance_repair_controller.follow_fleet_fault_code_events(token)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: token parameters invalid |
| 401 | TODO: Add an error description |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




### <a name="get_vehicle_location_history"></a>![Method: ](https://apidocs.io/img/method.png ".UseCaseInFieldMaintenanceRepairController.get_vehicle_location_history") get_vehicle_location_history

> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |

```python
def get_vehicle_location_history(self,
                                     vehicle_id,
                                     start_time,
                                     stop_time)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| vehicleId |  ``` Required ```  | The vehicle id to associate this route to |
| startTime |  ``` Required ```  | the start-date of the search |
| stopTime |  ``` Required ```  | the stop-date of the search |



#### Example Usage

```python
vehicle_id = '21232F297A57A5A743894A0E4A801FC3'
start_time = '2019-04-05T02:04:16Z'
stop_time = '2019-04-05T02:04:16Z'

result = use_case_in_field_maintenance_repair_controller.get_vehicle_location_history(vehicle_id, start_time, stop_time)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 400 | Error: startTime or stopTime parameters invalid |
| 401 | TODO: Add an error description |
| 404 | Error: vehicleId Not Found |
| 413 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)

## <a name="localization_controller"></a>![Class: ](https://apidocs.io/img/class.png ".LocalizationController") LocalizationController

### Get controller instance

An instance of the ``` LocalizationController ``` class can be accessed from the API Client.

```python
 localization_controller = client.localization
```

### <a name="get_a_translation_table"></a>![Method: ](https://apidocs.io/img/method.png ".LocalizationController.get_a_translation_table") get_a_translation_table

> Based on [LinguiJS formats](https://lingui.js.org/ref/catalog-formats.html); where the preferred format is gettext PO
> files, which are closely represented here. Unfortunately the Lingui JS raw format and JSON formats cannot be represented
> in API Blueprint's formal spec language.Clients can retrieve the current translation table for this TSP's Open Telematics API for given language (provided in the request headers.)
> 
> **Access Controls**
> 
> |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
> |-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
> |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | ALLOW         | ALLOW      | ALLOW      | ALLOW      |

```python
def get_a_translation_table(self,
                                accept_language)
```

#### Parameters

| Parameter | Tags | Description |
|-----------|------|-------------|
| acceptLanguage |  ``` Required ```  | TODO: Add a parameter description |



#### Example Usage

```python
accept_language = 'en'

result = localization_controller.get_a_translation_table(accept_language)

```

#### Errors

| Error Code | Error Description |
|------------|-------------------|
| 401 | TODO: Add an error description |
| 406 | TODO: Add an error description |
| 429 | TODO: Add an error description |




[Back to List of Controllers](#list_of_controllers)



