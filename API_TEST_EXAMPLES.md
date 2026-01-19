# API Test Examples

## Correct JSON Format for Registration

### ✅ CORRECT Format:
```json
{
  "email": "test@example.com",
  "username": "testuser",
  "password": "password123",
  "displayName": "Test User"
}
```

### ❌ INCORRECT Formats (Common Mistakes):

1. **Literal newlines in JSON** (causes 0x0A error):
```json
{
  "email": "test@example.com
",
  "username": "testuser"
}
```

2. **Missing quotes around property names**:
```json
{
  email: "test@example.com",
  username: "testuser"
}
```

3. **Trailing commas**:
```json
{
  "email": "test@example.com",
  "username": "testuser",
}
```

4. **Wrapped in extra object**:
```json
{
  "registerDto": {
    "email": "test@example.com",
    "username": "testuser"
  }
}
```

## Testing with Swagger UI

1. Navigate to `https://localhost:5001/swagger`
2. Find `POST /api/auth/register`
3. Click "Try it out"
4. **Paste the JSON directly** (don't copy-paste from formatted text that might have newlines)
5. Click "Execute"

## Testing with cURL

```bash
curl -X POST "https://localhost:5001/api/auth/register" \
  -H "Content-Type: application/json" \
  -d "{\"email\":\"test@example.com\",\"username\":\"testuser\",\"password\":\"password123\",\"displayName\":\"Test User\"}"
```

## Testing with Postman

1. Method: `POST`
2. URL: `https://localhost:5001/api/auth/register`
3. Headers: 
   - `Content-Type: application/json`
4. Body (select "raw" and "JSON"):
```json
{
  "email": "test@example.com",
  "username": "testuser",
  "password": "password123",
  "displayName": "Test User"
}
```

## Testing with PowerShell

```powershell
$body = @{
    email = "test@example.com"
    username = "testuser"
    password = "password123"
    displayName = "Test User"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/auth/register" `
    -Method Post `
    -ContentType "application/json" `
    -Body $body
```

## Testing Login

### Correct Format:
```json
{
  "emailOrUsername": "testuser",
  "password": "password123"
}
```

Note: `emailOrUsername` can be either the email address or the username.

## Common Issues

### Issue: "0x0A is invalid within a JSON string"
**Cause:** Literal newline character in JSON
**Solution:** Make sure your JSON is on a single line or properly formatted without literal newlines in string values

### Issue: "The registerDto field is required"
**Cause:** JSON is malformed or wrapped incorrectly
**Solution:** Send the JSON object directly, not wrapped in another object

### Issue: 400 Bad Request with validation errors
**Cause:** Missing required fields or invalid format
**Solution:** Check that all required fields are present:
- `email` (valid email format)
- `username` (min 3 characters)
- `password` (min 6 characters)
- `displayName` (optional)
