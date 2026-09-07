- This System's Target & Implementation
  - Providing Hobbyist a quick web service of OpenTravelData Access Locally

- Security
  - A dedicated API key for production ready environment
    - `header`：`X-Api-Key`

- Trace
  - Every request by client would be assigned a random UUID, or the client can set up their own.
    - `header`：`X-Trace-Id` 

- Version
  - Every response would reveal the W.S. current version
    - `header`：`X-Version-Code`

- Response Body
  - `schema`： encapsulated with `ApiResponse<>`, including properties:
    - `status`：0: `Success`；-1: `Fail`；1: `Otherwise`, where `data` might have value
    - `code`：for any `Fail` or `Otherwise`
    - `name`：the name of the code
    - `description`：the description of the code
    - `message`：the message from W.S.
    - `data`：root for all data
---