# Wasabi File Storage
 
Console application to manipulate bucket/objects using Wasabi cloud storage: https://wasabi.com/

## The code will cover simple actions:
1. Create bucket
2. Upload an object to a bucket
3. Download a specific object from a bucket
4. Download all objects from a bucket
5. Delete a specific object from a bucket

###### Actions to do:
6. Delete all objects from a bucket
7. Move objects between buckets

###### Code to do:
- Add unit test to the project.

## Instructions:
Replace your wasabi url/keys in the appsettings.json:
```
{
  "Wasabi": {
    "URL": "YOUR_WASABI_URL",
    "Credentials": {
      "AccessKey": "YOUR_WASABI_CLIENT_KEY",
      "SecretKey": "YOUR_WASABI_SECRET_KEY"
    }
  }
}
```

You can get those details at https://docs.wasabi.com/docs/creating-a-user-account-and-access-key. You can create a FREE trial account.

