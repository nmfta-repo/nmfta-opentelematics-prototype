1) Setup Python Virtual Environment => python -m venv env
2) Install Django Packages => python -m pip install django
3) Install Requirements => pip install -r requirements.txt
4) Install OTAPI Python SDK => pip install ../../sdk/python/v0.1-rc.3
3) Update Configuration options in app/settings.py
    API_SCHEME = http:// or https://
    API_HOST =  {location of otapi REST api}
    API_USER_NAME = "{api_user_name}" 
    API_PASSWORD = "{api_password}"
    GOOGLE_MAPS_API_KEY = "{google_maps_api_key}"    
4) The default database has been setup with Admin User / password otapiclient / Python@2019
5) Additional Users can be created by calling OTAPI_API_PREFIX/accountmanager/createuser?userName={user_name}&password={password}&roles={applicable_role}
6) The Account Manager end-point is added only for demo purposes to create sample users and is not part of the OTAPI Specification
7) Launch client => python manage.py runserver