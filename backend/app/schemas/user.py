import uuid
from pydantic import BaseModel, EmailStr

# Shared properties
class UserBase(BaseModel):
    email: EmailStr
    username: str
    full_name: str | None = None
    is_active: bool = True
    is_superuser: bool = False

# Properties to receive via API on creation
class UserCreate(UserBase):
    password: str

# Properties to receive via API on update
class UserUpdate(BaseModel):
    full_name: str | None = None
    is_active: bool | None = None

# Properties shared by models stored in DB
class UserInDBBase(UserBase):
    id: uuid.UUID

    class Config:
        orm_mode = True

# Properties to return to client
class User(UserInDBBase):
    pass

# Properties stored in DB
class UserInDB(UserInDBBase):
    hashed_password: str
